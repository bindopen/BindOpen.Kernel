namespace BindOpen.Framework.Core.Extensions.Definitions.Metrics
{
    public interface IMetricsDefinitionDto : IAppExtensionItemDefinitionDto
    {
        string ItemClass { get; set; }
        string UnitCode { get; set; }

    }
}