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
        public static T NewMetaWrapper<T>(
            this IBdoScope scope,
            IBdoMetaSet detail = null,
            bool onlyMetaAttributes = false)
            where T : class, IBdoMetaWrapper, new()
        {
            var obj = new T()
                .WithScope(scope)
                .WithDetail(detail);

            obj.UpdateFromMeta(detail, onlyMetaAttributes);

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T NewMetaWrapper<T>(
            this IBdoScope scope,
            params IBdoMetaData[] elms)
            where T : class, IBdoMetaWrapper, new()
        {
            return scope.NewMetaWrapper<T>(NewMetaSet(elms));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T NewMetaWrapper<T>(
            this IBdoScope scope,
            bool onlyMetaAttributes = false,
            params IBdoMetaData[] elms)
            where T : class, IBdoMetaWrapper, new()
        {
            return scope.NewMetaWrapper<T>(NewMetaSet(elms), onlyMetaAttributes);
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T NewMetaWrapper<T>(
            this IBdoScope scope,
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaWrapper, new()
        {
            return scope.NewMetaWrapper<T>(NewMetaSet(pairs));
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T NewMetaWrapper<T>(
            this IBdoScope scope,
            bool onlyMetaAttributes = false,
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaWrapper, new()
        {
            return scope.NewMetaWrapper<T>(NewMetaSet(pairs), onlyMetaAttributes);
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T NewMetaWrapper<T>(
            this IBdoScope scope,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : class, IBdoMetaWrapper, new()
        {
            return scope.NewMetaWrapper<T>(NewMetaSet(pairs));
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T NewMetaWrapper<T>(
            this IBdoScope scope,
            bool onlyMetaAttributes = false,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : class, IBdoMetaWrapper, new()
        {
            return scope.NewMetaWrapper<T>(NewMetaSet(pairs), onlyMetaAttributes);
        }
    }
}
