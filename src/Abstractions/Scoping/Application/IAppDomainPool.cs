using System;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This interface defines an assembly pool.
    /// </summary>
    public interface IAppDomainPool
    {
        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the specified application domain.
        /// </summary>
        AppDomain GetAppDomain(string appDomainId);

        #endregion
    }
}
