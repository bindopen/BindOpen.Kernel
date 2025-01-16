using BindOpen.Scoping;
using BindOpen.Scoping.Script;

namespace BindOpen;

public static class ScopingTestData
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
                _appScope.LoadExtensions(q => q
                    .AddAssemblyFrom<DataTestSetup>()
                    .AddAssemblyFrom<ScopingTestSetup>("scoping.tests"));
            }

            return _appScope;
        }
    }

    static IBdoScriptInterpreter _scriptInterpreter;

    public static IBdoScriptInterpreter ScriptInterpreter
    {
        get
        {
            if (_scriptInterpreter == null)
            {
                _scriptInterpreter = Scope.Interpreter; ;
            }

            return _scriptInterpreter;
        }
    }
}
