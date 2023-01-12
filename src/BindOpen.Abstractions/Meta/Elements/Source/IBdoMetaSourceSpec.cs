using BindOpen.Meta.Items;
using BindOpen.Meta.Specification;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaSourceSpec : IBdoMetaElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        DatasourceKind DatasourceKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IBdoMetaSourceSpec WithDatasourceKind(DatasourceKind kind);

        /// <summary>
        /// 
        /// </summary>
        IDataValueFilter DefinitionFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IBdoMetaSourceSpec WithDefinitionFilter(IDataValueFilter filter);
    }
}