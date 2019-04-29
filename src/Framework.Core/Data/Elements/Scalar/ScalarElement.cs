using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Scalar
{
    /// <summary>
    /// This class represents a scalar element that is an element whose items are scalars.
    /// </summary>
    [Serializable()]
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
        public string Value
        {
            get => Items.GetObjectAtIndex(0).ToString(ValueType);
            set => SetItem(value.ToObject(ValueType));
        }

        /// <summary>
        /// Specification of the Value property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ValueSpecified => Items?.Count == 1;

        /// <summary>
        /// Items of this instance.
        /// </summary>
        [XmlArray("values")]
        [XmlArrayItem("add")]
        public List<string> StringScalars
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the Items property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool StringScalarsSpecified => Items?.Count > 1;

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
        public override DataElementSpec NewSpecification()
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
            //String aStringItem = GetStringFromObject(indexItem);
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
        public override void UpdateStorageInfo(ILog log = null)
        {
            base.UpdateStorageInfo(log);

            StringScalars = Items?.Select(p => p.ToString(ValueType)).ToList();
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, scriptVariableSet, log);

            if (!ValueSpecified)
            {
                SetItems(StringScalars?.Select(p => p.ToObject(ValueType)).ToArray());
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
            ScalarElement scalarElement = MemberwiseClone() as ScalarElement;
            return scalarElement;
        }

        #endregion
    }
}
