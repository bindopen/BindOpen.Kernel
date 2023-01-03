using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This interface defines the carrier definition.
    /// </summary>
    public interface IBdoCarrierDefinition : IBdoExtensionItemDefinition
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
        IBdoElementSpecSet DetailSpec { get; set; }
    }
}