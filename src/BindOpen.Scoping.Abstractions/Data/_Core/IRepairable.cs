using BindOpen.Logging;

namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepairable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="areas"></param>
        /// <param key="updateModes"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        void Repair(string[] areas = null, UpdateModes[] updateModes = null, IBdoLog log = null);
    }
}