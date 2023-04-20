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
        void Update(string[] areas = null, UpdateModes[] updateModes = null, IBdoBaseLog log = null);
    }
}