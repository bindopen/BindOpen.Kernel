using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Data.Specification.Filters;

namespace BindOpen.Framework.Core.Data.Elements.Source
{
    public interface ISourceElementSpec
    {
        IDataValueFilter ConnectorFilter { get; set; }
        DataSourceKind DataSourceKind { get; set; }
    }
}