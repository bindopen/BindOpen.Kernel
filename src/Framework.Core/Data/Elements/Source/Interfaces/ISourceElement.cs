using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Core.Data.Elements.Source
{
    public interface ISourceElement : IDataElement
    {
        new IConnectorConfiguration this[int index] { get; }
        new IConnectorConfiguration this[string name] { get; }

        new IConnectorConfiguration First { get; }

        string DefinitionUniqueId { get; set; }
    }
}