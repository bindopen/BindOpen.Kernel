using System;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// This class represents an scope factory.
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