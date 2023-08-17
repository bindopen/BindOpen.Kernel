using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Scoping;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This static class provides methods to create data metaent list.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaWrap<T>(
            this IBdoScope scope,
            IBdoMetaSet detail = null)
            where T : class, IBdoMetaWrap, new()
        {
            var obj = new T()
                .WithScope(scope)
                .WithDetail(detail);

            obj.UpdateFromMeta(detail, true);

            return obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T NewMetaWrap<T>(
            this IBdoScope scope,
            params IBdoMetaData[] elms)
            where T : class, IBdoMetaWrap, new()
        {
            return scope.NewMetaWrap<T>(NewMetaSet(elms));
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T NewMetaWrap<T>(
            this IBdoScope scope,
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaWrap, new()
        {
            return scope.NewMetaWrap<T>(NewMetaSet(pairs));
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T NewMetaWrap<T>(
            this IBdoScope scope,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : class, IBdoMetaWrap, new()
        {
            return scope.NewMetaWrap<T>(NewMetaSet(pairs));
        }

    }
}
