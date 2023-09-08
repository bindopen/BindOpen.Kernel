using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Logging;

namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// This interface defines the script interpreter.
    /// </summary>
    public interface IBdoScriptInterpreter : IBdoObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="exp"></param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        object Evaluate(
            IBdoExpression exp,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="exp"></param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        object Evaluate(
            IBdoScriptword word,
            IBdoMetaSet varSet = null,
            IBdoLog log = null,
            bool root = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="exp"></param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        object Evaluate(
            IBdoReference reference,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="script"></param>
        /// <param key="parentScriptword"></param>
        /// <param key="index"></param>
        /// <param key="offsetIndex"></param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="isSimulationModeOn"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        IBdoScriptword FindNextWord(
            string script,
            IBdoLog log = null);
    }
}