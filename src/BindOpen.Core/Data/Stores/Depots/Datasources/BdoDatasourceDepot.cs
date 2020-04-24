using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a data source depot.
    /// </summary>
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
            get;
            set;
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
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public override IDatasource Get(string key = null)
        {
            if (key == null) return Items.FirstOrDefault(p => p.IsDefault) ?? this[0];
            return this[key];
        }

        /// <summary>
        /// Returns the module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The module name corresponding to the specified data module name.</returns>
        public string GetModuleName(string sourceName = null)
        {
            IDatasource source = Get(sourceName);

            return source != null ? source.ModuleName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceName(string sourceName = null)
        {
            IDatasource source = Get(sourceName);

            return source != null ? source.InstanceName : StringHelper.__NoneString;
        }

        /// <summary>
        /// Returns the instance name otherwise module name of the specified data source.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceOtherwiseModuleName(string sourceName = null)
        {
            IDatasource source = Get(sourceName);

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
            string sourceName = null,
            string connectorDefinitionUniqueId = null)
        {
            IDatasource dataSource = Get(sourceName);

            return dataSource?.GetConfiguration(connectorDefinitionUniqueId);
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector configuration.
        /// </summary>
        /// <param name="sourceName">The name of the data module to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The unique ID of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public bool HasConnectorConfiguration(string sourceName = null, string connectorDefinitionUniqueId = null)
        {
            IDatasource dataSource = Get(sourceName);

            return dataSource?.HasConfiguration(connectorDefinitionUniqueId) == true;
        }

        /// <summary>
        /// Returns the connection string corresponding to the specified configuration.
        /// </summary>
        /// <param name="sourceName">The name of the data source to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector unique name to consider.</param>
        /// <returns>The connection string corresponding to the specified data module name.</returns>
        public string GetConnectionString(
            string sourceName = null,
            string connectorDefinitionUniqueId = null)
        {
            IBdoConnectorConfiguration configuration = GetConnectorConfiguration(sourceName, connectorDefinitionUniqueId);

            return configuration != null ? configuration["connectionString"]?.GetValue() as string : StringHelper.__NoneString;
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

            Sources = Items?.Cast<Datasource>().ToList();
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            Items = Sources?.Cast<IDatasource>().ToList();

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion
    }
}