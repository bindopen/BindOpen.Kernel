using BindOpen.System.Data.Helpers;
using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Tests
{
    public static class SystemData
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

        static string _workingFolder;
        static IBdoScriptInterpreter _scriptInterpreter;

        /// <summary>
        /// The global working folder.
        /// </summary>
        public static string WorkingFolder
        {
            get
            {
                if (_workingFolder == null)
                {
                    _workingFolder = (FileHelper.GetAppRootFolderPath() + @"temp\").ToPath();
                }

                return _workingFolder;
            }
        }

        public static IBdoScriptInterpreter ScriptInterpreter
        {
            get
            {
                if (_scriptInterpreter == null)
                {
                    _scriptInterpreter = SystemData.Scope.Interpreter; ;
                }

                return _scriptInterpreter;
            }
        }
    }

}
