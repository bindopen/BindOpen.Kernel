namespace BindOpen.Data.Schema;

/// <summary>
/// This interface defines an object that conforms to a schema.
/// </summary>
public interface IBdoSchematized
{
    /// <summary>
    /// The schema.
    /// </summary>
    IBdoSchema Schema { get; set; }
}