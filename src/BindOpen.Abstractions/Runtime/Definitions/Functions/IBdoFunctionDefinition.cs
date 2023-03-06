using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Runtime.Definitions
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
        IBdoSpecSet SpecDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RuntimeFunctionName { get; set; }

        Delegate RuntimeFunction { get; set; }
    }
}