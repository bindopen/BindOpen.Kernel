using System;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetricsDefinition : IBdoExtensionDefinition
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
        string UnitCode { get; set; }
    }
}