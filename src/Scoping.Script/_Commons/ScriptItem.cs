using BindOpen.Data;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class represents a script item, used primarily during the tokenization phase.
/// </summary>
/// <remarks>
/// A ScriptItem stores the raw components of a script (like delimiters, function names, or literals)
/// before they are parsed into a hierarchical <see cref="BdoScriptword"/>.
/// </remarks>
public class ScriptItem : BdoObject, IScriptItem
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptItem"/> class.
    /// </summary>
    public ScriptItem()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptItem"/> class with specified values.
    /// </summary>
    /// <param name="kind">The token kind (Function, Syntax, Text, etc.).</param>
    /// <param name="name">The name or content of the token.</param>
    /// <param name="index">The character index where this token was found in the source string.</param>
    public ScriptItem(
        ScriptTokenKinds kind,
        string name,
        int index)
    {
        Kind = kind;
        Name = name;
        Index = index;
    }

    #endregion

    // ------------------------------------------
    // IScriptItem Implementation
    // ------------------------------------------

    #region IScriptItem

    /// <summary>
    /// Gets or sets the kind of this instance.
    /// </summary>
    /// <example>Scriptword, syntax, text, literal...</example>
    public ScriptTokenKinds Kind { get; set; } = ScriptTokenKinds.None;

    #endregion

    // ------------------------------------------
    // IIndexed Implementation
    // ------------------------------------------

    #region IIndexed

    /// <summary>
    /// Gets or sets the character index of this item in the source script.
    /// </summary>
    /// <remarks>
    /// This is essential for error reporting, allowing the engine to point to the 
    /// exact location of a syntax error.
    /// </remarks>
    public int? Index { get; set; }

    #endregion

    // ------------------------------------------
    // INamed Implementation
    // ------------------------------------------

    #region INamed

    /// <summary>
    /// Gets or sets the name or raw value of the script item.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Sets the name of the script item using a standardized naming convention.
    /// </summary>
    /// <param name="name">The name to assign.</param>
    /// <returns>Returns the current <see cref="IScriptItem"/> for fluent chaining.</returns>
    public IScriptItem WithName(string name)
    {
        Name = BdoData.NewName(name, "spec_");
        return this;
    }

    #endregion
}