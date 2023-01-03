using BindOpen.Data.Specification;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICarrierElementSpec : IBdoElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        IDataValueFilter DefinitionFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        ICarrierElementSpec WithDefinitionFilter(IDataValueFilter filter);
    }
}