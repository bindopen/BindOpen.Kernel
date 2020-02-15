using BindOpen.Data.Elements;
using BindOpen.Data.Specification;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICarrierElementSpec : IDataElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        DataValueFilter DefinitionFilter { get; set; }
    }
}