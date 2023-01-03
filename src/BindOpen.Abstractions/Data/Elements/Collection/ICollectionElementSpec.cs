using BindOpen.Data.Specification;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICollectionElementSpec : IBdoElementSpec
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
        ICollectionElementSpec WithClassFilter(IDataValueFilter filter);

        /// <summary>
        /// 
        /// </summary>
        IDataValueFilter DefinitionFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        ICollectionElementSpec WithDefinitionFilter(IDataValueFilter filter);
    }
}