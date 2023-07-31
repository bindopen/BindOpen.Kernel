﻿using BindOpen.System.Data.Assemblies;
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
                            task.UpdateFromMeta<BdoInputAttribute>(config, true, IBdoTaskExtensions.__Token_Input, scope: scope, varSet: varSet);
                            task.UpdateFromMeta<BdoOutputAttribute>(config, true, IBdoTaskExtensions.__Token_Output, scope: scope, varSet: varSet);
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
    }
}
