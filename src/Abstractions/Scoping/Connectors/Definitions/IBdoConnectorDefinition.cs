using BindOpen.System.Scoping;
using BindOpen.System.Data;

namespace BindOpen.System.Scoping
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