namespace BindOpen.Framework.Core.Extensions.Definition.Metrics
{
    public interface IMetricsDefinition : IAppExtensionItemDefinition
    {
        string ItemClass { get; set; }
        string UnitCode { get; set; }
    }
}