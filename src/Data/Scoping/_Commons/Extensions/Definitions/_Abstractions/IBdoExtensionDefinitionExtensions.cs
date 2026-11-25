namespace BindOpen.Scoping;

/// <summary>
/// This static class provides extension methods for <see cref="IBdoExtensionDefinition"/>.
/// </summary>
public static class IBdoExtensionDefinitionExtensions
{
    /// <summary>
    /// Sets the package definition of this instance.
    /// </summary>
    /// <param name="definition">The package definition to set.</param>
    public static T WithPackage<T>(
        this T obj,
        IBdoPackageDefinition definition)
        where T : IBdoExtensionDefinition
    {
        if (obj != null)
        {
            obj.PackageDefinition = definition;
        }
        return obj;
    }

    /// <summary>
    /// Sets the image URI of this instance.
    /// </summary>
    /// <param name="imageUri">The image URI to set.</param>
    public static T WithImageUri<T>(
        this T obj,
        string imageUri)
        where T : IBdoExtensionDefinition
    {
        if (obj != null)
        {
            obj.ImageUri = imageUri;
        }
        return obj;
    }

    /// <summary>
    /// Sets whether this instance is editable.
    /// </summary>
    /// <param name="isEditable">Indicates whether the extension is editable.</param>
    public static T AsEditable<T>(
        this T obj,
        bool isEditable)
        where T : IBdoExtensionDefinition
    {
        if (obj != null)
        {
            obj.IsEditable = isEditable;
        }
        return obj;
    }

    /// <summary>
    /// Sets whether this instance is indexed.
    /// </summary>
    /// <param name="isIndexed">Indicates whether the extension should be indexed.</param>
    public static T AsIndexed<T>(
        this T obj,
        bool isIndexed)
        where T : IBdoExtensionDefinition
    {
        if (obj != null)
        {
            obj.IsIndexed = isIndexed;
        }
        return obj;
    }

    /// <summary>
    /// Sets the library ID of this instance.
    /// </summary>
    /// <param name="libraryId">The library ID to set.</param>
    public static T WithLibraryId<T>(
        this T obj,
        string libraryId)
        where T : IBdoExtensionDefinition
    {
        if (obj != null)
        {
            obj.LibraryId = libraryId;
        }
        return obj;
    }
}