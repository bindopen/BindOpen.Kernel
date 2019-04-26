using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Data.Specification.Filters;

namespace BindOpen.Framework.Core.Data.Elements.Source
{
    public interface ISourceElementSpec : IDataElementSpec
    {
        DataSourceKind DataSourceKind { get; set; }

        DataValueFilter DefinitionFilter { get; set; }
    }
}