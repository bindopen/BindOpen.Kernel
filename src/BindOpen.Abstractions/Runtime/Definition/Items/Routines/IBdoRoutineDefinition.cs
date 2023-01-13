using BindOpen.MetaData;
using BindOpen.MetaData.Elements;
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
        IBdoElementSet ParameterStatement { get; set; }
    }
}