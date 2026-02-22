namespace BindOpen.Scoping;

/// <summary>
/// This static class provides extension methods for managing scoped objects.
/// </summary>
public static class IBdoScopeExtensions
{
    /// <summary>
    /// Sets the scope of the specified scoped object.
    /// </summary>
    /// <typeparam name="T">The type of the scoped object.</typeparam>
    /// <param name="scoped">The object to which the scope will be attached.</param>
    /// <param name="scope">The BindOpen scope to attach.</param>
    /// <returns>Returns the specified scoped object for fluent chaining.</returns>
    public static T WithScope<T>(
        this T scoped,
        IBdoScope scope)
        where T : IBdoScoped
    {
        if (scoped != null)
        {
            scoped.Scope = scope;
        }

        return scoped;
    }
}