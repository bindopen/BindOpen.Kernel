using BindOpen.Data.Specification;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaSetSpec : IBdoMetaDataSpec
    {
        /// <summary>
        /// 
        /// </summary>
        IDataValueFilter ClassFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IBdoMetaSetSpec WithClassFilter(IDataValueFilter filter);

        /// <summary>
        /// 
        /// </summary>
        IDataValueFilter DefinitionFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IBdoMetaSetSpec WithDefinitionFilter(IDataValueFilter filter);
    }
}