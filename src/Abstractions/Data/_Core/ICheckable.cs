using BindOpen.System.Diagnostics.Logging;

namespace BindOpen.System.Data
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
    }
}