namespace BindOpen.Framework.Core.Extensions.Items.Metrics.Definition.Dto
{
    public interface IMetricsDefinitionDto : IAppExtensionItemDefinitionDto
    {
        string ItemClass { get; set; }
        string UnitCode { get; set; }

    }
}