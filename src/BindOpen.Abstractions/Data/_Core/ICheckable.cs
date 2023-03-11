using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICheckable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="isExistenceChecked"></param>
        /// <param key="areas"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        void Check(bool isExistenceChecked = true, string[] areas = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="isExistenceChecked"></param>
        /// <param key="item"></param>
        /// <param key="areas"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        void Check<T>(bool isExistenceChecked = true, T item = default, string[] areas = null, IBdoLog log = null);
    }
}