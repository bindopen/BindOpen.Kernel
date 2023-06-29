using BindOpen.System.Scoping.Entities;
using BindOpen.System.Data;

namespace BindOpen.System.Scoping.Connectors
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