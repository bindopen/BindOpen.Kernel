using BindOpen.Data.Specification;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaObjectSpec : IBdoMetaSpec
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
        IBdoMetaObjectSpec WithClassFilter(IDataValueFilter filter);

        /// <summary>
        /// 
        /// </summary>
        IDataValueFilter DefinitionFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IBdoMetaObjectSpec WithDefinitionFilter(IDataValueFilter filter);
    }
}