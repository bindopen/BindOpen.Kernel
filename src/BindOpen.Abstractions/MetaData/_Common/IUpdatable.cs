using BindOpen.Logging;

namespace BindOpen.MetaData
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areas"></param>
        /// <param name="updateModes"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        void Update(string[] areas = null, UpdateModes[] updateModes = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="areas"></param>
        /// <param name="updateModes"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        void Update<T>(T item = default, string[] areas = null, UpdateModes[] updateModes = null, IBdoLog log = null);
    }
}