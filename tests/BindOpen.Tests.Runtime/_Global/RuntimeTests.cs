using BindOpen.Data;
using BindOpen.Runtime;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Tests.Runtime
{
    public static class RuntimeTests
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
                    _appScope.LoadExtensions(new[] { BdoData.AssemblyAsAll() });
                }

                return _appScope;
            }
        }

    }

}
