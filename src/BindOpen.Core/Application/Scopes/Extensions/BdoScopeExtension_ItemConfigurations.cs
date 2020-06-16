using BindOpen.Data.Helpers.Serialization;
using BindOpen.Extensions;
using BindOpen.Extensions.Definition;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// This static class provides methods to create extension DTO item.
    /// </summary>
    public static class BdoScopeExtension_ItemConfigurations
    {
        // Carriers ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static IBdoCarrierConfiguration CreateCarrierConfiguration(
            this IBdoScope scope,
            string definitionUniqueId,
            IBdoLog log = null)
        {
            IBdoCarrierConfiguration configuration = null;

            IBdoCarrierDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<BdoCarrierDefinition>(definitionUniqueId);

            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionUniqueId + "' of kind '" + BdoExtensionItemKind.Carrier.ToString() + "'");
            }
            else
            {
                configuration = BdoExtensionFactory.CreateCarrierConfiguration(definitionUniqueId);
                configuration.Update(definition);
            }

            return configuration;
        }

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static IBdoConnectorConfiguration CreateConnectorConfiguration(
            this IBdoScope scope,
            string definitionUniqueId,
            IBdoLog log = null)
        {
            IBdoConnectorConfiguration configuration = null;

            IBdoConnectorDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<BdoConnectorDefinition>(definitionUniqueId);
            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionUniqueId + "' of kind '" + BdoExtensionItemKind.Connector.ToString() + "'");
            }
            else
            {
                configuration = BdoExtensionFactory.CreateConnectorConfiguration(definitionUniqueId);
                configuration.Update(definition);
            }

            return configuration;
        }

        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static IBdoEntityConfiguration CreateEntityConfiguration(
            this IBdoScope scope,
            string definitionUniqueId,
            IBdoLog log = null)
        {
            IBdoEntityConfiguration configuration = null;

            IBdoEntityDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<BdoEntityDefinition>(definitionUniqueId);
            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionUniqueId + "' of kind '" + BdoExtensionItemKind.Entity.ToString() + "'");
            }
            else
            {
                configuration = BdoExtensionFactory.CreateEntityConfiguration(definitionUniqueId);
                configuration.Update(definition);
            }

            return configuration;
        }

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static IBdoTaskConfiguration CreateTaskConfiguration(
            this IBdoScope scope,
            string definitionUniqueId,
            IBdoLog log = null)
        {
            IBdoTaskConfiguration configuration = null;

            IBdoTaskDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<BdoTaskDefinition>(definitionUniqueId);
            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionUniqueId + "' of kind '" + BdoExtensionItemKind.Task.ToString() + "'");
            }
            else
            {
                configuration = BdoExtensionFactory.CreateTaskConfiguration(definitionUniqueId);
                configuration.Update(definition);
            }

            return configuration;
        }

        // Load ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="xmlstring">The XML string to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static ITBdoExtensionItemConfiguration<T> LoadConfiguration<T>(
            this IBdoScope scope,
            string xmlstring,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null) where T : IBdoExtensionItemDefinition
        {
            BdoExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();

            ITBdoExtensionItemConfiguration<T> configuration = default;

            switch (extensionItemKind)
            {
                case BdoExtensionItemKind.Carrier:
                    configuration = XmlHelper.LoadFromString<BdoCarrierConfiguration>(xmlstring, scope, scriptVariableSet, log) as ITBdoExtensionItemConfiguration<T>;
                    break;
                case BdoExtensionItemKind.Connector:
                    configuration = XmlHelper.LoadFromString<BdoConnectorConfiguration>(xmlstring, scope, scriptVariableSet, log) as ITBdoExtensionItemConfiguration<T>;
                    break;
                case BdoExtensionItemKind.Entity:
                    configuration = XmlHelper.LoadFromString<BdoEntityConfiguration>(xmlstring, scope, scriptVariableSet, log) as ITBdoExtensionItemConfiguration<T>;
                    break;
                case BdoExtensionItemKind.Format:
                    configuration = XmlHelper.LoadFromString<BdoFormatConfiguration>(xmlstring, scope, scriptVariableSet, log) as ITBdoExtensionItemConfiguration<T>;
                    break;
                case BdoExtensionItemKind.Task:
                    configuration = XmlHelper.LoadFromString<BdoTaskConfiguration>(xmlstring, scope, scriptVariableSet, log) as ITBdoExtensionItemConfiguration<T>;
                    break;
            }

            return configuration;
        }
    }
}
