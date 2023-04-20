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
        /// <param key="areas"></param>
        /// <param key="updateModes"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        void Repair(string[] areas = null, UpdateModes[] updateModes = null, IBdoBaseLog log = null);
    }
}