using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Specification.Filters;

namespace BindOpen.Framework.Core.Data.Elements.Carrier
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