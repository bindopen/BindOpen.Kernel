using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Data.Specification.Filters;

namespace BindOpen.Framework.Core.Data.Elements.Source
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISourceElementSpec : IDataElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        DataSourceKind DataSourceKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueFilter DefinitionFilter { get; set; }
    }
}