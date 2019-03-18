using System;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;

namespace BindOpen.Framework.Core.Application.Datasources
{

    /// <summary>
    /// This class represents a data source service.
    /// </summary>
    /// <remarks>The data source service stores sources by data sources.</remarks>
    [Serializable()]
    public class DataSourceService : DataItem
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The data sources of this instance.
        /// </summary>
        protected DataItemSet<DataSource> _DataSourceSet = null;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data sources of this instance. 
        /// </summary>
        public DataItemSet<DataSource> DataSourceSet
        {
            get { return this._DataSourceSet ?? new DataItemSet<DataSource>(); }
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
        public DataSourceService(params DataSource[] dataSources)
        {
            if (dataSources != null)
                this._DataSourceSet = new DataItemSet<DataSource>(dataSources);
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
        public void AddSource(DataSource source)
        {
            if (source == null)
                return;

            if (this._DataSourceSet == null)
                this._DataSourceSet = new DataItemSet<DataSource>();

            this._DataSourceSet.Add(source);
        }

        /// <summary>
        /// Adds the specified module instances.
        /// </summary>
        /// <param name="sources">The data sources to add.</param>
        public void AddSource(params DataSource[] sources)
        {
            if (sources != null)
                foreach (DataSource source in sources)
                    this.AddSource(source);
        }

        /// <summary>
        /// Remove the specified data sources.
        /// </summary>
        /// <param name="sourceNames">Names of the data source to remove.</param>
        public void RemoveSource(params String[] sourceNames)
        {
            if (this._DataSourceSet != null)
                this._DataSourceSet.Remove(sourceNames);
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
            if (this._DataSourceSet != null)
                this._DataSourceSet.ClearItems();
        }

        // Sources -----------------------------------------------

        /// <summary>
        /// Gets the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public DataSource GetSource(String sourceName)
        {
            DataSource dataSource = null;
            if (this._DataSourceSet != null)
            {
                dataSource = this._DataSourceSet.GetItem(sourceName);
            }

            return dataSource;
        }

        /// <summary>
        /// Indicates whether this instance has the specified data module name.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public Boolean HasSource(String sourceName)
        {
            return this._DataSourceSet != null && this._DataSourceSet.HasItem(sourceName);
        }

        /// <summary>
        /// Returns the module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The module name corresponding to the specified data module name.</returns>
        public String GetModuleName(String sourceName)
        {
            DataSource source = this.GetSource(sourceName);

            return source != null ? source.ModuleName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public String GetInstanceName(String sourceName)
        {
            DataSource source = this.GetSource(sourceName);

            return source != null ? source.InstanceName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name otherwise module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public String GetInstanceOtherwiseModuleName(String sourceName)
        {
            DataSource source = this.GetSource(sourceName);

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
        /// <param name="connectorDefinitionUniqueName">The unique name of the connector definition to consider.</param>
        /// <returns>The specified connector.</returns>
        public ConnectorConfiguration GetConnectorConfiguration(String sourceName, String connectorDefinitionUniqueName)
        {
            DataSource dataSource = this.GetSource(sourceName);

            return dataSource?.GetConfiguration(connectorDefinitionUniqueName);
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector configuration.
        /// </summary>
        /// <param name="sourceName">The name of the data module to consider.</param>
        /// <param name="connectorDefinitionUniqueName">The unique name of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public Boolean HasConnectorConfiguration(String sourceName, String connectorDefinitionUniqueName)
        {
            DataSource dataSource = this.GetSource(sourceName);

            return dataSource != null && dataSource.HasConfiguration(connectorDefinitionUniqueName);
        }

        /// <summary>
        /// Returns the connection string corresponding to the specified configuration.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <param name="connectorDefinitionUniqueName">The connector unique name to consider.</param>
        /// <returns>The connection string corresponding to the specified data module name.</returns>
        public String GetStringConnection(
            String sourceName,
            String connectorDefinitionUniqueName)
        {
            ConnectorConfiguration configuration = this.GetConnectorConfiguration(sourceName, connectorDefinitionUniqueName);

            return configuration != null ? configuration.ConnectionString : StringHelper.__NoneString;
        }

        #endregion

    }

}
