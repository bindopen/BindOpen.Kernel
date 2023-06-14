using BindOpen.System.Scoping.Script;
using BindOpen.System.Data.Helpers;

namespace BindOpen.System.Tests.Scoping
{
    public static class Tests
    {
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
                    _scriptInterpreter = ScopingTests.Scope.Interpreter; ;
                }

                return _scriptInterpreter;
            }
        }
    }

}
