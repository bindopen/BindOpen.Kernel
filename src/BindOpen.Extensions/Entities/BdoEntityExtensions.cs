using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Logging;
using BindOpen.Scopes;

namespace BindOpen.Extensions.Entities
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoEntityExtensions
    {
        // Create

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created entity.</returns>
        public static IBdoEntity CreateEntity(
            this IBdoScope scope,
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoEntity entity = null;

            if (config != null && scope?.Check(true, log: log) == true)
            {
                // we get the entity class reference

                IBdoEntityDefinition definition = scope.ExtensionStore.GetDefinition<IBdoEntityDefinition>(config.DefinitionUniqueName);
                if (definition == null)
                {
                    log?.AddEvent(EventKinds.Error, "Could not retrieve the extension entity '" + config.DefinitionUniqueName + "' definition in scope");
                }
                else
                {
                    // we intantiate the entity

                    object item = definition.RuntimeType.CreateInstance(log);

                    if ((entity = item as IBdoEntity) != null)
                    {
                        entity.DefinitionUniqueName = definition.UniqueName;
                        entity.UpdateFromMeta(config, true, scope: scope, varSet: varSet);
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The entity class to return.</typeparam>
        /// <returns>Returns the created entity.</returns>
        public static T CreateEntity<T>(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : BdoEntity
        {
            return scope.CreateEntity(config, varSet, log) as T;
        }
    }
}
