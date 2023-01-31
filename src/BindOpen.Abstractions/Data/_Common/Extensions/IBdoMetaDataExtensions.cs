using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoMetaDataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static object GetSubItem(
            this IBdoMetaData meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var subSet = GetSubSet(meta);
            var subMeta = subSet?.Get(key);
            var obj = subMeta?.Item(scope, varSet, log);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static Q GetSubItem<Q>(
            this IBdoMetaData meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var subSet = GetSubSet(meta);
            var subMeta = subSet?.Get(key);
            if (subMeta != null)
            {
                var obj = subMeta.Item<Q>(scope, varSet, log);
                return obj;
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
            this IBdoMetaData meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var subSet = GetSubSet(meta);
            var subMeta = subSet?.Get(key);
            var list = subMeta?.Items(scope, varSet, log);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static List<Q> GetSubItems<Q>(
            this IBdoMetaData meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var subSet = GetSubSet(meta);
            var subMeta = subSet?.Get(key);
            var list = subMeta?.Items<Q>(scope, varSet, log);
            return list;
        }

        private static IBdoMetaSet GetSubSet(
            IBdoMetaData meta)
        {
            if (meta is IBdoMetaScalar)
            {
                return null;
            }
            else if (meta is IBdoMetaObject metaObject)
            {
                return metaObject?.PropertyMetaSet;
            }
            else if (meta is IBdoMetaSet metaSet)
            {
                return metaSet;
            }

            return null;
        }
    }
}
