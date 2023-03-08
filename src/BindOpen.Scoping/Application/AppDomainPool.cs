using BindOpen.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping.Application
{
    /// <summary>
    /// This class represents an assembly pool.
    /// </summary>
    public class AppDomainPool : IAppDomainPool
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly List<AppDomain> _appDomains = new();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of AppDomainPool class.
        /// </summary>
        public AppDomainPool()
        {
            _appDomains = new List<AppDomain>();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the specified application domain.
        /// </summary>
        public AppDomain GetAppDomain(string appDomainId)
        {
            if (appDomainId == null)
                return null;

            return _appDomains.FirstOrDefault(p => p.FriendlyName.BdoKeyEquals(appDomainId));
        }

        #endregion
    }
}
