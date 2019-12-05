using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items.Datasources;
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
        DatasourceKind DatasourceKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueFilter DefinitionFilter { get; set; }
    }
}