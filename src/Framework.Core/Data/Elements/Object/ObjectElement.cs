using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This class represents a object element that is an element whose items are entities.
    /// </summary>
    [XmlType("ObjectElement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "object", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
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
        public string ClassFullName { get; set; } = "";

        /// <summary>
        /// Specification of the ClassFullName property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ClassFullNameSpecified => !string.IsNullOrEmpty(ClassFullName);

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        [XmlAttribute("definition")]
        public string DefinitionUniqueId { get; set; } = "";

        /// <summary>
        /// Specification of the DefinitionUniqueId property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DefinitionUniqueIdSpecified => !string.IsNullOrEmpty(DefinitionUniqueId);

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

        /// <summary>
        /// Specification of the Objects property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ObjectsSpecified => Items.Count > 0;

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
        /// Initializes a new entity element.
        /// </summary>
        public ObjectElement()
            : base(null, "ObjectElement_")
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="classFullName">The class full name to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            string name,
            string id,
            string classFullName,
            IObjectElementSpec specification,
            params IDataItem[] items)
            : base(name, "ObjectElement_", id)
        {
            this.ValueType = DataValueType.Object;
            this.Specification = specification as ObjectElementSpec;

            this.SetItem(items);
            if (!string.IsNullOrEmpty(classFullName))
                this.ClassFullName = classFullName;
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="classFullName">The entity unique name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            string name,
            string classFullName,
            params IDataItem[] items)
            : this(name, null, classFullName, null, items)
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            string name,
            params IDataItem[] items)
            : this(name, null, null, null, items)
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            params IDataItem[] items)
            : this(null, null, null, null, items)
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
        public override DataElementSpec NewSpecification()
        {
            return Specification = new ObjectElementSpec();
        }

        // Items ----------------------------

        /// <summary>
        /// Adds a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <param name="log">The log to populate.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public override bool AddItem(
            object item,
            IBdoLog log = null)
        {
            bool boolean = base.AddItem(item, log);
            if (this[0] is DataItem)
            {
                Assembly assembly = this[0].GetType().Assembly;
                this.ClassFullName = this[0].GetType().FullName.ToString()
                    + (assembly == null ? "" : "," + assembly.GetName().Name);
            }
            return boolean;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="item">The item to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public override void SetItem(
            object item)
        {
            base.SetItem(item);
        }

        /// <summary>
        /// Indicates whether this instance contains the specified scalar item or the specified entity name.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="isCaseSensitive">Indicates whether the verification is case sensitive.</param>
        /// <returns>Returns true if this instance contains the specified scalar item or the specified entity name.</returns>
        public override bool HasItem(object indexItem, bool isCaseSensitive = false)
        {
            if (indexItem is string)
                return this.Items.Any(p => p.KeyEquals(indexItem));

            return false;
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join("|", this.Items.Select(p => (p as NamedDataItem).Key() ?? "").ToArray());
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
                DataElementSet elementSet = ElementFactory.CreateSet<DataElementSet>(p);
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
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            foreach (DataElementSet elementSet in Objects)
            {
                log.Append(AssemblyHelper.CreateInstance(ClassFullName, out object item));

                if (!log.HasErrorsOrExceptions() && (item is DataItem dataItem))
                {
                    elementSet.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                    item.UpdateFromElementSet<DetailPropertyAttribute>(elementSet, scope, scriptVariableSet);
                }

                AddItem(item);
            }
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
        public override object Clone()
        {
            ObjectElement aObjectElement = base.Clone() as ObjectElement;
            //if (this.DataSchemreference != null)
            //    aObjectElement.DataSchemreference = this.DataSchemreference.Clone() as DataHandler;

            return aObjectElement;
        }

        #endregion
    }
}
