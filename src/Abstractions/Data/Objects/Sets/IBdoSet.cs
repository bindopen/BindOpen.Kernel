using System.Collections;
using BindOpen.Data;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
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

        T Descendant<T>(
            params object[] tokens)
            where T : IReferenced;

    }
}