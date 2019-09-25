using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;

namespace BindOpen.Framework.Core.Extensions.Items.Handlers.Definition.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHandlerDefinitionDto : IAppExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        string CallingClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string GetFunctionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSpecSet ParameterSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string PostFunctionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSpec SourceSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSpec TargetSpecification { get; set; }
    }
}