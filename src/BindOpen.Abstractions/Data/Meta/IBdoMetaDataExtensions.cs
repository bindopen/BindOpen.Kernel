using BindOpen.Data.Items;
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
        /// <param name="objs"></param>
        public static T WithData<T>(
            this T meta,
            object obj)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                obj = obj.ToBdoElementItem(meta?.GetSpecification());
                meta.WithDataList(obj);
            }
            return meta;
        }

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
            var obj = subMeta?.GetData(scope, varSet, log);
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
                var obj = subMeta.GetData<Q>(scope, varSet, log);
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
            var list = subMeta?.GetDataList(scope, varSet, log);
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
            var list = subMeta?.GetDataList<Q>(scope, varSet, log);
            return list;
        }

        private static IBdoMetaSet GetSubSet(
            this IBdoMetaData meta)
        {
            if (meta is IBdoMetaScalar)
            {
                return null;
            }
            else if (meta is IBdoMetaObject metaObject)
            {
                return metaObject?.PropertySet;
            }
            else if (meta is IBdoMetaSet metaSet)
            {
                return metaSet;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithItemizationMode<T>(
            this T meta,
            DataItemizationMode mode)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.ItemizationMode = mode;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataValueType<T>(
            this T meta,
            DataValueTypes valueType)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataValueType = valueType;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoReference reference)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataReference = reference;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataExpression<T>(
            this T meta,
            IBdoExpression exp)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataExpression = exp;
            }

            return meta;
        }
    }
}
