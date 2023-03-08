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
        IBdoStringFilter ClassFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="filter"></param>
        /// <returns></returns>
        IBdoObjectSpec WithClassFilter(IBdoStringFilter filter);

        /// <summary>
        /// 
        /// </summary>
        IBdoStringFilter DefinitionFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="filter"></param>
        /// <returns></returns>
        IBdoObjectSpec WithDefinitionFilter(IBdoStringFilter filter);
    }
}