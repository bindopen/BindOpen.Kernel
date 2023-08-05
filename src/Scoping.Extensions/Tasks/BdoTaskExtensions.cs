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
    public static partial class BdoTaskExtensions
    {
        public static IBdoMetaData AsProperty(
            this IBdoMetaData meta)
        {
            meta?.WithGroupId(null);

            return meta;
        }

        public static IBdoMetaData AsInput(
            this IBdoMetaData meta)
        {
            meta?.WithGroupId(IBdoTaskExtensions.__Token_Input);

            return meta;
        }

        public static IBdoMetaData AsOutput(
            this IBdoMetaData meta)
        {
            meta?.WithGroupId(IBdoTaskExtensions.__Token_Output);

            return meta;
        }

        // Create

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        private static IBdoTask CreateTask(
            this IBdoScope scope,
            IBdoMetaComposite metaSet = null,
            string definitionUniqueName = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoTask task = null;

            if (metaSet != null && scope?.Check(true, log: log) == true)
            {
                // we get the task class reference

                IBdoTaskDefinition definition = scope.ExtensionStore.GetDefinition<IBdoTaskDefinition>(definitionUniqueName);
                if (definition == null)
                {
                    log?.AddEvent(EventKinds.Error, "Could not retrieve the extension task '" + definitionUniqueName + "' definition in scope");
                }
                else
                {
                    // we intantiate the task

                    object item = definition.RuntimeType.CreateInstance(log);

                    if (log?.HasEvent(EventKinds.Error, EventKinds.Exception) != false)
                    {
                        if ((task = item as IBdoTask) != null)
                        {
                            task.DefinitionUniqueName = definition.UniqueName;

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
            var definitionUniqueName = meta.DataType.ClassReference?.DefinitionUniqueName;

            var task = scope.CreateTask(meta, definitionUniqueName, varSet, log);

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
            IBdoMetaComposite metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoTask, new()
        {
            var extensionDefinition = scope.ExtensionStore?.GetDefinitionFromType(
                BdoExtensionKind.Task,
                BdoData.Class(typeof(T)));

            var task = scope.CreateTask(metaSet, extensionDefinition?.UniqueName, varSet, log) as T;

            return task;
        }
    }
}
