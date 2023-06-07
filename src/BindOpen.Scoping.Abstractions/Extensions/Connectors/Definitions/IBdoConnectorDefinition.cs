using BindOpen.Scoping.Data;
using BindOpen.Scoping.Extensions.Entities;

namespace BindOpen.Scoping.Extensions.Connectors
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