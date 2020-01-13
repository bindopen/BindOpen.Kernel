using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoFormatDefinitionDto : IBdoExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        DatasourceKind DatasourceKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ViewerClass { get; set; }
    }
}