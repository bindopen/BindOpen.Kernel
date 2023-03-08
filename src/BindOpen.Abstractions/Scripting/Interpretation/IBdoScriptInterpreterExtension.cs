using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;

namespace BindOpen.Scripting
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class IBdoScriptInterpreterExtension
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param key="script"></param>
        ///// <param key="log"></param>
        ///// <returns></returns>
        //public static IBdoScriptword CreateWordFromScript(
        //    this IBdoScriptInterpreter interpreter,
        //    string script,
        //    IBdoMetaSet varSet = null,
        //    IBdoLog log = null)
        //{
        //    int index = 0;
        //    return interpreter?.FindNextWord(script, null, ref index, 0, varSet, log);
        //}

        /// <summary>
        /// Evaluates the specified script word using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param key="scriptword">The script word to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public static T Evaluate<T>(
            this IBdoScriptInterpreter interpreter,
            IBdoExpression exp,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            interpreter?.Evaluate(exp, varSet, log).As<T>();

            return default;
        }
    }
}
