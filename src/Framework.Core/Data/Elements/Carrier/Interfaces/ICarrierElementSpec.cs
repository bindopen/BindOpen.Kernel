using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Specification;

namespace BindOpen.Framework.Data.Elements
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