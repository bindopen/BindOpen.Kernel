using BindOpen.Data;
using BindOpen.Data.Meta;
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
        IBdoMetaSpecSet DatasourceDetailSpec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }
    }
}