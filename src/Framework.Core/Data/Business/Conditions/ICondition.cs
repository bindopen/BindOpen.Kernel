using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Business.Conditions
{
    public interface ICondition : IDataItem
    {
        bool TrueValue { get; set; }

        bool Evaluate(IScriptInterpreter scriptInterpreter, IScriptVariableSet scriptVariableSet);
    }
}