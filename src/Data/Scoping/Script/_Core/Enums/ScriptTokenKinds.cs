using System;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Specifies the fundamental types of tokens recognized by the BindOpen scripting engine.
/// </summary>
/// <remarks>
/// This flags enumeration allows the parser to identify and differentiate between 
/// static text, dynamic variables, and executable logic within a script expression.
/// </remarks>
[Flags]
public enum ScriptTokenKinds
{
    /// <summary>
    /// No token kind specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// An executable scriptword or method (e.g., "text:concat").
    /// </summary>
    Function = 1 << 0,

    /// <summary>
    /// A dynamic reference to a stored value or environment parameter (e.g., "$var1").
    /// </summary>
    Variable = 1 << 1,

    /// <summary>
    /// A raw string fragment that does not contain executable script logic.
    /// </summary>
    Text = 1 << 2,

    /// <summary>
    /// Structural characters used to define script boundaries (e.g., "{{", "}}", "(", ")").
    /// </summary>
    Syntax = 1 << 3,

    /// <summary>
    /// A constant value provided directly in the script (e.g., a number or a quoted string).
    /// </summary>
    Literal = 1 << 4,

    /// <summary>
    /// A composite flag representing any valid script token.
    /// </summary>
    Any = Function | Literal | Syntax | Text | Variable
}