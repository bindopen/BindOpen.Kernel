using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Specification;

namespace BindOpen.Framework.Core.Data.Elements
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