using BindOpen.System.Scoping.Script;
using System;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This class represents an scope factory.
    /// </summary>
    public static partial class BdoScoping
    {
        /// <summary>
        /// Creates a new scope.
        /// </summary>
        /// <param key="appDomain">The application domain to consider.</param>
        /// <returns>The log of check log.</returns>
        public static IBdoScope NewScope(AppDomain appDomain = null)
        {
            var scope = new BdoScope(appDomain)
                .LoadExtensions(
                            q => q.AddAssemblyFrom<BdoScriptword>());
            return scope;
        }
    }
}