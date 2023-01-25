using BindOpen.Extensions.Processing;
using BindOpen.Data.Meta;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoExt
    {
        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <typeparam name="T">The task class to return.</typeparam>
        /// <param name="config"></param>
        /// <returns>Returns the created connector.</returns>
        public static T NewTask<T>(IBdoTaskConfiguration config)
            where T : class, IBdoTask, new()
        {
            T task = new();
            task.WithConfig(config);

            return task;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoTaskConfiguration NewTaskConfig(
            string definitionUniqueId,
            params IBdoMetaData[] items)
        {
            var config = new BdoTaskConfiguration(definitionUniqueId);
            config.WithItems(items);

            return config;
        }
    }
}
