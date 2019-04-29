using System;

namespace BindOpen.Framework.Core.Extensions.Items.Metrics.Definition
{
    public interface IMetricsDefinition : ITAppExtensionItemDefinition<IMetricsDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}