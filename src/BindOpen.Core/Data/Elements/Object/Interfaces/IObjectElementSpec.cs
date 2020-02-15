using BindOpen.Data.Elements;
using BindOpen.Data.Specification;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectElementSpec : IDataElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        DataValueFilter ClassFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueFilter DefinitionFilter { get; set; }
    }
}