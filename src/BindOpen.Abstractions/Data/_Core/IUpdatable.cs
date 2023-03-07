using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="areas"></param>
        /// <param key="updateModes"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        void Update(string[] areas = null, UpdateModes[] updateModes = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="item"></param>
        /// <param key="areas"></param>
        /// <param key="updateModes"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        void Update<T>(T item = default, string[] areas = null, UpdateModes[] updateModes = null, IBdoLog log = null);
    }
}