using BindOpen.Scoping;
using BindOpen.Scoping.Script;

namespace BindOpen
{
    /// <summary>
    /// This static class provides shared data and engine instances for all scoping tests.
    /// It ensures that the BindOpen environment is initialized once per test run.
    /// </summary>
    public static class ScopingTestData
    {
        private static IBdoScope _appScope = null;

        /// <summary>
        /// Gets the global application scope. 
        /// Initializes the scope and loads required test extensions upon first access.
        /// </summary>
        public static IBdoScope Scope
        {
            get
            {
                if (_appScope == null)
                {
                    // 1. Create a fresh scope instance
                    _appScope = BdoScoping.NewScope();

                    // 2. Register extensions from specific test assemblies.
                    // This allows the interpreter to resolve custom functions, tasks, and connectors.
                    _appScope.LoadExtensions(q => q
                        .AddAssemblyFrom<DataTestSetup>()
                        .AddAssemblyFrom<ScopingTestSetup>("scoping.tests"));
                }

                return _appScope;
            }
        }

        private static IBdoScriptInterpreter _scriptInterpreter;

        /// <summary>
        /// Gets the default script interpreter associated with the global scope.
        /// </summary>
        public static IBdoScriptInterpreter ScriptInterpreter
        {
            get
            {
                // The interpreter inherits the extension store and settings from the scope.
                _scriptInterpreter ??= Scope.Interpreter;

                return _scriptInterpreter;
            }
        }
    }
}