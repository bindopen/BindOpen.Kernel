using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Extensions.Configuration.Scriptwords;
using BindOpen.Framework.Core.Extensions.Indexes;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.System.Scripting
{
    public interface IScriptInterpreter
    {
        IScriptWordIndex Index { get; set; }

        object Evaluate(IDataExpression dataExpression, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        object Evaluate(IDataExpression dataExpression, out string resultScript, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        object Evaluate(string script, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        object Evaluate(string script, out string resultScript, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        IScriptWord FindNextScriptWord(ref string script, IScriptWord parentScriptWord, ref int index, int offsetIndex, IScriptVariableSet scriptVariableSet = null, bool isSimulationModeOn = false, ILog log = null);

        string Interprete(IDataExpression dataExpression, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        string Interprete(string script, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        void LoadDefinitions(string[] libraryNames = null);
    }
}