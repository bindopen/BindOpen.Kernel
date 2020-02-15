using BindOpen.Data.Elements;
using BindOpen.Data.Items;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnectorDefinitionDto : IBdoExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        DatasourceKind DatasourceKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSpecSet DatasourceDetailSpec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }
    }
}