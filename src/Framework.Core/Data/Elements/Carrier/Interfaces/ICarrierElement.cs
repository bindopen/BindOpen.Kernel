using BindOpen.Framework.Core.Data.Elements;

namespace BindOpen.Framework.Core.Data.Elements.Carrier
{
    public interface ICarrierElement : IDataElement
    {
        string DefinitionUniqueId { get; set; }

        new CarrierElementSpec Specification { get; set; }
    }
}