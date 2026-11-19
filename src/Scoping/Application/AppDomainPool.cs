using BindOpen.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents a pool of application domains, allowing for centralized 
/// management and retrieval of execution contexts.
/// </summary>
public class AppDomainPool : IAppDomainPool
{
    // ------------------------------------------
    // VARIABLES
    // ------------------------------------------

    #region Variables

    /// <summary>
    /// The internal list of application domains.
    /// </summary>
    private readonly List<AppDomain> _appDomains = new();

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AppDomainPool"/> class.
    /// </summary>
    public AppDomainPool()
    {
        // The list is already initialized above via C# target-typed new
    }

    #endregion

    // ------------------------------------------
    // ACCESSORS
    // ------------------------------------------

    #region Accessors

    /// <summary>
    /// Retrieves an application domain by its identifier (FriendlyName).
    /// </summary>
    /// <param name="appDomainId">The identifier of the application domain to find.</param>
    /// <returns>Returns the matching <see cref="AppDomain"/> if found; otherwise, null.</returns>
    public AppDomain GetAppDomain(string appDomainId)
    {
        if (string.IsNullOrEmpty(appDomainId))
        {
            return null;
        }

        // Using BdoKeyEquals for standardized string comparison
        return _appDomains.FirstOrDefault(p => p.FriendlyName.BdoKeyEquals(appDomainId));
    }

    #endregion
}