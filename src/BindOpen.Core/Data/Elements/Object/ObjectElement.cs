using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Assemblies;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a object element that is an element whose items are entities.
    /// </summary>
    [XmlType("ObjectElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "object", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ObjectElement : DataElement, IObjectElement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The class full name of this instance.
        /// </summary>
        [XmlAttribute("class")]
        [DefaultValue("")]
        public string ClassFullName { get; set; } = "";

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        [XmlAttribute("definition")]
        [DefaultValue("")]
        public string DefinitionUniqueId { get; set; } = "";

        // --------------------------------------------------

        /// <summary>
        /// Objects of this instance.
        /// </summary>
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<DataElementSet> Objects
        {
            get;
            set;
        }

        // Specifcation -----------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new ObjectElementSpec Specification
        {
            get { return base.Specification as ObjectElementSpec; }
            set { base.Specification = value; }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ObjectElement class.
        /// </summary>
        public ObjectElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ObjectElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public ObjectElement(string name = null, string id = null)
            : base(name, "objectElem_", id)
        {
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        // Specification ---------------------

        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override IDataElementSpec NewSpecification()
        {
            return Specification = new ObjectElementSpec();
        }

        // Items ----------------------------

        /// <summary>
        /// Adds a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        protected override void Add(object item)
        {
            if (item != null)
            {
                base.Add(item);
                if (this[0] is DataItem)
                {
                    Assembly assembly = this[0].GetType().Assembly;
                    ClassFullName = this[0].GetType().FullName.ToString()
                        + (assembly == null ? "" : "," + assembly.GetName().Name);
                }
            }
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join("|", Items.Select(p => (p as NamedDataItem).Key() ?? "").ToArray());
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);

            Objects = Items?.Select(p =>
            {
                DataElementSet elementSet = ElementFactory.CreateSetFromObject<DataElementSet>(p);
                elementSet?.UpdateStorageInfo(log);
                return elementSet;
            }).ToList();
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            foreach (DataElementSet elementSet in Objects)
            {
                AssemblyHelper.CreateInstance(ClassFullName, out object item).AddEventsTo(log);

                if (!log.HasErrorsOrExceptions() && (item is DataItem))
                {
                    elementSet.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                    item.UpdateFromElementSet<DetailPropertyAttribute>(elementSet, scope, scriptVariableSet);
                }

                Add(item);
            }

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            ObjectElement aObjectElement = base.Clone(areas) as ObjectElement;
            //if (DataSchemreference != null)
            //    aObjectElement.DataSchemreference = DataSchemreference.Clone() as DataHandler;

            return aObjectElement;
        }

        #endregion
    }
}
