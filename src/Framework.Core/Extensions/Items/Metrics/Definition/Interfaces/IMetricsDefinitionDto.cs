namespace BindOpen.Framework.Core.Extensions.Items.Metrics.Definition
{
    public interface IMetricsDefinitionDto : IAppExtensionItemDefinitionDto
    {
        string ItemClass { get; set; }
        string UnitCode { get; set; }

    }
}