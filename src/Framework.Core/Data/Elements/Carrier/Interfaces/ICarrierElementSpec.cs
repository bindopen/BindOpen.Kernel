using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Specification.Filters;

namespace BindOpen.Framework.Core.Data.Elements.Carrier
{
    public interface ICarrierElementSpec : IDataElementSpec
    {
        DataValueFilter DefinitionFilter { get; set; }
    }
}