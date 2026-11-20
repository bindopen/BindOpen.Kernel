namespace BindOpen.Scoping.Script;

/// <summary>
/// This class represents a strongly-typed script word.
/// </summary>
/// <typeparam name="T">The type of the item or value associated with this script word.</typeparam>
/// <remarks>
/// This class extends the standard <see cref="BdoScriptword"/> to provide type-specific 
/// context, allowing for better integration with strongly-typed data models and 
/// enhanced validation during script execution.
/// </remarks>
public class TBdoScriptword<T> : BdoScriptword, ITBdoScriptword<T>
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TBdoScriptword{T}"/> class.
    /// </summary>
    public TBdoScriptword() : base()
    {
        // Inherits ExpressionKind.Word and DataValueTypes.Scriptword from the base constructor.
    }

    #endregion
}