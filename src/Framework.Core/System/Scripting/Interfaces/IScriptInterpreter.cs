using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.System.Scripting
{
    /// <summary>
    /// This interface defines the script interpreter.
    /// </summary>
    public interface IScriptInterpreter : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataExpression"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Evaluate(IDataExpression dataExpression, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataExpression"></param>
        /// <param name="resultScript"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Evaluate(IDataExpression dataExpression, out string resultScript, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Evaluate(string script, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="resultScript"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Evaluate(string script, out string resultScript, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parentScriptword"></param>
        /// <param name="index"></param>
        /// <param name="offsetIndex"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="isSimulationModeOn"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IScriptword FindNextScriptword(ref string script, IScriptword parentScriptword, ref int index, int offsetIndex, IScriptVariableSet scriptVariableSet = null, bool isSimulationModeOn = false, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataExpression"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        string Interprete(IDataExpression dataExpression, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        string Interprete(string script, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptWord"></param>
        /// <param name="scriptWordDefinition"></param>
        /// <returns></returns>
        bool IsWordMatching(IScriptword scriptWord, IScriptwordDefinition scriptWordDefinition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentDefinition"></param>
        /// <returns></returns>
        List<IScriptwordDefinition> GetDefinitionsWithApproximativeName(string name, IScriptwordDefinition parentDefinition = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentDefinition"></param>
        /// <returns></returns>
        List<IScriptwordDefinition> GetDefinitionsWithExactName(string name, IScriptwordDefinition parentDefinition = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<IScriptwordDefinition> GetDefinitions();
    }
}