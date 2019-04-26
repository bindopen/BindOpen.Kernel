using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements;

namespace BindOpen.Framework.Core.Data.Elements.Document
{
    public interface IDocumentElement : IDataElement
    {
        CarrierElement ContainerElement { get; }
        ObjectElement ContentElement { get; }

        new DocumentElementSpec Specification { get; set; }
    }
}