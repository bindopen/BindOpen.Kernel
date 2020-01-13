using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Data.Specification;

namespace BindOpen.Framework.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISourceElementSpec : IDataElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        DatasourceKind DatasourceKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueFilter DefinitionFilter { get; set; }
    }
}