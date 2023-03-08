using BindOpen.Data;
using BindOpen.Scoping;
using BindOpen.Scoping.Scopes;

namespace BindOpen.Tests.Kernel
{
    public static class ScopingTests
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
                    _appScope = BdoScoping.NewScope();
                    _appScope.LoadExtensions(
                        q => q.AddAllAssemblies());
                }

                return _appScope;
            }
        }

    }

}
