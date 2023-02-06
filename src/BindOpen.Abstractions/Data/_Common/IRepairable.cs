using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepairable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areas"></param>
        /// <param name="updateModes"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        void Repair(string[] areas = null, UpdateModes[] updateModes = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="areas"></param>
        /// <param name="updateModes"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        void Repair<T>(T item = default, string[] areas = null, UpdateModes[] updateModes = null, IBdoLog log = null);
    }
}