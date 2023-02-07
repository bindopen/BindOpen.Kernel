using BindOpen.Data.Items;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoObjectSpec : IBdoSpec
    {
        /// <summary>
        /// 
        /// </summary>
        IStringFilter ClassFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IBdoObjectSpec WithClassFilter(IStringFilter filter);

        /// <summary>
        /// 
        /// </summary>
        IStringFilter DefinitionFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IBdoObjectSpec WithDefinitionFilter(IStringFilter filter);
    }
}