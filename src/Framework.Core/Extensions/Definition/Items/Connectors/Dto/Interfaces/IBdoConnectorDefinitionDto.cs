using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
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