using BindOpen.MetaData.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoElementSet : ITBdoItemSet<IBdoMetaElement>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> GetSpecIds();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="specId"></param>
        /// <returns></returns>
        IBdoMetaElement Get(string name, string specId = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet">The variable element set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetItem(
            string key,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet">The variable element set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        T GetItem<T>(
            string key,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        List<object> GetItemList(
            string key,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        List<Q> GetItemList<Q>(
            string key,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);
    }
}