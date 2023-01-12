using BindOpen.Extensions.Processing;
using BindOpen.Meta.Elements;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoExtensions
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
            task.WithConfiguration(config);

            return task;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoTaskConfiguration NewTaskConfiguration(
            string definitionUniqueId,
            params IBdoMetaElement[] items)
        {
            var config = new BdoTaskConfiguration(definitionUniqueId);
            config.WithItems(items);

            return config;
        }
    }
}
