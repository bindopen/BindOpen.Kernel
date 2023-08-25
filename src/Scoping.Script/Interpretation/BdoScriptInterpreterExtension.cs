using BindOpen.System.Data;
using BindOpen.System.Data.Helpers;
using BindOpen.System.Data.Meta;
using BindOpen.System.Logging;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScriptInterpreterExtension
    {
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
            string st,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            object obj = interpreter?.Evaluate(st.ToExpression(BdoExpressionKind.Script), varSet, log);

            return obj.As<T>();
        }
    }
}
