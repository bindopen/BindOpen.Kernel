using BindOpen.Scopes;

namespace BindOpen.Tests
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
                _appScope ??= BdoScoping.NewScope()
                    .LoadExtensions(
                        q => q.AddAssemblyFrom<GlobalSetUp>());

                return _appScope;
            }
        }

    }

}
