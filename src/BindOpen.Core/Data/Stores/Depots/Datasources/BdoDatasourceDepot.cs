using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a data source depot.
    /// </summary>
    [Serializable()]
    [XmlType("DatasourceDepot", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataSource.depot", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoDatasourceDepot : TBdoDepot<IDatasource>, IBdoDatasourceDepot
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The sources of this instance.
        /// </summary>
        [XmlArray("datasources")]
        [XmlArrayItem("add")]
        public List<Datasource> Sources
        {
            get { return _items?.Select(p => p as Datasource)?.ToList(); }
            set { _items = value?.Select(p => p as IDatasource)?.ToList(); }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDatasourceDepot class.
        /// </summary>
        public BdoDatasourceDepot() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The module name corresponding to the specified data module name.</returns>
        public string GetModuleName(string sourceName)
        {
            IDatasource source = GetItem(sourceName);

            return source != null ? source.ModuleName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceName(string sourceName)
        {
            IDatasource source = GetItem(sourceName);

            return source != null ? source.InstanceName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name otherwise module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceOtherwiseModuleName(string sourceName)
        {
            IDatasource source = GetItem(sourceName);

            string name = (source == null ?
                StringHelper.__NoneString :
                source.InstanceName?.Length > 0 ? source.InstanceName : source.ModuleName);

            return name;
        }

        // Configurations ------------------------------------------

        /// <summary>
        /// Gets the specified connector configuration.
        /// </summary>
        /// <param name="sourceName">The name of the data module to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The unique ID of the connector definition to consider.</param>
        /// <returns>The specified connector.</returns>
        public IBdoConnectorConfiguration GetConnectorConfiguration(
            string sourceName,
            string connectorDefinitionUniqueId = null)
        {
            IDatasource dataSource = GetItem(sourceName);

            return dataSource?.GetConfiguration(connectorDefinitionUniqueId);
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector configuration.
        /// </summary>
        /// <param name="sourceName">The name of the data module to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The unique ID of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public bool HasConnectorConfiguration(string sourceName, string connectorDefinitionUniqueId = null)
        {
            IDatasource dataSource = GetItem(sourceName);

            return dataSource?.HasConfiguration(connectorDefinitionUniqueId) == true;
        }

        /// <summary>
        /// Returns the connection string corresponding to the specified configuration.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector unique name to consider.</param>
        /// <returns>The connection string corresponding to the specified data module name.</returns>
        public string GetConnectionString(
            string sourceName,
            string connectorDefinitionUniqueId = null)
        {
            IBdoConnectorConfiguration configuration = GetConnectorConfiguration(sourceName, connectorDefinitionUniqueId);

            return configuration != null ? configuration["connectionString"]?.GetObject() as string : StringHelper.__NoneString;
        }

        #endregion
    }
}