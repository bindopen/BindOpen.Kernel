using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoMetaObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static object GetSubItem(
            this IBdoMetaObject meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => meta?.SubSet?.GetItem(key, scope, varSet, log);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static Q GetSubItem<Q>(
            this IBdoMetaObject meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (meta?.SubSet != null)
            {
                return meta.SubSet.GetItem<Q>(key, scope, varSet, log);
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static List<object> GetSubItems(
            this IBdoMetaObject meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => meta?.SubSet?.GetItems(key, scope, varSet, log);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static List<Q> GetSubItems<Q>(
            this IBdoMetaObject meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => meta?.SubSet?.GetItems<Q>(key, scope, varSet, log);
    }
}
