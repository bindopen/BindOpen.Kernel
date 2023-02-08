using BindOpen.Data.Helpers;
using BindOpen.Extensions.Scripting;

namespace BindOpen.Tests
{
    public static class GlobalVariables
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
                    _scriptInterpreter = BdoScript.CreateInterpreter(); ;
                }

                return _scriptInterpreter;
            }
        }
    }

}
