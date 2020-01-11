using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Helpers.Objects;
using BindOpen.Framework.Extensions.Runtime;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Data.Elements
{
    /// <summary>
    /// This class represents a carrier element.
    /// </summary>
    [XmlType("CarrierElement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "carrier", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class CarrierElement : DataElement, ICarrierElement
    {
        /// <summary>
        /// Returns the element with the specified indexed.
        /// </summary>
        [XmlIgnore()]
        public new IBdoCarrierConfiguration this[int index] => base[index] as BdoCarrierConfiguration;

        /// <summary>
        /// Returns the element with the specified unique name.
        /// </summary>
        [XmlIgnore()]
        public new IBdoCarrierConfiguration this[string name] => base[name] as BdoCarrierConfiguration;

        /// <summary>
        /// Returns the first item.
        /// </summary>
        [XmlIgnore()]
        public new IBdoCarrierConfiguration First => this[0];

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        [XmlElement("definition")]
        public string DefinitionUniqueId { get; set; } = "";

        // --------------------------------------------------

        /// <summary>
        /// Carriers of this instance.
        /// </summary>
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<BdoCarrierConfiguration> Carriers
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the Carriers property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool CarriersSpecified => Items.Count > 0;

        // --------------------------------------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new CarrierElementSpec Specification
        {
            get { return base.Specification as CarrierElementSpec; }
            set { base.Specification = value; }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CarrierElement class.
        /// </summary>
        public CarrierElement() : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CarrierElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public CarrierElement(
            string name = null,
            string id = null)
            : base(name, "carrier_", id)
        {
            ValueType = DataValueType.Carrier;
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
            return Specification = new CarrierElementSpec();
        }

        // Items ----------------------------

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="item">The item to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public override void SetItem(
            object item)
        {
            base.SetItem(item);

            if (this[0] is BdoCarrierConfiguration configuration && !string.IsNullOrEmpty(configuration.DefinitionUniqueId))
                DefinitionUniqueId = configuration?.DefinitionUniqueId;
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
                return Items.Any(p => p.KeyEquals(indexItem));

            return false;
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join("|", Items.Select(p => (p as BdoEntityConfiguration)?.Key() ?? "").ToArray());
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

            Carriers = Items?.Select(p =>
                {
                    BdoCarrierConfiguration configuration = p as BdoCarrierConfiguration;
                    configuration?.UpdateStorageInfo(log);
                    return configuration;
                }).ToList();
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            SetItems(Carriers?.Select(p =>
                {
                    p.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                    return p;
                }).ToArray());
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
            CarrierElement dataCarrierElement = base.Clone() as CarrierElement;
            return dataCarrierElement;
        }

        #endregion
    }
}

