using System;

namespace BindOpen.Framework.Core.System.Scripting
{

    /// <summary>
    /// This enumeration lists the possible kinds of script items.
    /// </summary>
    [Flags]
    public enum ScriptItemKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,
        /// <summary>
        /// Any.
        /// </summary>
        Any = ScriptItemKind.Function | ScriptItemKind.Literal | ScriptItemKind.Syntax | ScriptItemKind.Text | ScriptItemKind.Variable,
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
        Literal = 1 << 4
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
        public static Boolean IsFunctionOrVariable(this ScriptItemKind scriptItemKind)
        {
            return (scriptItemKind == ScriptItemKind.Function) | (scriptItemKind == ScriptItemKind.Variable);
        }
    }

    #endregion


}
