using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoStringFilter : IBdoHandledItem
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
        /// <param name="allValues"></param>
        /// <returns></returns>
        List<string> GetValues(List<string> allValues = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="allValues"></param>
        /// <returns></returns>
        bool IsValueAllowed(string value, List<string> allValues = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allValues"></param>
        void UpdateWith(List<string> allValues = null);
    }
}