using System.Collections;

namespace BindOpen.System.Data
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
            where T : class, IReferenced;

    }
}