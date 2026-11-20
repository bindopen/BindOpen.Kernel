namespace BindOpen.Scoping.Script;

/// <summary>
/// This static class provides factory methods to create script elements and execution engines.
/// </summary>
public static partial class BdoScript
{
    /// <summary>
    /// Initializes a new instance of the script interpreter for a specific scope.
    /// </summary>
    /// <param name="scope">The scope providing the functions and variables available for script resolution.</param>
    /// <returns>Returns a new <see cref="IBdoScriptInterpreter"/> instance.</returns>
    /// <remarks>
    /// The interpreter is the core engine that transforms raw script strings 
    /// or <see cref="IBdoScriptword"/> trees into evaluated data results.
    /// </remarks>
    public static IBdoScriptInterpreter NewInterpreter(IBdoScope scope)
    {
        return new BdoScriptInterpreter(scope);
    }
}