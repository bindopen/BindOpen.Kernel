using BindOpen.Runtime;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Tests.Mango
{
    public static class GlobalVariables
    {
        static IBdoScope _appScope = null;

        /// <summary>
        /// The global scope.
        /// </summary>
        public static IBdoScope Scope
        {
            get
            {
                if (_appScope == null)
                {
                    _appScope = BdoRuntime.NewScope();
                    _appScope.LoadExtensions(new[] { BdoRuntime.ExtensionFromAll() });
                }

                return _appScope;
            }
        }

    }

}
