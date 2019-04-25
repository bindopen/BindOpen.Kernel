using System;

namespace BindOpen.Framework.Core.Extensions.Definitions.Metrics
{
    public interface IMetricsDefinition : ITAppExtensionItemDefinition<IMetricsDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}