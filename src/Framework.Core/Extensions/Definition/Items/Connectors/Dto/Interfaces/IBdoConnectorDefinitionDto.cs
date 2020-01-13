using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Extensions.Definition
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