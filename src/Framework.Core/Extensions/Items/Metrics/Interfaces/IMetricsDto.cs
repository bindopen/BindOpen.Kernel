using BindOpen.Framework.Core.Extensions.Definition.Metrics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Metrics
{
    public interface IMetricsDto
        : ITAppExtensionTitledItemDto<MetricsDefinitionDto>
    {
        string ValueScript { get; set; }

        int? GetValue(IScriptInterpreter scriptInterpreter, IScriptVariableSet scriptVariableSet = null);
    }
}