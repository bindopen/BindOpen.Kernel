using BindOpen.Meta.Specification;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaCarrierSpec : IBdoMetaElementSpec
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
        IBdoMetaCarrierSpec WithDefinitionFilter(IDataValueFilter filter);
    }
}