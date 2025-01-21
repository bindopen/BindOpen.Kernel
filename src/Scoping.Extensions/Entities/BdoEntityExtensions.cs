using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Logging;
using BindOpen.Scoping.Entities;
using System;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static partial class BdoEntityExtensions
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
            IBdoDataType dataType,
            IBdoMetaSet metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoEntity entity = null;

            if (metaSet != null && scope?.Check(true, log: log) == true)
            {
                Type type = dataType?.GetRuntimeType(scope, log);

                // we get the entity class reference

                if (type != null)
                {
                    object item = type.CreateInstance(log);

                    if (log?.HasEvent(BdoEventKinds.Error, BdoEventKinds.Exception) != false)
                    {
                        if ((entity = item as IBdoEntity) != null)
                        {
                            entity.DefinitionUniqueName = dataType?.DefinitionUniqueName;
                            entity.DefinitionUniqueName ??= scope.ExtensionStore?.GetDefinitionFromType(type)?.UniqueName;

                            entity.UpdateFromMeta(metaSet, true, scope: scope, varSet: varSet);
                        }
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
            IBdoMetaNode meta,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var entity = scope.CreateEntity(meta?.DataType, meta, varSet, log);

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
            IBdoMetaSet metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoEntity, new()
        {
            var dataType = BdoData.NewDataType<T>();

            var entity = scope.CreateEntity(dataType, metaSet, varSet, log) as T;

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
            string definitionUniqueName,
            IBdoMetaSet metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var dataType = BdoData.NewDataType(BdoExtensionKinds.Entity, definitionUniqueName);

            var entity = scope.CreateEntity(dataType, metaSet, varSet, log);

            return entity;
        }
    }
}
