using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Helpers;
using BindOpen.Scoping.Data.Meta;
using BindOpen.Logging;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class IBdoScriptInterpreterExtension
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
            IBdoExpression exp,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            object obj = interpreter?.Evaluate(exp, varSet, log);

            return obj.As<T>();
        }

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
            IBdoScriptword word,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            object obj = interpreter?.Evaluate(word, varSet, log);

            return obj.As<T>();
        }

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
            IBdoReference reference,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            object obj = interpreter?.Evaluate(reference, varSet, log);

            return obj.As<T>();
        }
    }
}
