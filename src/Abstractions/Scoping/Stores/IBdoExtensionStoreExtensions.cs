using BindOpen.Data.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This static class extends the IBdoExtensionStore interface.
    /// </summary>
    public static class IBdoExtensionStoreExtensions
    {
        /// <summary>
        /// Gets the specified extension definitions.
        /// </summary>
        /// <param name="store">The extension store to consider.</param>
        /// <typeparam name="T">The type to consider.</typeparam>
        /// <returns>Returns the extension definitions of the specified type.</returns>
        public static IEnumerable<T> GetDefinitions<T>(this IBdoExtensionStore store)
            where T : IBdoExtensionDefinition
        {
            if (store != null)
            {
                return store.GetDefinitions(typeof(T).GetExtensionKind()).Cast<T>().Where(q => q is not null);
            }

            return default;
        }

        /// <summary>
        /// Get the specified extension definition.
        /// </summary>
        /// <param name="store">The extension store to consider.</param>
        /// <typeparam name="T">The type to consider.</typeparam>
        /// <returns>Returns the extension definitions of the specified type and the specified unique name.</returns>
        public static T GetDefinition<T>(
            this IBdoExtensionStore store,
            string uniqueName) where T : IBdoExtensionDefinition
        {
            if (store != null)
            {
                return store.GetDefinition(typeof(T).GetExtensionKind(), uniqueName).As<T>();
            }

            return default;
        }

        /// <summary>
        /// Get the specified extension definition.
        /// </summary>
        /// <param name="store">The extension store to consider.</param>
        /// <typeparam name="T">The type to consider.</typeparam>
        /// <returns>Returns the extension definitions of the specified type.</returns>
        public static T GetDefinitionFromType<T>(this IBdoExtensionStore store) where T : IBdoExtension
        {
            if (store != null)
            {
                return store.GetDefinitionFromType(typeof(T)).As<T>();
            }

            return default;
        }
    }
}