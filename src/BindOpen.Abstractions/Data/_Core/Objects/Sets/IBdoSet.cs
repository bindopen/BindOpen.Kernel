using System.Collections;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBdoSet :
        IBdoObject,
        IIdentified, IReferenced,
        IEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <returns></returns>
        IReferenced this[string key] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <returns></returns>
        IReferenced this[int index] { get; }
    }
}