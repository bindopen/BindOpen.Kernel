using BindOpen.Data.Elements;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoHandlerDefinition : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        BdoHandlerGetFunction RuntimeFunctionGet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoHandlerPostFunction RuntimeFunctionPost { get; set; }

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
        IBdoElementSpecSet ParameterSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string PostFunctionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoElementSpec SourceSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoElementSpec TargetSpecification { get; set; }
    }
}