using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoMetaObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithProperties<T>(
            this T meta,
            IBdoMetaSet set)
            where T : IBdoMetaObject
        {
            if (meta != null)
            {
                meta.PropertySet = set;
            }
            //var item = set?.FirstOrDefault();
            //if (item is IBdoConfiguration config
            //    && !string.IsNullOrEmpty(item.DefinitionUniqueId))
            //{
            //    ClassReference = BdoData.ClassFromEntity(
            //        item?.DefinitionUniqueId);
            //}
            return meta;
        }

        // Get property 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static object GetPropertyData(
            this IBdoMetaObject meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = meta?.PropertySet?
                .GetData(key, scope, varSet, log);

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static Q GetPropertyData<Q>(
            this IBdoMetaObject meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (meta?.PropertySet != null)
            {
                return meta.PropertySet
                    .GetData<Q>(key, scope, varSet, log);
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
        public static List<object> GetPropertyDataList(
            this IBdoMetaObject meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var objList = meta?.PropertySet?
                .GetDataList(key, scope, varSet, log);

            return objList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static List<Q> GetPropertyDataList<Q>(
            this IBdoMetaObject meta,
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var objList = meta?.PropertySet?
                .GetDataList<Q>(key, scope, varSet, log);

            return objList;
        }

    }
}