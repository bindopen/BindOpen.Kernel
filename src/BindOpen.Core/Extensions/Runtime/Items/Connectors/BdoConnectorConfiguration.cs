using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a connector configuration.
    /// </summary>
    [XmlType("BdoConnectorConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "connector", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoConnectorConfiguration
        : TBdoExtensionTitledItemConfiguration<IBdoConnectorDefinition>, IBdoConnectorConfiguration
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorConfiguration class.
        /// </summary>
        public BdoConnectorConfiguration() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ConnectorConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public BdoConnectorConfiguration(
            string definitionUniqueId,
            params DataElement[] items)
            : base(BdoExtensionItemKind.Connector, definitionUniqueId, items)
        {
        }

        #endregion

        /// <summary>
        /// Sets the connection string with the specified string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns a clone of this instance.</returns>
        public virtual IBdoConnectorConfiguration WithConnectionString(string connectionString = null)
        {
            AddElement(ElementFactory.CreateScalar("connectionString", DataValueType.Text, connectionString));

            return this;
        }

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
            BdoConnectorConfiguration configuration = base.Clone() as BdoConnectorConfiguration;

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
        public override IBdoLog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is BdoConnectorConfiguration configuration)
            {
                log.AddEvents(Update(configuration, specificationAreas, updateModes));
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
        public override IBdoLog Check<T>(
            bool isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null)
        {
            var log = new BdoLog();

            if (item is BdoConnectorConfiguration configuration)
            {
                log.AddEvents(Check(isExistenceChecked, configuration, specificationAreas));
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
        public override IBdoLog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is BdoConnectorConfiguration configuration)
            {
                Repair(configuration, specificationAreas, updateModes);
            }
            return log;
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
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion
    }
}
