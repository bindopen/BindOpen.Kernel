using BindOpen.Data;
using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Extensions;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Logging;
using BindOpen.Runtime.Definition;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public partial class BdoScope : BdoItem, IBdoScope
    {
        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created entity.</returns>
        public IBdoEntity CreateEntity(
            IBdoConfiguration config,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            IBdoEntity entity = null;

            if (config != null && this.Check(true, log: log) == false)
            {
                // we get the entity class reference

                IBdoEntityDefinition definition = ExtensionStore.GetItemDefinitionWithUniqueName<IBdoEntityDefinition>(config.DefinitionUniqueName);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension entity '" + config.DefinitionUniqueName + "' definition");
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
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public IBdoConnector CreateConnector(
            IBdoConfiguration config,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            IBdoConnector connector = null;

            if (config != null && this.Check(true, log: log) == true)
            {
                // we get the connector class reference

                IBdoConnectorDefinition definition = ExtensionStore.GetItemDefinitionWithUniqueName<IBdoConnectorDefinition>(config?.DefinitionUniqueName);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension connector '" + config.DefinitionUniqueName + "' definition");
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
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        public IBdoTask CreateTask(
            IBdoConfiguration config = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            IBdoTask task = null;

            if (config != null && this.Check(true, log: log) == true)
            {
                // we get the task class reference

                IBdoTaskDefinition definition = ExtensionStore.GetItemDefinitionWithUniqueName<IBdoTaskDefinition>(config?.DefinitionUniqueName);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension task '" + config.DefinitionUniqueName + "' definition");
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
                        //task.UpdateFromMetaList<BdoTaskInputAttribute>(config, scope, varSet);
                        //task.UpdateFromMetaList<BdoTaskOutputAttribute>(config, scope, varSet);
                    }
                }
            }

            return task;
        }

        // Handlers -------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataHandlerUniqueName"></param>
        /// <param name="obj"></param>
        /// <param name="pathDetail"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public object GetData(
            string dataHandlerUniqueName,
            object obj, IBdoMetaList pathDetail,
            IBdoMetaList varSet, IBdoLog log)
        {
            var definition = ExtensionStore?.GetItemDefinitionWithUniqueName<BdoHandlerDefinition>(
                dataHandlerUniqueName);

            if (definition == null)
            {
                log?.AddError(title: "Definition not found");
            }
            else if (definition.RuntimeGetFunction == null)
            {
                log?.AddError(title: "Data handler Get function missing");
            }
            else
            {
                definition.RuntimeGetFunction(obj, pathDetail, this, varSet, log);
            }

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataHandlerUniqueName"></param>
        /// <param name="obj"></param>
        /// <param name="pathDetail"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public object PostData(
            string dataHandlerUniqueName,
            object obj, IBdoMetaList pathDetail,
            IBdoMetaList varSet, IBdoLog log)
        {
            var definition = ExtensionStore?.GetItemDefinitionWithUniqueName<BdoHandlerDefinition>(
                dataHandlerUniqueName);

            if (definition == null)
            {
                log?.AddError(title: "Definition not found");
            }
            else if (definition.RuntimePostFunction == null)
            {
                log?.AddError(title: "Data handler Post function missing");
            }
            else
            {
                definition.RuntimePostFunction(obj, pathDetail, this, varSet, log);
            }

            return obj;
        }
    }
}
