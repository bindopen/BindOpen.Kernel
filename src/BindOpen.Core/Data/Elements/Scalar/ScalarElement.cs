using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a scalar element that is an element whose items are scalars.
    /// </summary>
    [XmlType("ScalarElement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "scalar", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class ScalarElement : DataElement, IScalarElement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [XmlAttribute("value")]
        public string DtoValue
        {
            get;
            set;
        }

        /// <summary>
        /// The values of this instance.
        /// </summary>
        [XmlArray("values")]
        [XmlArrayItem("add")]
        public List<string> DtoValues
        {
            get;
            set;
        }

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new ScalarElementSpec Specification
        {
            get => base.Specification as ScalarElementSpec;
            set { base.Specification = value; }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElement class.
        /// </summary>
        public ScalarElement() : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public ScalarElement(
            string name = null,
            string id = null)
            : base(name, "scalar_", id)
        {
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override IDataElementSpec NewSpecification()
        {
            return Specification = new ScalarElementSpec();
        }

        // Items ----------------------------

        /// <summary>
        /// Indicates whether this instance contains the specified scalar item or the specified entity name.
        /// </summary>
        /// <param name="indexItem">The item to consider.</param>
        /// <param name="isCaseSensitive">Indicates whether the verification is case sensitive.</param>
        /// <returns>Returns true if this instance contains the specified scalar item or the specified entity name.</returns>
        public override bool HasItem(object indexItem, bool isCaseSensitive = false)
        {
            return Items.Any(p => p == indexItem);
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join("|", Items.Select(p => p == null ? "" : p.ToString()).ToArray());
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

            if (Items?.Count == 1)
            {
                DtoValue = Items?.FirstOrDefault()?.ToString(ValueType);
                DtoValues = null;
            }
            else
            {
                DtoValue = null;
                DtoValues = Items?.Select(p => p.ToString(ValueType)).ToList();
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            if (DtoValue != null)
            {
                SetItem(DtoValue?.ToObject(ValueType));
            }
            else
            {
                SetItems(DtoValues?.Select(p => p.ToObject(ValueType)).ToArray());
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
        public override object Clone()
        {
            ScalarElement scalarElement = base.Clone() as ScalarElement;
            return scalarElement;
        }

        #endregion
    }
}
