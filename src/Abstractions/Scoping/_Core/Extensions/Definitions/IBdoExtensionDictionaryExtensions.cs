using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoExtensionDictionaryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithGroups<T>(
            this T obj,
            params IBdoExtensionGroup[] groups)
            where T : IBdoExtensionDictionary
        {
            if (obj != null)
            {
                obj.Groups = groups?.ToList();
            }
            return obj;
        }

        public static T AddGroups<T>(
            this T obj,
            params IBdoExtensionGroup[] groups)
            where T : IBdoExtensionDictionary
        {
            if (obj != null)
            {
                obj.Groups ??= new List<IBdoExtensionGroup>(groups);
            }
            return obj;
        }

        public static T WithLibraryId<T>(
            this T obj,
            string libraryId)
            where T : IBdoExtensionDictionary
        {
            if (obj != null)
            {
                obj.LibraryId = libraryId;
            }
            return obj;
        }

        public static T WithLibraryName<T>(
            this T obj,
            string libraryName)
            where T : IBdoExtensionDictionary
        {
            if (obj != null)
            {
                obj.LibraryName = libraryName;
            }
            return obj;
        }
    }
}