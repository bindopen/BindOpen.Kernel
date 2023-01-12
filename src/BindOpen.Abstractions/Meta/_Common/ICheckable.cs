using BindOpen.Logging;

namespace BindOpen.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICheckable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isExistenceChecked"></param>
        /// <param name="areas"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        void Check(bool isExistenceChecked = true, string[] areas = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isExistenceChecked"></param>
        /// <param name="item"></param>
        /// <param name="areas"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        void Check<T>(bool isExistenceChecked = true, T item = default, string[] areas = null, IBdoLog log = null);
    }
}