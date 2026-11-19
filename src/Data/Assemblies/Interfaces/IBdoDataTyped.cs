namespace BindOpen.Data.Assemblies;

/// <summary>
/// Defines an object that carries a specific BindOpen data type.
/// Implementation of this interface allows for strict type-checking and 
/// validation across the framework.
/// </summary>
public interface IBdoDataTyped
{
    /// <summary>
    /// Gets or sets the data type of this object.
    /// This property defines the structural and value constraints of the 
    /// data this object represents.
    /// </summary>
    IBdoDataType DataType { get; set; }
}