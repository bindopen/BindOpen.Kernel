using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using System;

namespace BindOpen.System.Scoping
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
        BdoDataType ParentDataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoDataType OutputDataType { get; set; }

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