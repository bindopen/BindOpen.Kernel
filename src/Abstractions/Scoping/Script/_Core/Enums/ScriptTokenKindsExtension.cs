namespace BindOpen.Scoping.Script;

/// <summary>
/// Provides extension methods for the <see cref="ScriptTokenKinds"/> enumeration.
/// </summary>
public static class ScriptTokenKindsExtension
{
    /// <summary>
    /// Determines whether the specified script token kind represents a dynamic element 
    /// (either a function or a variable).
    /// </summary>
    /// <param name="scriptElementKind">The token kind to evaluate.</param>
    /// <returns>
    /// Returns <c>true</c> if the kind is <see cref="ScriptTokenKinds.Function"/> 
    /// or <see cref="ScriptTokenKinds.Variable"/>; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This is used by the interpreter to identify tokens that require 
    /// dynamic resolution against the script domain.
    /// </remarks>
    public static bool IsFunctionOrVariable(this ScriptTokenKinds scriptElementKind)
    {
        // Note: Uses bitwise OR logic to check for dynamic content
        return (scriptElementKind & (ScriptTokenKinds.Function | ScriptTokenKinds.Variable)) != 0;
    }
}