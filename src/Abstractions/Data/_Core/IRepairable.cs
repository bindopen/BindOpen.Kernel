using BindOpen.System.Diagnostics.Logging;

namespace BindOpen.System.Data
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