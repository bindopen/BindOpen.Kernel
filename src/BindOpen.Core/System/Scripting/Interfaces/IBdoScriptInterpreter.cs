using BindOpen.Data.Expression;
using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using System.Collections.Generic;

namespace BindOpen.System.Scripting
{
    /// <summary>
    /// This interface defines the script interpreter.
    /// </summary>
    public interface IBdoScriptInterpreter : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataExpression"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Evaluate(IDataExpression dataExpression, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="expressionKind">The expression kind to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Evaluate(string script, DataExpressionKind expressionKind, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// Evaluates the specified script word using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        object Evaluate(
            BdoScriptword scriptword,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parentScriptword"></param>
        /// <param name="index"></param>
        /// <param name="offsetIndex"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="isSimulationModeOn"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IBdoScriptword FindNextScriptword(string script, IBdoScriptword parentScriptword, ref int index, int offsetIndex, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptWord"></param>
        /// <param name="scriptWordDefinition"></param>
        /// <returns></returns>
        bool IsWordMatching(IBdoScriptword scriptWord, IBdoScriptwordDefinition scriptWordDefinition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentDefinition"></param>
        /// <returns></returns>
        Dictionary<string, IBdoScriptwordDefinition> GetDefinitionsWithApproximativeName(string name, IBdoScriptwordDefinition parentDefinition = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentDefinition"></param>
        /// <returns></returns>
        Dictionary<string, IBdoScriptwordDefinition> GetDefinitionsWithExactName(string name, IBdoScriptwordDefinition parentDefinition = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, IBdoScriptwordDefinition> GetDefinitions();
    }
}