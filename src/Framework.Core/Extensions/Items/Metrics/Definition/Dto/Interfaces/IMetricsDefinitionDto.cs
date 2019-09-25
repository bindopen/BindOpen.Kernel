namespace BindOpen.Framework.Core.Extensions.Items.Metrics.Definition.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMetricsDefinitionDto : IAppExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string UnitCode { get; set; }

    }
}