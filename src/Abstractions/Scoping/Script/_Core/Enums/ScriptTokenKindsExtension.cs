namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents an schema level extension.
    /// </summary>
    public static class ScriptTokenKindsExtension
    {
        /// <summary>
        /// Indicates whether the specified element kind is a script word kind.
        /// </summary>
        /// <param key="scriptElementKind">The kind to consider.</param>
        /// <returns>Returns true if the specified element is a script word kind.</returns>
        public static bool IsFunctionOrVariable(this ScriptTokenKinds scriptElementKind)
        {
            return scriptElementKind == ScriptTokenKinds.Function | scriptElementKind == ScriptTokenKinds.Variable;
        }
    }
}
