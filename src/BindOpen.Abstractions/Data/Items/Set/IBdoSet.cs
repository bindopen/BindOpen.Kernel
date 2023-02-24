using System.Collections;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBdoSet : IBdoItem, IIdentified, IReferenced, IEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <returns></returns>
        IReferenced this[string key] { get; }
    }
}