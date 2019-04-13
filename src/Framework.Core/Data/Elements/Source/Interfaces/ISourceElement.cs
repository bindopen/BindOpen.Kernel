using BindOpen.Framework.Core.Data.Elements;

namespace BindOpen.Framework.Core.Data.Elements.Source
{
    public interface ISourceElement : IDataElement
    {
        string DefinitionUniqueId { get; set; }
    }
}