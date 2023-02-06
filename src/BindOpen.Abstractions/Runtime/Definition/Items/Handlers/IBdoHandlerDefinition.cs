using BindOpen.Data.Meta;
using BindOpen.Data.Meta;

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
        IBdoMetaSpecList ParameterSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string PostFunctionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSpec SourceSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSpec TargetSpecification { get; set; }
    }
}