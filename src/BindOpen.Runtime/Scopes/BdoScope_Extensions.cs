using BindOpen.Data.Configuration;
using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Extensions;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Extensions.Scripting;
using BindOpen.Logging;
using BindOpen.Runtime.Definitions;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public partial class BdoScope : BdoItem, IBdoScope
    {
        // Functions ------------------------------------------------

        public object CallFunction(
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var objs = config.GetDataList(this, varSet, log)?.ToArray();
            return CallFunction(config.DefinitionUniqueName, objs, varSet, log);
        }

        public object CallFunction(
            string functionUniqueName,
            object[] objs,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var def = ExtensionStore?.GetDefinition(
                BdoExtensionKind.Function,
                functionUniqueName);

            if (def is not IBdoFunctionDefinition funcDef)
            {
                log?.AddError(string.Format("Function ('{0}') not found", functionUniqueName));
                return null;
            }

            var result = funcDef.RuntimeFunction.DynamicInvoke(objs);

            return result;
        }

        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created entity.</returns>
        public IBdoEntity CreateEntity(
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoEntity entity = null;

            if (config != null && this.Check(true, log: log) == false)
            {
                // we get the entity class reference

                IBdoEntityDefinition definition = ExtensionStore.GetDefinition<IBdoEntityDefinition>(config.DefinitionUniqueName);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension entity '" + config.DefinitionUniqueName + "' definition in scope");
                }
                else
                {
                    // we intantiate the entity

                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if ((entity = item as IBdoEntity) != null)
                    {
                        entity.UpdateFromMeta(config, true, this, varSet);
                    }
                }
            }

            return entity;
        }

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public IBdoConnector CreateConnector(
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoConnector connector = null;

            if (config != null && this.Check(true, log: log) == true)
            {
                // we get the connector class reference

                IBdoConnectorDefinition definition = ExtensionStore.GetDefinition<IBdoConnectorDefinition>(config?.DefinitionUniqueName);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension connector '" + config.DefinitionUniqueName + "' definitio in scope");
                }
                else
                {
                    // we intantiate the connector
                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if ((connector = item as IBdoConnector) != null)
                    {
                        connector.UpdateFromMeta(config, true, this, varSet);
                    }
                }
            }

            return null;
        }

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        public IBdoTask CreateTask(
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoTask task = null;

            if (config != null && this.Check(true, log: log) == true)
            {
                // we get the task class reference

                IBdoTaskDefinition definition = ExtensionStore.GetDefinition<IBdoTaskDefinition>(config?.DefinitionUniqueName);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension task '" + config.DefinitionUniqueName + "' definition in scope");
                }
                else
                {
                    // we intantiate the task

                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if (log?.HasEvent(EventKinds.Error, EventKinds.Exception) != false)
                    {
                        if ((task = item as IBdoTask) != null)
                        {
                            task.UpdateFromMeta(config, true, this, varSet);
                        }
                        //task.UpdateFromMetaSet<BdoInputAttribute>(config, scope, varSet);
                        //task.UpdateFromMetaSet<BdoOutputAttribute>(config, scope, varSet);
                    }
                }
            }

            return task;
        }

        // Script ------------------------------------------------

        /// <summary>
        /// Creates a new script interpreter.
        /// </summary>
        /// <returns>Returns the new script interpreter.</returns>
        public IBdoScriptInterpreter CreateInterpreter()
            => BdoScript.CreateInterpreter(this);
    }
}
