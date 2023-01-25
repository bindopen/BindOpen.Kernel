using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Modeling;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create extension elms.
    /// </summary>
    public static partial class BdoExt
    {
        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <param name="config"></param>
        /// <returns>Returns the created connector.</returns>
        public static T NewEntity<T>(
            IBdoEntityConfiguration config)
            where T : class, IBdoEntity, new()
        {
            T entity = new();
            entity.WithConfig(config);
            entity.UpdateFromElementSet<BdoDataAttribute>(config);

            return entity;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoEntityConfiguration NewEntityConfig(
            string definitionUniqueId,
            params IBdoMetaData[] elms)
        {
            var config = new BdoEntityConfiguration(definitionUniqueId);
            config.WithItems(elms);

            return config;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoEntityConfiguration NewEntityConfig(
            params IBdoMetaData[] elms)
            => NewEntityConfig(null, elms);
    }
}
