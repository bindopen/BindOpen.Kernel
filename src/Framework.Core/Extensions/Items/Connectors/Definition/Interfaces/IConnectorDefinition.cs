using System;
using BindOpen.Framework.Core.Extensions.Items.Connectors.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectorDefinition : ITAppExtensionItemDefinition<IConnectorDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}