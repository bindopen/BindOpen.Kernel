namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This class represents an specification level extension.
    /// </summary>
    public static class ScriptItemKindsExtension
    {
        /// <summary>
        /// Indicates whether the specified element kind is a script word kind.
        /// </summary>
        /// <param key="scriptItemKind">The kind to consider.</param>
        /// <returns>Returns true if the specified element is a script word kind.</returns>
        public static bool IsFunctionOrVariable(this ScriptItemKinds scriptItemKind)
        {
            return scriptItemKind == ScriptItemKinds.Function | scriptItemKind == ScriptItemKinds.Variable;
        }
    }
}
