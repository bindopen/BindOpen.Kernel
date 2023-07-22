using BindOpen.System.Data.Meta;
using System.Linq;

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
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet()
            => NewMetaSet<BdoMetaSet>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(
            string name)
            => NewMetaSet<BdoMetaSet>(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(
            params IBdoMetaData[] metas)
            => NewMetaSet<BdoMetaSet>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(
            string name,
            params IBdoMetaData[] metas)
            => NewMetaSet<BdoMetaSet>(name, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(
            string name,
            params (string Key, IBdoMetaData Value)[] items)
            => NewMetaSet<BdoMetaSet>(name, items);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(
            params (string Key, IBdoMetaData Value)[] items)
            => NewMetaSet<BdoMetaSet>(null, items);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(
            string name,
            params (string Name, object Value)[] pairs)
            => NewMetaSet<BdoMetaSet>(name, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(
            params (string Name, object Value)[] pairs)
            => NewMetaSet<BdoMetaSet>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewMetaSet<BdoMetaSet>(name, triplets);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewMetaSet<BdoMetaSet>(triplets);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>()
            where T : class, IBdoMetaSet, new()
            => NewSet<T, IBdoMetaData>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>(string name)
            where T : class, IBdoMetaSet, new()
            => NewMetaSet<T>().WithName(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>(
            string name,
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaSet, new()
        {
            var list = NewMetaSet<T>();
            list
                .With(metas)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>(
            params (string Key, IBdoMetaData Value)[] items)
            where T : class, IBdoMetaSet, new()
            => NewMetaSet<T>(null, items);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>(
            string name,
            params (string Key, IBdoMetaData Value)[] items)
            where T : class, IBdoMetaSet, new()
        {
            var list = NewMetaSet<T>();
            list
                .With(items)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>(
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaSet, new()
            => NewMetaSet<T>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>(
            string name,
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaSet, new()
        {
            var list = NewMetaSet<T>()
                .With(pairs)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>(
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaSet, new()
            => NewMetaSet<T>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaSet, new()
        {
            var list = NewMetaSet<T>(
                triplets.Select(q => NewMeta(q.Name, q.ValueType, q.Value)).ToArray())
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaSet<T>(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaSet, new()
            => NewMetaSet<T>(null, triplets);
    }
}
