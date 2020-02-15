using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;

namespace BindOpen.Data.Elements
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