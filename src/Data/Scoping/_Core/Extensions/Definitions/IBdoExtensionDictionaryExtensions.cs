using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides extension methods for <see cref="IBdoExtensionDictionary"/>.
/// </summary>
public static class IBdoExtensionDictionaryExtensions
{
    /// <summary>
    /// Sets the groups of the specified dictionary, replacing any existing ones.
    /// </summary>
    /// <param name="groups">The extension groups to set.</param>
    public static T WithGroups<T>(
        this T obj,
        params IBdoExtensionGroup[] groups)
        where T : IBdoExtensionDictionary
    {
        if (obj != null)
        {
            obj.Groups = groups?.ToList() ?? new List<IBdoExtensionGroup>();
        }
        return obj;
    }

    /// <summary>
    /// Adds the specified groups to the dictionary's existing groups.
    /// </summary>
    /// <param name="groups">The extension groups to add.</param>
    public static T AddGroups<T>(
        this T obj,
        params IBdoExtensionGroup[] groups)
        where T : IBdoExtensionDictionary
    {
        if (obj != null && groups != null)
        {
            obj.Groups ??= new List<IBdoExtensionGroup>();
            foreach (var group in groups)
            {
                obj.Groups.Add(group);
            }
        }
        return obj;
    }

    /// <summary>
    /// Sets the library ID for the specified dictionary.
    /// </summary>
    /// <param name="libraryId">The library ID to set.</param>
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

    /// <summary>
    /// Sets the library name for the specified dictionary.
    /// </summary>
    /// <param name="libraryName">The library name to set.</param>
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