using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Formats;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This static class provides methods to create extension DTO item.
    /// </summary>
    public static class AppExtensionItemDtoFactory
    {
        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static ITAppExtensionItemConfiguration<T> CreateConfiguration<T>(
            this IAppScope appScope,
            string definitionUniqueId,
            ILog log = null) where T : IAppExtensionItemDefinition
        {
            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();

            T definition = appScope.Extension.GetItemDefinitionWithUniqueId<T>(definitionUniqueId);

            ITAppExtensionItemConfiguration<T> configuration = null;

            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionUniqueId + "' of kind '" + extensionItemKind.ToString() + "'");
            }
            else
            {
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Carrier:
                        configuration = new CarrierConfiguration(definitionUniqueId) as ITAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Connector:
                        configuration = new ConnectorConfiguration(definitionUniqueId) as ITAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Entity:
                        configuration = new EntityConfiguration(definitionUniqueId) as ITAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Format:
                        configuration = new FormatConfiguration(definitionUniqueId) as ITAppExtensionItemConfiguration<T>;
                        break;
                    //case AppExtensionItemKind.Metrics:
                    //    configuration = new MetricsDto(definitionUniqueId) as ITAppExtensionItemDto<T>;
                    //    break;
                    //case AppExtensionItemKind.Routine:
                    //    configuration = new RoutineConfiguration(definitionUniqueId) as ITAppExtensionItemDto<T>;
                    //    break;
                    case AppExtensionItemKind.Task:
                        configuration = new TaskConfiguration(definitionUniqueId) as ITAppExtensionItemConfiguration<T>;
                        break;
                }
            }

            if (configuration!=null)
            {
                configuration.DefinitionUniqueId = definitionUniqueId;
                configuration.Update(definition);
            }

            return configuration;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="xmlstring">The XML string to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static ITAppExtensionItemConfiguration<T> LoadConfiguration<T>(
            this IAppScope appScope,
            string xmlstring,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null) where T : IAppExtensionItemDefinition
        {
            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();

            ITAppExtensionItemConfiguration<T> configuration = default;

            switch (extensionItemKind)
            {
                case AppExtensionItemKind.Carrier:
                    configuration = XmlHelper.LoadFromString<CarrierConfiguration>(xmlstring, appScope, scriptVariableSet, log) as ITAppExtensionItemConfiguration<T>;
                    break;
                case AppExtensionItemKind.Connector:
                    configuration = XmlHelper.LoadFromString<ConnectorConfiguration>(xmlstring, appScope, scriptVariableSet, log) as ITAppExtensionItemConfiguration<T>;
                    break;
                case AppExtensionItemKind.Entity:
                    configuration = XmlHelper.LoadFromString<EntityConfiguration>(xmlstring, appScope, scriptVariableSet, log) as ITAppExtensionItemConfiguration<T>;
                    break;
                case AppExtensionItemKind.Format:
                    configuration = XmlHelper.LoadFromString<FormatConfiguration>(xmlstring, appScope, scriptVariableSet, log) as ITAppExtensionItemConfiguration<T>;
                    break;
                //case AppExtensionItemKind.Metrics:
                //    configuration = XmlHelper.LoadFromString<MetricsDto>(xmlstring, log) as ITAppExtensionItemDto<T>;
                //    break;
                //case AppExtensionItemKind.Routine:
                //    configuration = XmlHelper.LoadFromString<RoutineConfiguration>(xmlstring, log) as ITAppExtensionItemDto<T>;
                //    break;
                case AppExtensionItemKind.Task:
                    configuration = XmlHelper.LoadFromString<CarrierConfiguration>(xmlstring, appScope, scriptVariableSet, log) as ITAppExtensionItemConfiguration<T>;
                    break;
            }

            return configuration;
        }
    }
}
