using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Assemblies;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Data.Meta.Reflection;
using BindOpen.Kernel.Logging;
using System;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static partial class BdoTaskExtensions
    {
        // Create

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        public static IBdoTask CreateTask(
            this IBdoScope scope,
            IBdoDataType dataType,
            IBdoMetaSet metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoTask task = null;

            if (metaSet != null && scope?.Check(true, log: log) == true)
            {
                Type type = dataType?.GetRuntimeType(scope, log);

                // we get the task class reference

                if (type != null)
                {
                    object item = type.CreateInstance(log);

                    if (log?.HasEvent(EventKinds.Error, EventKinds.Exception) != false)
                    {
                        if ((task = item as IBdoTask) != null)
                        {
                            task.DefinitionUniqueName = dataType?.DefinitionUniqueName;
                            task.DefinitionUniqueName ??= scope.ExtensionStore?.GetDefinitionFromType(type)?.UniqueName;

                            task.UpdateFromMeta(metaSet, true, null, scope: scope, varSet: varSet);
                            task.UpdateFromMeta<BdoInputAttribute>(metaSet, true, IBdoTaskExtensions.__Token_Input, scope: scope, varSet: varSet);
                            task.UpdateFromMeta<BdoOutputAttribute>(metaSet, true, IBdoTaskExtensions.__Token_Output, scope: scope, varSet: varSet);
                        }
                    }
                }
            }

            return task;
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
        public static IBdoTask CreateTask(
            this IBdoScope scope,
            IBdoMetaObject meta,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var task = scope.CreateTask(meta?.DataType, meta, varSet, log);

            return task;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The task class to return.</typeparam>
        /// <returns>Returns the created task.</returns>
        public static T CreateTask<T>(
            this IBdoScope scope,
            IBdoMetaSet metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoTask, new()
        {
            var dataType = BdoData.NewDataType<T>();

            var task = scope.CreateTask(dataType, metaSet, varSet, log) as T;

            return task;
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
        public static IBdoTask CreateTask(
            this IBdoScope scope,
            string definitionUniqueName,
            IBdoMetaSet metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var dataType = BdoData.NewDataType(BdoExtensionKinds.Task, definitionUniqueName);

            var task = scope.CreateTask(dataType, metaSet, varSet, log);

            return task;
        }
    }
}
