namespace BindOpen.Data.Assemblies;

/// <summary>
/// Defines a BindOpen data type.
/// This interface bridges fundamental value types with complex class references,
/// providing the logic required for compatibility checking and data validation.
/// </summary>
public interface IBdoDataType : IBdoClassReference, IBdoDefinable
{
    /// <summary>
    /// Gets or sets the fundamental value type (e.g., String, Number, Boolean, Null).
    /// </summary>
    DataValueTypes ValueType { get; set; }

    /// <summary>
    /// Determines whether this data type is compatible with another specified data type.
    /// This is used for schema inheritance and type casting logic.
    /// </summary>
    /// <param name="dataType">The data type to compare against.</param>
    /// <returns>True if the types are compatible; otherwise, false.</returns>
    bool IsCompatibleWithType(IBdoDataType dataType);

    /// <summary>
    /// Determines whether a specific object instance is compatible with this data type.
    /// This is the primary method used during runtime metadata validation.
    /// </summary>
    /// <param name="data">The data instance to check.</param>
    /// <returns>True if the data matches the type requirements; otherwise, false.</returns>
    bool IsCompatibleWithData(object data);
}