using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Formats;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Factories
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
        public static ITAppExtensionItemDto<T> CreateConfiguration<T>(
            this IAppScope appScope,
            string definitionUniqueId,
            ILog log = null) where T : IAppExtensionItemDefinition
        {
            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();

            T definition = appScope.AppExtension.GetItemDefinitionWithUniqueId<T>(definitionUniqueId);

            ITAppExtensionItemDto<T> configuration = null;

            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionUniqueId + "' of kind '" + extensionItemKind.ToString() + "'");
            }
            else
            {
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Carrier:
                        configuration = new CarrierDto(definitionUniqueId) as ITAppExtensionItemDto<T>;
                        break;
                    case AppExtensionItemKind.Connector:
                        configuration = new ConnectorDto(definitionUniqueId) as ITAppExtensionItemDto<T>;
                        break;
                    case AppExtensionItemKind.Entity:
                        configuration = new EntityDto(definitionUniqueId) as ITAppExtensionItemDto<T>;
                        break;
                    case AppExtensionItemKind.Format:
                        configuration = new FormatDto(definitionUniqueId) as ITAppExtensionItemDto<T>;
                        break;
                    //case AppExtensionItemKind.Metrics:
                    //    configuration = new MetricsDto(definitionUniqueId) as ITAppExtensionItemDto<T>;
                    //    break;
                    //case AppExtensionItemKind.Routine:
                    //    configuration = new RoutineDto(definitionUniqueId) as ITAppExtensionItemDto<T>;
                    //    break;
                    case AppExtensionItemKind.Task:
                        configuration = new TaskDto(definitionUniqueId) as ITAppExtensionItemDto<T>;
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
        public static ITAppExtensionItemDto<T> LoadConfiguration<T>(
            this IAppScope appScope,
            string xmlstring,
            ILog log = null) where T : IAppExtensionItemDefinition
        {
            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();

            ITAppExtensionItemDto<T> configuration = default;

            switch (extensionItemKind)
            {
                case AppExtensionItemKind.Carrier:
                    configuration = XmlHelper.LoadFromString<CarrierDto>(xmlstring, log) as ITAppExtensionItemDto<T>;
                    break;
                case AppExtensionItemKind.Connector:
                    configuration = XmlHelper.LoadFromString<ConnectorDto>(xmlstring, log) as ITAppExtensionItemDto<T>;
                    break;
                case AppExtensionItemKind.Entity:
                    configuration = XmlHelper.LoadFromString<EntityDto>(xmlstring, log) as ITAppExtensionItemDto<T>;
                    break;
                case AppExtensionItemKind.Format:
                    configuration = XmlHelper.LoadFromString<FormatDto>(xmlstring, log) as ITAppExtensionItemDto<T>;
                    break;
                //case AppExtensionItemKind.Metrics:
                //    configuration = XmlHelper.LoadFromString<MetricsDto>(xmlstring, log) as ITAppExtensionItemDto<T>;
                //    break;
                //case AppExtensionItemKind.Routine:
                //    configuration = XmlHelper.LoadFromString<RoutineDto>(xmlstring, log) as ITAppExtensionItemDto<T>;
                //    break;
                case AppExtensionItemKind.Task:
                    configuration = XmlHelper.LoadFromString<CarrierDto>(xmlstring, log) as ITAppExtensionItemDto<T>;
                    break;
            }

            return configuration;
        }
    }
}
