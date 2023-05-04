using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Logging;
using BindOpen.Scopes;
using System.Collections.Generic;

namespace BindOpen.Extensions.Tasks
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoTaskExtensions
    {
        public static IBdoMetaData AsInput(
            this IBdoMetaData meta)
        {
            if (meta != null)
            {
                meta.WithGroupId(IBdoTaskExtensions.__Token_Input);
            }

            return meta;
        }

        public static IBdoMetaData AsOutput(
            this IBdoMetaData meta)
        {
            if (meta != null)
            {
                meta.WithGroupId(IBdoTaskExtensions.__Token_Output);
            }

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
        public static IBdoTask CreateTask(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoTask task = null;

            if (config != null && scope?.Check(true, log: log) == true)
            {
                // we get the task class reference

                IBdoTaskDefinition definition = scope.ExtensionStore.GetDefinition<IBdoTaskDefinition>(config?.DefinitionUniqueName);
                if (definition == null)
                {
                    log?.AddEvent(EventKinds.Error, "Could not retrieve the extension task '" + config.DefinitionUniqueName + "' definition in scope");
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

                            task.UpdateFromMeta(config, true, null, scope: scope, varSet: varSet);
                            task.UpdateFromMeta<BdoInputAttribute>(config, true, "input", scope: scope, varSet: varSet);
                            task.UpdateFromMeta<BdoOutputAttribute>(config, true, "output", scope: scope, varSet: varSet);
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
        /// <typeparam name="T">The entity class to return.</typeparam>
        /// <returns>Returns the created entity.</returns>
        public static T CreateTask<T>(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : BdoTask
        {
            return scope.CreateTask(config, varSet, log) as T;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoTaskConfiguration NewTaskConfig(string name = null)
        {
            return new BdoTaskConfiguration()
                .WithName(name);
        }
    }
}
