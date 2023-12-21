using BindOpen.Data.Meta;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a named data.
    /// </summary>
    public static class IBdoDetailedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static IBdoMetaSet GetOrNewDetail(this IBdoDetailed obj)
        {
            return obj.Detail ??= BdoData.NewSet();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithDetail<T>(
            this T obj,
            params IBdoMetaData[] metas)
            where T : IBdoDetailed
        {
            if (obj != null)
            {
                obj.GetOrNewDetail().Clear();
                obj.Detail.Add(metas);
            }
            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T WithDetail<T>(
            this T obj,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoDetailed
        {
            obj?.WithDetail(pairs?.Select(q => BdoData.NewMeta(q.Key, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithDetail<T>(
            this T obj,
            params (string Name, object Value)[] pairs)
            where T : IBdoDetailed
        {
            obj?.WithDetail(pairs?.Select(q => BdoData.NewMeta(q.Name, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithDetail<T>(
            this T obj,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoDetailed
        {
            obj?.WithDetail(pairs?.Select(q => BdoData.NewMeta(q.Name, q.ValueType, q.Value)).ToArray());

            return obj;
        }

        // Flag

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isAllocatable"></param>
        /// <returns></returns>
        public static T SetFlagValue<T>(
            this T spec,
            string flagName,
            bool isFlag = true)
            where T : IBdoDetailed
        {
            spec?.GetOrNewDetail().Add(flagName, isFlag);

            return spec;
        }
    }
}
