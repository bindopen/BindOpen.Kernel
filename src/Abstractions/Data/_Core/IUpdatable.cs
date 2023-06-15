using BindOpen.System.Logging;

namespace BindOpen.System.Data
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
    }
}