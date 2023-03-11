using BindOpen.Data;
using BindOpen.Extensions.Entities;

namespace BindOpen.Extensions.Connectors
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