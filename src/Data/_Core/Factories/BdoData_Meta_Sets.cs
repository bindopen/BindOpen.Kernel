using BindOpen.Kernel.Data.Meta;
using System.Linq;

namespace BindOpen.Kernel.Data
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
        public static BdoMetaSet NewSet()
            => NewSet<BdoMetaSet>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(string name)
            => NewSet<BdoMetaSet>(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(
            params IBdoMetaData[] metas)
            => NewSet<BdoMetaSet>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(
            string name,
            params IBdoMetaData[] metas)
            => NewSet<BdoMetaSet>(name, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(
            string name,
            params (string Name, object Value)[] pairs)
            => NewSet<BdoMetaSet>(name, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(
            params (string Name, object Value)[] pairs)
            => NewSet<BdoMetaSet>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewSet<BdoMetaSet>(name, triplets);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewSet<BdoMetaSet>(triplets);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>()
            where T : class, IBdoMetaSet, new()
            => NewItemSet<T, IBdoMetaData>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(string name)
            where T : class, IBdoMetaSet, new()
            => NewSet<T>().WithName(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(
            string name,
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaSet, new()
        {
            var list = NewSet<T>();
            list
                .With(metas)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaSet, new()
            => NewSet<T>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(
            string name,
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaSet, new()
        {
            var list = NewSet<T>()
                .With(pairs)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaSet, new()
            => NewSet<T>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaSet, new()
        {
            var list = NewSet<T>(
                triplets.Select(q => NewMeta(q.Name, q.ValueType, q.Value)).ToArray())
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaSet, new()
            => NewSet<T>(null, triplets);
    }
}
