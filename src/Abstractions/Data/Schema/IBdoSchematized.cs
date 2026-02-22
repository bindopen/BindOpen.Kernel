namespace BindOpen.Data.Schema;

/// <summary>
/// Defines an object that conforms to a specific schema.
/// Implementation of this interface allows the BindOpen engine to associate 
/// metadata instances with their corresponding blueprints for validation and resolution.
/// </summary>
public interface IBdoSchematized
{
    /// <summary>
    /// Gets or sets the schema associated with this instance.
    /// This schema defines the rules, requirements, and constraints that the 
    /// object must follow.
    /// </summary>
    IBdoSchema Schema { get; set; }
}