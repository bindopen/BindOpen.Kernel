using System;
using BindOpen.Framework.Core.Extensions.Items.Metrics.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Metrics.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMetricsDefinition : ITAppExtensionItemDefinition<IMetricsDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}