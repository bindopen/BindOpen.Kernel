using System;
using BindOpen.Framework.Core.Extensions.Items.Carriers.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers.Definition
{
    /// <summary>
    /// This interface defines the carrier definition.
    /// </summary>
    public interface ICarrierDefinition : ITAppExtensionItemDefinition<ICarrierDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}