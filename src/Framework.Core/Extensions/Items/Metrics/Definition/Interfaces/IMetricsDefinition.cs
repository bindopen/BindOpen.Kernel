using System;
using BindOpen.Framework.Core.Extensions.Items.Metrics.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Metrics.Definition
{
    public interface IMetricsDefinition : ITAppExtensionItemDefinition<IMetricsDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}