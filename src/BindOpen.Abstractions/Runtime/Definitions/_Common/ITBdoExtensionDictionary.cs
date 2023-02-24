using System.Collections.Generic;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoExtensionDictionary<T> : IBdoExtensionDictionary
        where T : IBdoExtensionDefinition
    {
        /// <summary>
        /// The definitions.
        /// </summary>
        List<T> Definitions { get; set; }

        ITBdoExtensionDictionary<T> AddDefinitions(params T[] definitions);

        ITBdoExtensionDictionary<T> WithDefinitions(params T[] definitions);

        /// <summary>
        /// The groups.
        /// </summary>
        List<IBdoExtensionGroup> Groups { get; }

        ITBdoExtensionDictionary<T> WithGroups(params IBdoExtensionGroup[] groups);

        ITBdoExtensionDictionary<T> AddGroups(params IBdoExtensionGroup[] groups);

        /// <summary>
        /// ID of library.
        /// </summary>
        string LibraryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="libraryId"></param>
        /// <returns></returns>
        ITBdoExtensionDictionary<T> WithLibraryId(string libraryId);

        /// <summary>
        /// Name of library.
        /// </summary>
        string LibraryName { get; set; }

        ITBdoExtensionDictionary<T> WithLibraryName(string libraryName);
    }
}