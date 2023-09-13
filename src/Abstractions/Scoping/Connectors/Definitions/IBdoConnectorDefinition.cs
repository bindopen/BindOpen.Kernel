using BindOpen.Kernel.Data;
using BindOpen.Kernel.Scoping.Entities;

namespace BindOpen.Kernel.Scoping.Connectors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnectorDefinition : IBdoEntityDefinition
    {
        /// <summary>
        /// Data source kind of this instance.
        /// </summary>
        DatasourceKind DatasourceKind { get; set; }
    }
}