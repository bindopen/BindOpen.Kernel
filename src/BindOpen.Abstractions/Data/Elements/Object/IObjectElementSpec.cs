using BindOpen.Data.Specification;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectElementSpec : IBdoElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        IDataValueFilter ClassFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IObjectElementSpec WithClassFilter(IDataValueFilter filter);

        /// <summary>
        /// 
        /// </summary>
        IDataValueFilter DefinitionFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IObjectElementSpec WithDefinitionFilter(IDataValueFilter filter);
    }
}