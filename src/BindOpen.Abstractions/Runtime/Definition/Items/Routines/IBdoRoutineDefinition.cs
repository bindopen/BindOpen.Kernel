using BindOpen.Data.Meta;
using BindOpen.Data;
using System;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRoutineDefinition : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IDescribed> OutputResultCodes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaList ParameterStatement { get; set; }
    }
}