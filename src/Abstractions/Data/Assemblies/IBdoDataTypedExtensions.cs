using BindOpen.Data.Assemblies;

namespace BindOpen.Data;

/// <summary>
/// Provides extension methods for IBdoDataTyped to simplify type assignment 
/// and compatibility validation.
/// </summary>
public static partial class IBdoDataTypedExtensions
{
    /// <summary>
    /// Sets the data type for the specified object and returns the instance for fluent chaining.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoDataTyped.</typeparam>
    /// <param name="dataTyped">The object to update.</param>
    /// <param name="dataType">The data type to assign.</param>
    /// <returns>Returns the original instance with the data type applied.</returns>
    public static T WithDataType<T>(
        this T dataTyped,
        IBdoDataType dataType)
        where T : IBdoDataTyped
    {
        if (dataTyped != null)
        {
            dataTyped.DataType = dataType;
        }

        return dataTyped;
    }

    /// <summary>
    /// Verifies if the object's data type is compatible with the specified data type.
    /// </summary>
    /// <param name="dataTyped">The typed object to check.</param>
    /// <param name="dataType">The target data type to compare against.</param>
    /// <returns>True if the types are compatible; otherwise, false.</returns>
    public static bool IsCompatibleWithType(
        this IBdoDataTyped dataTyped,
        IBdoDataType dataType)
    {
        return dataTyped?.DataType?.IsCompatibleWithType(dataType) ?? false;
    }

    /// <summary>
    /// Verifies if the object's data type is compatible with a specific data value.
    /// </summary>
    /// <param name="dataTyped">The typed object providing the validation rules.</param>
    /// <param name="data">The raw data to validate.</param>
    /// <returns>True if the data matches the type requirements; otherwise, false.</returns>
    public static bool IsCompatibleWithData(
        this IBdoDataTyped dataTyped,
        object data)
    {
        return dataTyped?.DataType?.IsCompatibleWithData(data) ?? false;
    }
}