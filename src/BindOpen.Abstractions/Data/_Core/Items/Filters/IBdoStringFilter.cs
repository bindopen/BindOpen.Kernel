using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoStringFilter : IBdoNotMetableItem
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
        /// <param key="allValues"></param>
        /// <returns></returns>
        List<string> GetValues(List<string> allValues = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="value"></param>
        /// <param key="allValues"></param>
        /// <returns></returns>
        bool IsValueAllowed(string value, List<string> allValues = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="allValues"></param>
        void UpdateWith(List<string> allValues = null);
    }
}