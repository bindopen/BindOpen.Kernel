using BindOpen.Meta.Elements;
using BindOpen.Meta.Items;
using System;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnectorDefinition : IBdoExtensionItemDefinition
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
        IBdoElementSpecSet DatasourceDetailSpec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }
    }
}