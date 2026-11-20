using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Stores;
using BindOpen.Logging;
using BindOpen.Scoping.Script;
using System;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents a BindOpen application scope, acting as the central 
/// orchestration point for extensions, data, and scripts.
/// </summary>
public partial class BdoScope : BdoObject, IBdoScope
{
    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoScope"/> class.
    /// </summary>
    /// <param name="appDomain">The application domain to use. If null, current domain is used.</param>
    public BdoScope(AppDomain appDomain = null) : base()
    {
        AppDomain = appDomain ?? AppDomain.CurrentDomain;

        // Initialize stores and interpreter using the standard factories
        ExtensionStore = BdoData.New<BdoExtensionStore>();
        Interpreter = BdoScript.NewInterpreter(this);
        DepotStore = BdoData.NewDepotStore();
        DataStore = BdoData.NewSet();
    }

    #endregion

    // ------------------------------------------
    // IBdoScope Implementation
    // ------------------------------------------

    #region IBdoScope

    /// <summary>
    /// Gets the application domain associated with this scope.
    /// </summary>
    public AppDomain AppDomain { get; }

    /// <summary>
    /// Gets or sets the extension store where all definitions (Connectors, Tasks, etc.) are indexed.
    /// </summary>
    public IBdoExtensionStore ExtensionStore { get; set; }

    /// <summary>
    /// Gets or sets the depot store for managing data resources.
    /// </summary>
    public IBdoDepotStore DepotStore { get; set; }

    /// <summary>
    /// Gets the primary data store (MetaSet) of this instance.
    /// </summary>
    public IBdoMetaSet DataStore { get; }

    /// <summary>
    /// Gets the script interpreter associated with this scope.
    /// </summary>
    public IBdoScriptInterpreter Interpreter { get; }

    /// <summary>
    /// Clears all stores within this instance.
    /// </summary>
    public void Clear()
    {
        DepotStore?.Clear();
        ExtensionStore?.Clear();
    }

    /// <summary>
    /// Validates the existence and integrity of required stores.
    /// </summary>
    /// <param name="checkExtensionStore">True if the extension store presence must be checked.</param>
    /// <param name="checkDataStore">True if the depot store presence must be checked.</param>
    /// <param name="log">The log to populate with check results.</param>
    /// <returns>Returns True if all requested checks passed; otherwise, False.</returns>
    public bool Check(
        bool checkExtensionStore = false,
        bool checkDataStore = false,
        IBdoLog log = null)
    {
        if (checkExtensionStore && ExtensionStore == null)
        {
            log?.AddEvent(BdoEventKinds.Error,
                "Application extension missing",
                description: "No extension item definition store specified.");
            return false;
        }

        if (checkDataStore && DepotStore == null)
        {
            log?.AddEvent(BdoEventKinds.Error,
                "Depot set missing",
                description: "No depot set specified.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Creates a new script domain for script evaluation.
    /// </summary>
    /// <param name="metaSet">The metadata set to associate with the domain.</param>
    /// <param name="scriptword">The current script word being evaluated.</param>
    /// <param name="log">The log to use during evaluation.</param>
    /// <returns>A new instance of <see cref="IBdoScriptDomain"/>.</returns>
    public IBdoScriptDomain NewScriptDomain(
        IBdoMetaSet metaSet,
        IBdoScriptword scriptword = null,
        IBdoLog log = null)
    {
        return new BdoScriptDomain(this, metaSet, scriptword, log);
    }

    #endregion
}