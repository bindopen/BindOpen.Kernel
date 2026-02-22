namespace BindOpen.Logging;

/// <summary>
/// This static class provides extension methods for objects that support internal logging.
/// </summary>
public static class IBdoLoggedExtensions
{
    /// <summary>
    /// Sets the internal log for the specified object.
    /// </summary>
    /// <typeparam name="T">A type implementing the IBdoLogged interface.</typeparam>
    /// <param name="logged">The object to consider.</param>
    /// <param name="log">The BindOpen log to attach.</param>
    /// <returns>Returns the specified object with the log attached for fluent chaining.</returns>
    public static T WithLog<T>(
        this T logged,
        IBdoLog log)
        where T : IBdoLogged
    {
        if (logged != null)
        {
            logged.Log = log;
        }

        return logged;
    }
}