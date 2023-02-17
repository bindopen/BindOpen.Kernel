using BindOpen.Data;
using BindOpen.Data.Configuration;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Extensions.Scripting;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class Bdo
    {
        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <param name="config"></param>
        /// <returns>Returns the created connector.</returns>
        public static T NewConnector<T>(
            IBdoConfiguration config)
            where T : class, IBdoConnector, new()
        {
            T connector = new();
            connector.WithConfig(config);
            connector.UpdateFromMeta(config);
            connector.DefinitionUniqueName = config?.DefinitionUniqueName;

            return connector;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <param name="config"></param>
        /// <returns>Returns the created connector.</returns>
        public static T NewEntity<T>(
            IBdoConfiguration config)
            where T : class, IBdoEntity, new()
        {
            T entity = new();
            entity.WithConfig(config);
            entity.UpdateFromMeta(config);
            entity.DefinitionUniqueName = config?.DefinitionUniqueName;

            return entity;
        }


        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <typeparam name="T">The task class to return.</typeparam>
        /// <param name="config"></param>
        /// <returns>Returns the created connector.</returns>
        public static T NewTask<T>(
            IBdoConfiguration config)
            where T : class, IBdoTask, new()
        {
            T task = new();
            task.WithConfig(config);
            task.UpdateFromMeta(config);
            task.DefinitionUniqueName = config?.DefinitionUniqueName;

            return task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IBdoScriptword NewScriptword(IBdoConfiguration config)
        {
            BdoScriptword scriptword = new();
            scriptword.WithConfig(config);
            scriptword.UpdateFromMeta(config);
            scriptword.DefinitionUniqueName = config?.DefinitionUniqueName;

            return scriptword;
        }

    }
}
