using BindOpen.Framework.Core.Extensions.Definition.Metrics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Configuration.Metrics
{
    public interface IMetricsConfiguration : ITAppExtensionTitledItemConfiguration<IMetricsDefinition>
    {
        string ValueScript { get; set; }

        int? GetValue(IScriptInterpreter scriptInterpreter, IScriptVariableSet scriptVariableSet = null);
    }
}