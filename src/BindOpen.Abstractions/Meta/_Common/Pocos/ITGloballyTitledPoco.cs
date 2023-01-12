using BindOpen.Meta.Items;
using System.Collections.Generic;

namespace BindOpen.Meta
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public interface ITGloballyTitledPoco<T> : IGloballyTitled
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        T AddTitle(KeyValuePair<string, string> item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        T WithTitle(IBdoDictionary dictionary);

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="defaultKey">The default variant name to consider.</param>
        string GetTitleText(string key = "*", string defaultKey = "*");
    }
}
