using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a carrier element.
    /// </summary>
    [XmlType("CarrierElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "carrier", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class CarrierElement : DataElement, ICarrierElement
    {
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
        public CarrierElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CarrierElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public CarrierElement(string name = null, string id = null)
            : base(name, "carrierElem_", id)
        {
            ValueType = DataValueTypes.Carrier;
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        /// <summary>
        /// Sets the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public ICarrierElement WithConfiguration(IBdoCarrierConfiguration configuration)
        {
            WithItems(configuration);

            return this;
        }

        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        /// <returns></returns>
        public IBdoCarrierConfiguration Item()
        {
            return this[0] as IBdoCarrierConfiguration;
        }

        // Specification ---------------------

        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override IDataElementSpec NewSpecification()
        {
            return Specification = new CarrierElementSpec();
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
                if (this[0] is BdoCarrierConfiguration configuration
                    && !string.IsNullOrEmpty(configuration.DefinitionUniqueId))
                {
                    DefinitionUniqueId = configuration?.DefinitionUniqueId;
                }
            }
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
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            WithItems(Carriers?.Select(p =>
                {
                    p.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                    return p;
                }).ToArray());

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
            CarrierElement dataCarrierElement = base.Clone(areas) as CarrierElement;
            return dataCarrierElement;
        }

        #endregion
    }
}

