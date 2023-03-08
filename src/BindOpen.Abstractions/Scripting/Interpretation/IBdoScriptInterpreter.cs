using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Functions;
using BindOpen.Logging;

namespace BindOpen.Scripting
{
    /// <summary>
    /// This interface defines the script interpreter.
    /// </summary>
    public interface IBdoScriptInterpreter : IBdoItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="exp"></param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="log"></param>
        /// <returns></returns>
        object Evaluate(
            IBdoExpression exp,
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
        /// <param key="log"></param>
        /// <returns></returns>
        IBdoScriptword FindNextWord(
            string script,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        ITBdoSet<IBdoFunctionDefinition> GetDefinitions();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        ITBdoSet<IBdoFunctionDefinition> GetDefinitionsWithName(
            string name,
            bool isExact = false);

        bool IsWordMatchingFunction(IBdoScriptword scriptword, IBdoFunctionDefinition functionDefinition);
    }
}