using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Specification;

namespace BindOpen.Framework.Core.Data.Elements
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