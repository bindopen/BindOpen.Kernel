using System.Collections.Generic;
using BindOpen.Data.Items;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataValueFilter : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> AddedValues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool AddedValuesSpecified { get; }

        /// <summary>
        /// 
        /// </summary>
        List<string> RemovedValues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool RemovedValuesSpecified { get; }

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
        void Repair(List<string> allValues = null);
    }
}