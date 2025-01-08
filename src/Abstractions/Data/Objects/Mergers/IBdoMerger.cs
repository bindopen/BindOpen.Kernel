using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMerger : IBdoObjectNotMetable, IIdentified, IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        IList<string> AddedValues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IList<string> RemovedValues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<string> ToList();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string[] ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="value"></param>
        /// <param key="allValues"></param>
        /// <returns></returns>
        bool Contains(string value);
    }
}