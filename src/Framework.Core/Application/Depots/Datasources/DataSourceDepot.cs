using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Core.Application.Depots.Datasources
{
    /// <summary>
    /// This class represents a data source service.
    /// </summary>
    /// <remarks>The data source service stores sources by data sources.</remarks>
    [Serializable()]
    public class DataSourceDepot : DataItemSet<DataSource>, IDataSourceDepot
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Elements of this instance.
        /// </summary>
        [XmlArray("dataSources")]
        [XmlArrayItem("add")]
        public List<DataSource> Sources
        {
            get { return _items; }
            set { _items = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataSourceDepot class.
        /// </summary>
        public DataSourceDepot()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataSourceDepot class.
        /// </summary>
        /// <param name="dataSources">The data sources to consider.</param>
        public DataSourceDepot(params DataSource[] dataSources) : base(dataSources)
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
            IDataSource source = GetItem(sourceName);

            return source != null ? source.ModuleName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceName(string sourceName)
        {
            IDataSource source = GetItem(sourceName);

            return source != null ? source.InstanceName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name otherwise module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceOtherwiseModuleName(string sourceName)
        {
            IDataSource source = GetItem(sourceName);

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
        public IConnectorConfiguration GetConnectorConfiguration(
            string sourceName,
            string connectorDefinitionUniqueId)
        {
            IDataSource dataSource = GetItem(sourceName);

            return dataSource?.GetConfiguration(connectorDefinitionUniqueId);
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector configuration.
        /// </summary>
        /// <param name="sourceName">The name of the data module to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The unique ID of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public bool HasConnectorConfiguration(string sourceName, string connectorDefinitionUniqueId)
        {
            IDataSource dataSource = GetItem(sourceName);

            return dataSource?.HasConfiguration(connectorDefinitionUniqueId) == true;
        }

        /// <summary>
        /// Returns the connection string corresponding to the specified configuration.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector unique name to consider.</param>
        /// <returns>The connection string corresponding to the specified data module name.</returns>
        public string GetStringConnection(
            string sourceName,
            string connectorDefinitionUniqueId)
        {
            IConnectorConfiguration configuration = GetConnectorConfiguration(sourceName, connectorDefinitionUniqueId);

            return configuration != null ? configuration["connectionString"]?.GetObject() as string : StringHelper.__NoneString;
        }

        #endregion
    }
}
