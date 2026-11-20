namespace BindOpen.Logging;

/// <summary>
/// This interface defines an object that can hold its own logging information.
/// </summary>
public interface IBdoLogged
{
    /// <summary>
    /// Gets or sets the BindOpen log associated with this object.
    /// </summary>
    IBdoLog Log { get; set; }
}