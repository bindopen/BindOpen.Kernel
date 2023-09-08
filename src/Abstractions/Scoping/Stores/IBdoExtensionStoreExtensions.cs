using BindOpen.Kernel.Data.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    public static class IBdoExtensionStoreExtensions
    {
        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <returns>The item words of specified library names.</returns>
        public static IEnumerable<T> GetDefinitions<T>(this IBdoExtensionStore store) where T : IBdoExtensionDefinition
        {
            if (store != null)
            {
                return store.GetDefinitions(typeof(T).GetExtensionKind()).Cast<T>().Where(q => q is not null);
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="uniqueName"></param>
        /// <returns></returns>
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