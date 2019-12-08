using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification;

namespace BindOpen.Framework.Core.Data.Elements
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