using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Data.Objects.Interfaces;
using System.Linq;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class represents a script word within the BindOpen Scripting Language (BOSL).
/// </summary>
/// <remarks>
/// A script word is an atomic unit of an expression (function, variable, or literal) 
/// that can be chained to other script words to form a fluent execution path.
/// </remarks>
public class BdoScriptword : BdoMetaObject, IBdoScriptword
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoScriptword"/> class.
    /// </summary>
    public BdoScriptword() : base()
    {
        ExpressionKind = BdoExpressionKind.Word;
        this.WithDataType(DataValueTypes.Scriptword);
    }

    #endregion

    // -----------------------------------------------
    // CONVERTERS
    // -----------------------------------------------

    #region Converters

    /// <summary>
    /// Explicitly converts a <see cref="BdoScriptword"/> to its string representation.
    /// </summary>
    /// <param name="word">The script word to convert.</param>
    public static explicit operator string(BdoScriptword word)
    {
        return word?.ToString();
    }

    /// <summary>
    /// Concatenates a string with a script word wrapped in BOSL delimiters.
    /// </summary>
    /// <param name="st">The prefix string.</param>
    /// <param name="word">The script word to append.</param>
    /// <returns>A formatted string: "prefix{{script}}".</returns>
    public static string operator +(string st, BdoScriptword word)
        => st + (word == null ? string.Empty : "{{" + word.ToString() + "}}");

    /// <summary>
    /// Concatenates a script word wrapped in BOSL delimiters with a string.
    /// </summary>
    /// <param name="word">The script word prefix.</param>
    /// <param name="st">The suffix string.</param>
    /// <returns>A formatted string: "{{script}}suffix".</returns>
    public static string operator +(BdoScriptword word, string st)
        => (word == null ? string.Empty : "{{" + word.ToString() + "}}") + st;

    #endregion

    // ------------------------------------------
    // IBdoExpression Implementation
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the raw text value of this instance.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the expression kind of this instance.
    /// </summary>
    public BdoExpressionKind ExpressionKind { get; set; } = BdoExpressionKind.Word;

    #endregion

    // -----------------------------------------------
    // IBdoScriptword Implementation
    // -----------------------------------------------

    #region IBdoScriptword

    /// <summary>
    /// Adds the specified item.
    /// </summary>
    /// <param key="item">The item of the item to add.</param>
    /// <returns>Returns the new item that has been added.
    /// Returns null if the new item is null or else its name is null.</returns>
    /// <remarks>The new item must have a name.</remarks>
    public override IBdoMetaData Insert(IBdoMetaData item)
    {
        _propertySet ??= BdoData.NewSet();
        _propertySet.Insert(item);

        return item;
    }

    /// <summary>
    /// Gets or sets the token kind of this script word (Function, Variable, Literal, etc.).
    /// </summary>
    public ScriptTokenKinds TokenKind { get; set; } = ScriptTokenKinds.None;

    /// <summary>
    /// Gets or sets the child script word in the execution chain.
    /// </summary>
    public IBdoScriptword Child { get; set; }

    /// <summary>
    /// Returns the unique key identifying this instance.
    /// </summary>
    /// <returns>The identifier of the script word.</returns>
    public override string Key() => Identifier;

    /// <summary>
    /// Navigates to the last script word of the current chain.
    /// </summary>
    /// <param name="levelMax">The maximum recursion depth to prevent stack overflow.</param>
    /// <returns>The final <see cref="IBdoScriptword"/> in the chain.</returns>
    public IBdoScriptword Last(int levelMax = 50)
    {
        if (levelMax <= 0) return null;
        return Child == null ? this : Child.Last(--levelMax);
    }

    /// <summary>
    /// Returns a string that represents the current script word chain.
    /// </summary>
    public override string ToString() => ToString(fromRoot: true);

    /// <summary>
    /// Formats the script word.
    /// </summary>
    /// <param name="fromRoot">If true, climbs to the root. Set to false for internal recursion.</param>
    public string ToString(bool fromRoot)
    {
        // 1. Navigation to Root (Entry point only)
        // We climb up once to ensure the full script is generated.
        if (fromRoot && Parent is BdoScriptword rootCandidate)
        {
            return rootCandidate.ToString(true);
        }

        string script;

        // 2. Prefix Resolution
        // Prefix '$' is added if:
        // - It's the root (Parent == null)
        // - OR it's a parameter (not a fluent child of its parent).
        bool isFluentChild = Parent is BdoScriptword pWord && pWord.Child == this;
        string prefix = !isFluentChild ? BdoScriptHelper.Symbol_Fun : string.Empty;

        switch (TokenKind)
        {
            case ScriptTokenKinds.Function:
                // 3. Enumerating parameters
                // We use ToString(false) to ensure parameters start their own prefix logic ($).
                var args = string.Join(", ", this.Select(p =>
                {
                    if (p is BdoScriptword sw) return sw.ToString(false);
                    // For literals/meta, we wrap in single quotes and escape existing quotes.
                    var val = p?.GetData()?.ToString() ?? string.Empty;
                    return $"'{val.Replace("'", "''")}'";
                }));

                script = $"{prefix}{Name}({args})";
                break;

            case ScriptTokenKinds.Variable:
                // Specific variable syntax: $('varName')
                script = $"{prefix}('{Name?.Replace("'", "''")}')";
                break;

            case ScriptTokenKinds.Text:
                // Simple text literal syntax: 'text'
                script = $"'{Name?.Replace("'", "''")}'";
                break;

            case ScriptTokenKinds.None:
                return string.Empty;

            default:
                script = Name ?? string.Empty;
                break;
        }

        // 4. Downward Fluent Chaining
        // If the word has a child, we append it with a dot.
        if (Child is BdoScriptword childWord)
        {
            script += "." + childWord.ToString(false);
        }

        return script;
    }
    
    #endregion

    // --------------------------------------------------
    // CLONING
    // --------------------------------------------------

    #region Accessors

    /// <summary>
    /// Creates a shallow copy of the current script word.
    /// </summary>
    /// <returns>A cloned instance of the script word.</returns>
    public override object Clone()
    {
        return MemberwiseClone();
    }

    #endregion

    // --------------------------------------------------
    // DISPOSABLE
    // --------------------------------------------------

    #region IDisposable Implementation

    private bool _isDisposed = false;

    /// <summary>
    /// Disposes the current instance, ensuring no circular references remain.
    /// </summary>
    /// <param name="isDisposing">Indicates whether the instance is being disposed.</param>
    protected override void Dispose(bool isDisposing)
    {
        if (_isDisposed) return;
        _isDisposed = true;
        base.Dispose(isDisposing);
    }

    #endregion
}