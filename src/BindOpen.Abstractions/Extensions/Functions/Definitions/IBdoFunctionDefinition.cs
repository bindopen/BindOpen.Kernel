using BindOpen.Data;
using BindOpen.Data.Assemblies;
using System;

namespace BindOpen.Extensions.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoFunctionDefinition : IBdoExtensionDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        DataValueTypes OutputValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RuntimeFunctionName { get; set; }

        Delegate RuntimeFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes ParentValueType { get; set; }

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