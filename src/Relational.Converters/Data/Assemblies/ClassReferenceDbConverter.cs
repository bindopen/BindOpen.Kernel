using BindOpen.Data.Helpers;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a Db converter of assembly references.
/// </summary>
public static class ClassReferenceDbConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a database entity one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The database entity object.</returns>
    public static ClassReferenceDb ToDb(
        this IBdoClassReference poco)
    {
        if (poco == null) return null;

        ClassReferenceDb dbItem = new();
        dbItem.UpdateFromPoco(poco);

        return dbItem;
    }

    public static ClassReferenceDb UpdateFromPoco(
        this ClassReferenceDb dbItem,
        IBdoClassReference poco)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        poco.Identifier ??= StringHelper.NewGuid();

        dbItem.Alias = poco.Alias;
        dbItem.AssemblyFileName = poco.AssemblyFileName;
        dbItem.AssemblyName = poco.AssemblyName;
        dbItem.AssemblyVersion = poco.AssemblyVersion?.ToString();
        dbItem.Identifier = poco.Identifier;

        return dbItem;
    }

    /// <summary>
    /// Converts an assembly reference database entity into a poco one.
    /// </summary>
    /// <param key="dbItem">The database entity to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoClassReference ToPoco(
        this ClassReferenceDb dbItem)
    {
        if (dbItem == null) return null;

        BdoClassReference poco = new()
        {
            Alias = dbItem.Alias,
            AssemblyFileName = dbItem.AssemblyFileName,
            AssemblyName = dbItem.AssemblyName,
            AssemblyVersion = dbItem.AssemblyVersion == null ? null : new(dbItem.AssemblyVersion),
            Identifier = dbItem.Identifier,
            ClassName = dbItem.ClassName
        };

        return poco;
    }
}
