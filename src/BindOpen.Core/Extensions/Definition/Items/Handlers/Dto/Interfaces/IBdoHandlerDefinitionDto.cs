using BindOpen.Data.Elements;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoHandlerDefinitionDto : IBdoExtensionItemDefinitionDto
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