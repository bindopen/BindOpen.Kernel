using BindOpen.Data.Meta;

namespace BindOpen.Data;

/// <summary>
/// This class provides extension methods for IBdoMetaData to facilitate 
/// fluent data population and schema-based group validation.
/// </summary>
public static partial class IBdoMetaDataExtensions
{
    /// <summary>
    /// Sets the data of the specified metadata element and returns it.
    /// </summary>
    /// <typeparam name="T">The type of metadata.</typeparam>
    /// <param name="meta">The metadata element to update.</param>
    /// <param name="obj">The data object to assign.</param>
    /// <returns>Returns the metadata instance for fluent chaining.</returns>
    public static T WithData<T>(
        this T meta,
        object obj)
        where T : IBdoMetaData
    {
        meta?.SetData(obj);

        return meta;
    }

    /// <summary>
    /// Determines whether the metadata element belongs to the specified group 
    /// based on its schema.
    /// </summary>
    /// <param name="meta">The metadata element to check.</param>
    /// <param name="groupId">The group identifier to match.</param>
    /// <returns>True if the metadata belongs to the group or has no schema; otherwise, false.</returns>
    public static bool OfGroup(
        this IBdoMetaData meta,
        string groupId)
    {
        // A metadata is considered part of a group if it has no schema (generic)
        // or if its schema explicitly matches the Group ID.
        var schema = meta?.Schema;
        return schema == null || schema.OfGroup(groupId);
    }
}