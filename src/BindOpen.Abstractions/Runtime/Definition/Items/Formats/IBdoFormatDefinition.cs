using BindOpen.Meta.Items;
using System;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoFormatDefinition : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DatasourceKind DatasourceKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ViewerClass { get; set; }
    }
}