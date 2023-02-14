using BindOpen.Logging;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class IBdoScriptInterpreterExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static IBdoScriptword CreateWordFromScript(
            this IBdoScriptInterpreter interpreter,
            string script,
            IBdoLog log = null)
        {
            return interpreter?.FindNextScriptword(script, log);
        }
    }
}
