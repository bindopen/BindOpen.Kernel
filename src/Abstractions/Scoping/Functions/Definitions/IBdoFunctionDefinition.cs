using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using System;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoFunctionDefinition : IBdoExtensionDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        ITBdoSet<IBdoSpec> AdditionalSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoDataType ParentDataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoDataType OutputDataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Type RuntimeClassType { get; set; }


        Delegate RuntimeFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoFunctionDomainedDelegate RuntimeScopedFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoFunctionDelegate RuntimeBasicFunction { get; set; }
    }
}