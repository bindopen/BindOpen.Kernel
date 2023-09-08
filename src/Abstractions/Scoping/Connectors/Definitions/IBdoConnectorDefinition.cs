using BindOpen.Kernel.Scoping;
using BindOpen.Kernel.Data;

namespace BindOpen.Kernel.Scoping
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