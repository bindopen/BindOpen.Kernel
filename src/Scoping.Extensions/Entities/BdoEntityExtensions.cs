using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Logging;

namespace BindOpen.System.Scoping
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
        private static IBdoEntity CreateEntity(
            this IBdoScope scope,
            IBdoMetaComposite metaSet = null,
            string definitionUniqueName = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoEntity entity = null;

            if (metaSet != null && scope?.Check(true, log: log) == true)
            {
                // we get the entity class reference

                IBdoEntityDefinition definition = scope.ExtensionStore.GetDefinition<IBdoEntityDefinition>(definitionUniqueName);
                if (definition == null)
                {
                    log?.AddEvent(EventKinds.Error, "Could not retrieve the extension entity '" + definitionUniqueName + "' definition in scope");
                }
                else
                {
                    // we intantiate the entity

                    object item = definition.RuntimeType.CreateInstance(log);

                    if ((entity = item as IBdoEntity) != null)
                    {
                        entity.DefinitionUniqueName = definition.UniqueName;
                        entity.UpdateFromMeta(metaSet, true, scope: scope, varSet: varSet);
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
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static IBdoEntity CreateEntity(
            this IBdoScope scope,
            IBdoMetaObject meta,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var definitionUniqueName = meta.DataType.ClassReference?.DefinitionUniqueName;

            var entity = scope.CreateEntity(meta, definitionUniqueName, varSet, log);

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
            IBdoMetaComposite metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoEntity, new()
        {
            var extensionDefinition = scope.ExtensionStore?.GetDefinitionFromType(
                BdoExtensionKind.Entity,
                BdoData.Class(typeof(T)));

            var entity = scope.CreateEntity(metaSet, extensionDefinition?.UniqueName, varSet, log) as T;

            return entity;
        }
    }
}
