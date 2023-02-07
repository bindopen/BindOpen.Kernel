using BindOpen.Data.Meta;
using BindOpen.Extensions;
using System;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntityDefinition : IBdoExtensionItemDefinition
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
        IBdoSpecList DetailSpec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IBdoFormatDefinition GetFormatWithId(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoFormatDefinition GetFormatWithName(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <returns></returns>
        IBdoFormatDefinition GetFormatWithUniqueName(string uniqueName);

    }
}