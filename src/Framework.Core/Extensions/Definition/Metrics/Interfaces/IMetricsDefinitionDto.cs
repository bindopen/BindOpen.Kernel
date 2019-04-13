namespace BindOpen.Framework.Core.Extensions.Definition.Metrics
{
    public interface IMetricsDefinitionDto : IAppExtensionItemDefinitionDto
    {
        string ItemClass { get; set; }
        string UnitCode { get; set; }

    }
}