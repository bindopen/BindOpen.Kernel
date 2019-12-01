using System;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents a BindOpen host scope helper.
    /// </summary>
    public static class BdoHostScopeFactory
    {
        /// <summary>
        /// Creates a new scope.
        /// </summary>
        /// <returns>The log of check log.</returns>
        public static IBdoHostScope CreateHostScope(AppDomain appDomain = null) => new BdoHostScope(appDomain);
    }
}