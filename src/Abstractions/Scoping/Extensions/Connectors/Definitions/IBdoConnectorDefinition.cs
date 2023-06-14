using BindOpen.System.Data;
using BindOpen.System.Scoping.Entities;

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