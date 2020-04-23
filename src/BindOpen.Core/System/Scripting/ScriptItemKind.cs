using System;

namespace BindOpen.System.Scripting
{
    /// <summary>
    /// This enumeration lists the possible kinds of script items.
    /// </summary>
    [Flags]
    public enum ScriptItemKinds
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Function.
        /// </summary>
        Function = 1 << 0,

        /// <summary>
        /// Variable.
        /// </summary>
        Variable = 1 << 1,

        /// <summary>
        /// Text.
        /// </summary>
        Text = 1 << 2,

        /// <summary>
        /// Syntax.
        /// </summary>
        Syntax = 1 << 3,

        /// <summary>
        /// Literal.
        /// </summary>
        Literal = 1 << 4,

        /// <summary>
        /// Any.
        /// </summary>
        Any = ScriptItemKinds.Function | ScriptItemKinds.Literal | ScriptItemKinds.Syntax | ScriptItemKinds.Text | ScriptItemKinds.Variable
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an specification level extension.
    /// </summary>
    public static class ScriptItemKindExtension
    {

        /// <summary>
        /// Indicates whether the specified element kind is a script word kind.
        /// </summary>
        /// <param name="scriptItemKind">The kind to consider.</param>
        /// <returns>Returns true if the specified element is a script word kind.</returns>
        public static bool IsFunctionOrVariable(this ScriptItemKinds scriptItemKind)
        {
            return (scriptItemKind == ScriptItemKinds.Function) | (scriptItemKind == ScriptItemKinds.Variable);
        }
    }

    #endregion


}
