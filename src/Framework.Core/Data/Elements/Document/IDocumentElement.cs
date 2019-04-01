using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Entity;

namespace BindOpen.Framework.Core.Data.Elements.Document
{
    public interface IDocumentElement : IDataElement
    {
        CarrierElement ContainerElement { get; }
        EntityElement ContentElement { get; }
    }
}