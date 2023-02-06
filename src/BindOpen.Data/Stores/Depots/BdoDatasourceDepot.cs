using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a data source depot.
    /// </summary>
    public class BdoDatasourceDepot : TBdoDepot<IBdoDatasource>, IBdoSourceDepot
    {
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
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public override IBdoDatasource Get(string key = null)
        {
            if (key == null) return Items?.FirstOrDefault(p => p.IsDefault) ?? this[0];
            return this[key];
        }

        /// <summary>
        /// Returns the module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The module name corresponding to the specified data module name.</returns>
        public string GetModuleName(string sourceName = null)
        {
            IBdoDatasource source = Get(sourceName);

            return source != null ? source.ModuleName : null;
        }

        /// <summary>
        /// Returns the instance name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceName(string sourceName = null)
        {
            IBdoDatasource source = Get(sourceName);

            return source != null ? source.InstanceName : null;
        }

        /// <summary>
        /// Returns the instance name otherwise module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceOtherwiseModuleName(string sourceName = null)
        {
            IBdoDatasource source = Get(sourceName);

            string name = (source == null ?
                null :
                source.InstanceName?.Length > 0 ? source.InstanceName : source.ModuleName);

            return name;
        }

        // Configurations ------------------------------------------

        /// <summary>
        /// Gets the specified connector config.
        /// </summary>
        /// <param name="sourceName">The name of the data module to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The unique ID of the connector definition to consider.</param>
        /// <returns>The specified connector.</returns>
        public IBdoConfiguration GetConnectorConfig(
            string sourceName = null,
            string connectorDefinitionUniqueId = null)
        {
            IBdoDatasource dataSource = Get(sourceName);

            return dataSource?.GetConfig(connectorDefinitionUniqueId);
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector config.
        /// </summary>
        /// <param name="sourceName">The name of the data module to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The unique ID of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public bool HasConnectorConfig(string sourceName = null, string connectorDefinitionUniqueId = null)
        {
            IBdoDatasource dataSource = Get(sourceName);

            return dataSource?.HasConfig(connectorDefinitionUniqueId) == true;
        }

        /// <summary>
        /// Returns the connection string corresponding to the specified config.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector unique name to consider.</param>
        /// <returns>The connection string corresponding to the specified data module name.</returns>
        public string GetConnectionString(
            string sourceName = null,
            string connectorDefinitionUniqueId = null)
        {
            IBdoConfiguration config = GetConnectorConfig(sourceName, connectorDefinitionUniqueId);

            return config?.GetData<string>("connectionString");
        }

        #endregion
    }
}