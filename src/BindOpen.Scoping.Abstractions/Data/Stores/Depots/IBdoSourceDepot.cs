namespace BindOpen.Scoping.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSourceDepot : ITBdoDepot<IBdoDatasource>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="sourceName"></param>
        /// <returns></returns>
        string GetInstanceName(string sourceName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="sourceName"></param>
        /// <returns></returns>
        string GetInstanceOtherwiseModuleName(string sourceName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="sourceName"></param>
        /// <returns></returns>
        string GetModuleName(string sourceName = null);
    }
}