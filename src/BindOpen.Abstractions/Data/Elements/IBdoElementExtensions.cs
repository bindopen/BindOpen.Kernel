using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents an extension of the DataValueType enumeration.
    /// </summary>
    public static class IBdoElementExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static List<object> GetItemList(
            this IBdoElement element,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var obj = element?.GetItem(scope, varElementSet, log);
            return obj is List<object> objList ? objList : new List<object>() { obj };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="element"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static List<Q> GetItemList<Q>(
            this IBdoElement element,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
            => element?.GetItemList(scope, varElementSet, log).Cast<Q>().ToList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static object GetFirstItem(
            this IBdoElement element,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var obj = element?.GetItemList(scope, varElementSet, log);
            return obj?.First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static Q GetFirstItem<Q>(
            this IBdoElement element,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var obj = element?.GetItemList<Q>(scope, varElementSet, log);
            return obj.FirstOrDefault() ?? default;
        }
    }
}