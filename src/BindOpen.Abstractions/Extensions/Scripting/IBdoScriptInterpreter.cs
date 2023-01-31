using BindOpen.Data.Items;
using BindOpen.Logging;
using System.Collections.Generic;
using BindOpen.Data.Meta;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This interface defines the script interpreter.
    /// </summary>
    public interface IBdoScriptInterpreter : IBdoItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="varSet">The variable element set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Evaluate(
            IBdoExpression exp,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="varSet">The variable element set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        T Evaluate<T>(
            IBdoExpression exp,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="expKind">The exp kind to consider.</param>
        /// <param name="varSet">The variable element set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Evaluate(
            string script,
            BdoExpressionKind expKind = BdoExpressionKind.Auto,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="expKind">The exp kind to consider.</param>
        /// <param name="varSet">The variable element set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        T Evaluate<T>(
            string script,
            BdoExpressionKind expKind = BdoExpressionKind.Auto,
            IBdoMetaSet varSet = null, IBdoLog log = null);

        /// <summary>
        /// Evaluates the specified script word using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        object Evaluate(
            IBdoScriptword scriptword,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// Evaluates the specified script word using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        T Evaluate<T>(
            IBdoScriptword scriptword,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parentScriptword"></param>
        /// <param name="index"></param>
        /// <param name="offsetIndex"></param>
        /// <param name="varSet">The variable element set to consider.</param>
        /// <param name="isSimulationModeOn"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IBdoScriptword FindNextScriptword(
            string script,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parentScriptword"></param>
        /// <param name="index"></param>
        /// <param name="offsetIndex"></param>
        /// <param name="varSet">The variable element set to consider.</param>
        /// <param name="isSimulationModeOn"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IBdoScriptword FindNextScriptword(
            string script,
            IBdoScriptword parentScriptword,
            ref int index,
            int offsetIndex,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptword"></param>
        /// <param name="scriptwordDefinition"></param>
        /// <returns></returns>
        bool IsWordMatching(
            IBdoScriptword scriptword,
            IBdoScriptwordDefinition scriptwordDefinition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentDefinition"></param>
        /// <returns></returns>
        Dictionary<string, IBdoScriptwordDefinition> GetDefinitionsWithApproximativeName(
            string name,
            IBdoScriptwordDefinition parentDefinition = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentDefinition"></param>
        /// <returns></returns>
        Dictionary<string, IBdoScriptwordDefinition> GetDefinitionsWithExactName(
            string name,
            IBdoScriptwordDefinition parentDefinition = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, IBdoScriptwordDefinition> GetDefinitions();
    }
}