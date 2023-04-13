using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoStringSet : IBdoObjectNotMetable
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> AddedValues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> RemovedValues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> ToList();

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