using BindOpen.Data.Meta;
using BindOpen.Extensions;
using System;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntityDefinition : IBdoExtensionDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IBdoFormatDefinition> FormatDefinitions { get; }

        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoEntityKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ViewerClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoSpecSet DetailSpec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        /// <returns></returns>
        IBdoFormatDefinition GetFormatWithUniqueName(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        IBdoFormatDefinition GetFormatWithName(string name);
    }
}