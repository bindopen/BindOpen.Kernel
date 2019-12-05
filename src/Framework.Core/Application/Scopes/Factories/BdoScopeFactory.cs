using System;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents an application scope helper.
    /// </summary>
    public static class BdoScopeFactory
    {
        /// <summary>
        /// Creates a new scope.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        /// <returns>The log of check log.</returns>
        public static IBdoScope CreateScope(AppDomain appDomain = null)
        {
            var scope = new BdoScope(appDomain);
            return scope;
        }
    }
}