using System;
using System.Linq;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Core.Application.Services.Data.Datasources
{
    /// <summary>
    /// This class represents a data source service.
    /// </summary>
    /// <remarks>The data source service stores sources by data sources.</remarks>
    [Serializable()]
    public class DataSourceService : DataItem, IDataSourceService
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The data sources of this instance.
        /// </summary>
        protected IDataItemSet<DataSource> _dataSourceSet = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data source set of this instance. 
        /// </summary>
        public IDataItemSet<DataSource> DataSourceSet
        {
            get { return _dataSourceSet ?? new DataItemSet<DataSource>(); }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataSourceService class.
        /// </summary>
        public DataSourceService()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataSourceService class.
        /// </summary>
        /// <param name="dataSources">The data sources to consider.</param>
        public DataSourceService(params IDataSource[] dataSources)
        {
            if (dataSources != null)
            {
                _dataSourceSet = new DataItemSet<DataSource>(dataSources.Cast<DataSource>().ToArray());
            }
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        // Module instances -----------------------------------------

        /// <summary>
        /// Adds the specified data source.
        /// </summary>
        /// <param name="source">The data source to add.</param>
        public void AddSource(IDataSource source)
        {
            if (source == null) return;

            (_dataSourceSet ?? (_dataSourceSet = new DataItemSet<DataSource>())).Add(source as DataSource);
        }

        /// <summary>
        /// Adds the specified module instances.
        /// </summary>
        /// <param name="sources">The data sources to add.</param>
        public void AddSource(params IDataSource[] sources)
        {
            if (sources != null)
            {
                foreach (IDataSource source in sources)
                {
                    AddSource(source);
                }
            }
        }

        /// <summary>
        /// Remove the specified data sources.
        /// </summary>
        /// <param name="sourceNames">Names of the data source to remove.</param>
        public void RemoveSource(params string[] sourceNames)
        {
            _dataSourceSet?.Remove(sourceNames);
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _dataSourceSet?.ClearItems();
        }

        // Sources -----------------------------------------------

        /// <summary>
        /// Gets the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public IDataSource GetSource(string sourceName)
        {
            IDataSource dataSource = null;
            if (_dataSourceSet != null)
            {
                dataSource = _dataSourceSet.GetItem(sourceName);
            }

            return dataSource;
        }

        /// <summary>
        /// Indicates whether this instance has the specified data module name.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public bool HasSource(string sourceName)
        {
            return _dataSourceSet?.HasItem(sourceName) == true;
        }

        /// <summary>
        /// Returns the module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The module name corresponding to the specified data module name.</returns>
        public string GetModuleName(string sourceName)
        {
            IDataSource source = GetSource(sourceName);

            return source != null ? source.ModuleName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceName(string sourceName)
        {
            IDataSource source = GetSource(sourceName);

            return source != null ? source.InstanceName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name otherwise module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceOtherwiseModuleName(string sourceName)
        {
            IDataSource source = GetSource(sourceName);

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
            IDataSource dataSource = GetSource(sourceName);

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
            IDataSource dataSource = GetSource(sourceName);

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
