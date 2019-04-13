using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Specification.Filters;

namespace BindOpen.Framework.Core.Data.Elements._Object
{
    public interface IObjectElementSpec : IDataElementSpec
    {
        DataValueFilter ClassFilter { get; set; }

        DataValueFilter DefinitionFilter { get; set; }
    }
}