using System;
using System.Dynamic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Configuration.Connectors
{
    /// <summary>
    /// This class represents a connector configuration factory.
    /// </summary>
    public static class ConnectorConfigurationFactory
    {
        /// <summary>
        /// Creates a connector of the specified unique name.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="name">The name of connector to create.</param>
        /// <param name="definitionName">The unique name of connector to create.</param>
        /// <param name="connectionString">The connection string of this instance.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public static ConnectorConfiguration Create(
            IAppScope appScope,
            String name,
            String definitionName,
            String connectionString = null,
            Log log = null)
        {
            log = (log ?? new Log());

            if (!log.Append(appScope.Check()).HasErrorsOrExceptions())
                return appScope.AppExtension.CreateConfiguration<ConnectorDefinition>(
                    definitionName,
                    null, log,
                    name, connectionString, null, appScope, log) as ConnectorConfiguration;

            return null;
        }

        /// <summary>
        /// Creates a connector of the specified unique name.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="name">The name of connector to create.</param>
        /// <param name="definitionName">The unique name of connection to create.</param>
        /// <param name="dynamicObject">The object parameters to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public static ConnectorConfiguration Create(
            IAppScope appScope,
            String name,
            String definitionName,
            DynamicObject dynamicObject,
            Log log = null)
        {
            return ConnectorConfigurationFactory.Create(appScope, name, definitionName, DataElementSet.Create(dynamicObject), log);
        }

        /// <summary>
        /// Creates a connector of the specified unique name.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="name">The name of connector to create.</param>
        /// <param name="definitionName">The unique name of connection to create.</param>
        /// <param name="detail">The detail to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public static ConnectorConfiguration Create(
            IAppScope appScope,
            String name,
            String definitionName,
            DataElementSet detail = null,
            Log log = null)
        {
            log = (log ?? new Log());

            if (!log.Append(appScope.Check()).HasErrorsOrExceptions())
            {
                return appScope.AppExtension.CreateConfiguration<ConnectorDefinition>(
                   definitionName,
                   null, log,
                   name, null, detail, appScope, log) as ConnectorConfiguration;
            }

            return null;
        }
    }
}
