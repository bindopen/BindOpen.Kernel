using BindOpen.Meta.Specification;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaCollectionSpec : IBdoMetaElementSpec
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
        IBdoMetaCollectionSpec WithClassFilter(IDataValueFilter filter);

        /// <summary>
        /// 
        /// </summary>
        IDataValueFilter DefinitionFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IBdoMetaCollectionSpec WithDefinitionFilter(IDataValueFilter filter);
    }
}