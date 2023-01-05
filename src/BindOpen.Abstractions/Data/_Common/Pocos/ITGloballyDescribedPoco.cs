using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public interface ITGloballyDescribedPoco<T> : IGloballyDescribed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        T AddDescription(KeyValuePair<string, string> item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        T WithDescription(IBdoDictionary dictionary);

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="defaultKey">The default variant name to consider.</param>
        string GetDescriptionText(string key = "*", string defaultKey = "*");
    }
}
