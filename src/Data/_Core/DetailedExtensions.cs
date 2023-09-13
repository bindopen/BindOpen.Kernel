using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface represents a named data.
    /// </summary>
    public static class DetailedExtensions
    {
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
                obj.Detail ??= BdoData.NewSet();
                obj.Detail.Clear();
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
    }
}
