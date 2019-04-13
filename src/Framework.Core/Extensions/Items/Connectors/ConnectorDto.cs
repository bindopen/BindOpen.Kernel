using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors
{
    /// <summary>
    /// This class represents a connector configuration.
    /// </summary>
    [XmlType("ConnectorDto", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "connector", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ConnectorDto
        : TAppExtensionTitledItemDto<IConnectorDefinition>, IConnectorDto
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        private string _connectionString = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        [XmlElement("connectionString")]
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = (value ?? "").Replace("\n", "").Trim();
            }
        }

        /// <summary>
        /// Specification of the Connectionstring property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ConnectionStringSpecified => !string.IsNullOrEmpty(_connectionString);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorDto class.
        /// </summary>
        public ConnectorDto() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ConnectorDto class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ConnectorDto(
            string definitionUniqueId,
            string connectionString = null,
            params IDataElement[] items)
            : base(AppExtensionItemKind.Connector, definitionUniqueId, items)
        {
            _connectionString = connectionString;
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A cloned connector of this instance.</returns>
        public override object Clone()
        {
            ConnectorDto configuration = base.Clone() as ConnectorDto;

            return configuration;
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            ILog log = new Log();

            if (item is ConnectorDto configuration)
            {
                _connectionString = configuration.ConnectionString;
                log.Append(Update(configuration, specificationAreas, updateModes));
            }
            return log;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public override ILog Check<T>(
            bool isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null)
        {
            ILog log = new Log();

            if (item is ConnectorDto configuration)
            {
                _connectionString = configuration.ConnectionString;
                log.Append(Check(isExistenceChecked, configuration, specificationAreas));
            }
            return log;
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        public override ILog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            ILog log = new Log();

            if (item is ConnectorDto configuration)
            {
                _connectionString = configuration.ConnectionString;
                Repair(configuration, specificationAreas, updateModes);
            }
            return log;
        }

        #endregion
    }
}
