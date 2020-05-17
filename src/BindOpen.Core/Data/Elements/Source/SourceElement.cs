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
    /// This class represents a data source element.
    /// </summary>
    /// <remarks>A data source element can only have one item maximum.</remarks>
    [XmlType("SourceElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataSource", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class SourceElement : DataElement, ISourceElement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        [XmlAttribute("definition")]
        public string DefinitionUniqueId { get; set; } = "";

        // --------------------------------------------------

        /// <summary>
        /// Connectors of this instance.
        /// </summary>
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<BdoConnectorConfiguration> Connectors
        {
            get;
            set;
        }

        // --------------------------------------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new SourceElementSpec Specification
        {
            get { return base.Specification as SourceElementSpec; }
            set { base.Specification = value; }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SourceElement class.
        /// </summary>
        public SourceElement() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SourceElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public SourceElement(string name = null, string id = null)
            : base(name, "source_", id)
        {
            ValueType = DataValueTypes.Datasource;
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
        public ISourceElement WithConfiguration(IBdoConnectorConfiguration configuration)
        {
            WithItems(configuration);

            return this;
        }

        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        /// <returns></returns>
        public IBdoConnectorConfiguration Item()
        {
            return this[0] as IBdoConnectorConfiguration;
        }

        // Specification ---------------------

        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override IDataElementSpec NewSpecification()
        {
            return Specification = new SourceElementSpec();
        }

        // Items ----------------------------

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
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            SourceElement dataSourceElement = base.Clone(areas) as SourceElement;
            return dataSourceElement;
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

            Connectors = Items?.Select(p =>
            {
                BdoConnectorConfiguration configuration = p as BdoConnectorConfiguration;
                configuration?.UpdateStorageInfo(log);
                return configuration;
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
            WithItems(Connectors?.Select(p =>
            {
                p.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                return p;
            }).ToArray());

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion
    }
}
