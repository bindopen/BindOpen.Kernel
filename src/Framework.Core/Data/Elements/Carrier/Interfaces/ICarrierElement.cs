using BindOpen.Framework.Core.Extensions.Items.Carriers;

namespace BindOpen.Framework.Core.Data.Elements.Carrier
{
    public interface ICarrierElement : IDataElement
    {
        new ICarrierConfiguration this[int index] { get; }
        new ICarrierConfiguration this[string name] { get; }
        new ICarrierConfiguration First { get; }

        string DefinitionUniqueId { get; set; }

        new CarrierElementSpec Specification { get; set; }
    }
}