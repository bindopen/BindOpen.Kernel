using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// This static class provides methods to create extension DTO item.
    /// </summary>
    public static class BdoExtensionItemConfigurationFactory
    {
        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static ITBdoExtensionItemConfiguration<T> CreateConfiguration<T>(
            this IBdoScope scope,
            string definitionUniqueId,
            IBdoLog log = null) where T : IBdoExtensionItemDefinition
        {
            BdoExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();

            T definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<T>(definitionUniqueId);

            ITBdoExtensionItemConfiguration<T> configuration = null;

            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionUniqueId + "' of kind '" + extensionItemKind.ToString() + "'");
            }
            else
            {
                switch (extensionItemKind)
                {
                    case BdoExtensionItemKind.Carrier:
                        configuration = new BdoCarrierConfiguration(definitionUniqueId) as ITBdoExtensionItemConfiguration<T>;
                        break;
                    case BdoExtensionItemKind.Connector:
                        configuration = new BdoConnectorConfiguration(definitionUniqueId) as ITBdoExtensionItemConfiguration<T>;
                        break;
                    case BdoExtensionItemKind.Entity:
                        configuration = new BdoEntityConfiguration(definitionUniqueId) as ITBdoExtensionItemConfiguration<T>;
                        break;
                    case BdoExtensionItemKind.Format:
                        configuration = new BdoFormatConfiguration(definitionUniqueId) as ITBdoExtensionItemConfiguration<T>;
                        break;
                    case BdoExtensionItemKind.Task:
                        configuration = new BdoTaskConfiguration(definitionUniqueId) as ITBdoExtensionItemConfiguration<T>;
                        break;
                }
            }

            if (configuration != null)
            {
                configuration.DefinitionUniqueId = definitionUniqueId;
                configuration.Update(definition);
            }

            return configuration;
        }

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
            IBdoScriptVariableSet scriptVariableSet = null,
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
                    configuration = XmlHelper.LoadFromString<BdoCarrierConfiguration>(xmlstring, scope, scriptVariableSet, log) as ITBdoExtensionItemConfiguration<T>;
                    break;
            }

            return configuration;
        }
    }
}
