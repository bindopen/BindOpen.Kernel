using System;

namespace BindOpen.Framework.Core.Extensions.Definition.Metrics
{
    public interface IMetricsDefinition : ITAppExtensionItemDefinition<IMetricsDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}