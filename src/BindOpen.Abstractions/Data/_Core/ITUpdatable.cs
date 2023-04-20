using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITUpdatable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="areas"></param>
        /// <param key="updateModes"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        void Update(
            T refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoBaseLog log = null);
    }
}