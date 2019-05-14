using BindOpen.Framework.Core.Data.Specification.Filters;

namespace BindOpen.Framework.Core.Data.Elements.Collection
{
    public interface ICollectionElementSpec : IDataElementSpec
    {
        DataValueFilter ClassFilter { get; set; }

        DataValueFilter DefinitionFilter { get; set; }
    }
}