using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors.Definition.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectorDefinitionDto : IAppExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        DataSourceKind DataSourceKind { get; set; }

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