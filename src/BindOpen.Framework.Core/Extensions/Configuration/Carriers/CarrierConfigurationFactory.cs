using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Dynamic;

namespace BindOpen.Framework.Core.Extensions.Configuration.Carriers
{
    /// <summary>
    /// This class represents a carrier configuration factory.
    /// </summary>
    public static class CarrierConfigurationFactory
    {

        /// <summary>
        /// Creates a connector of the specified unique name.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="name">The name of connector to create.</param>
        /// <param name="carrierUniqueName">The unique name of connector to create.</param>
        /// <param name="path">The path to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        /// <param name="relativePath">The relative path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public static CarrierConfiguration CreateConfiguration(
            AppScope appScope,
            String name,
            String carrierUniqueName,
            String path,
            DataElementSet detail,
            String relativePath,
            Log log = null)
        {
            if (!log.Append(appScope.Check()).HasErrorsOrExceptions())
                return appScope.AppExtension.CreateConfiguration<CarrierDefinition>(
                    carrierUniqueName, null, log,
                    name, path, detail, relativePath) as CarrierConfiguration;

            return null;
        }

        /// <summary>
        /// Creates a connector of the specified unique name.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="name">The name of connector to create.</param>
        /// <param name="carrierUniqueName">The unique name of connection to create.</param>
        /// <param name="path">The path to consider.</param>
        /// <param name="pathDynamicObject">The path dynamic object to consider.</param>
        /// <param name="relativePath">The relative path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public static CarrierConfiguration CreateConfiguration(
            AppScope appScope,
            String name, String carrierUniqueName,
            String path, DynamicObject pathDynamicObject, String relativePath,
            Log log = null)
        {
            return CarrierConfigurationFactory.CreateConfiguration(
                appScope, name, carrierUniqueName, path, DataElementSet.Create(pathDynamicObject), relativePath, log);
        }

    }
}
