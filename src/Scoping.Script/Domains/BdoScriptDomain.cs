using BindOpen.Data.Meta;
using BindOpen.Logging;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class represents the execution domain for a script.
/// </summary>
/// <remarks>
/// The script domain provides the necessary environment—including scope, 
/// variables, and logging—to evaluate a specific <see cref="IBdoScriptword"/>.
/// </remarks>
public class BdoScriptDomain : IBdoScriptDomain
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the scope associated with this domain.
    /// </summary>
    /// <value>The scope providing access to function dictionaries and extensions.</value>
    public IBdoScope Scope { get; set; }

    /// <summary>
    /// Gets or sets the set of variables available within this domain.
    /// </summary>
    /// <value>The metadata set containing variable definitions used during evaluation.</value>
    public IBdoMetaSet VariableSet { get; set; }

    /// <summary>
    /// Gets or sets the log used to track events during script evaluation.
    /// </summary>
    public IBdoLog Log { get; set; }

    /// <summary>
    /// Gets or sets the scriptword currently being evaluated within this domain.
    /// </summary>
    public IBdoScriptword Scriptword { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoScriptDomain"/> class.
    /// </summary>
    /// <param name="scope">The scope to utilize for function lookups.</param>
    /// <param name="metaSet">The set of variables available for resolution.</param>
    /// <param name="scriptword">The scriptword to be evaluated.</param>
    /// <param name="log">The optional log for capturing execution events.</param>
    public BdoScriptDomain(
        IBdoScope scope,
        IBdoMetaSet metaSet,
        IBdoScriptword scriptword,
        IBdoLog log = null)
    {
        Scope = scope;
        VariableSet = metaSet;
        Scriptword = scriptword;
        Log = log;
    }

    #endregion
}