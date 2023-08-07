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
        public static BdoMetaComposite NewMetaComposite()
            => NewMetaComposite<BdoMetaComposite>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaComposite NewMetaComposite(
            string name)
            => NewMetaComposite<BdoMetaComposite>(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaComposite NewMetaComposite(
            params IBdoMetaData[] metas)
            => NewMetaComposite<BdoMetaComposite>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaComposite NewMetaComposite(
            string name,
            params IBdoMetaData[] metas)
            => NewMetaComposite<BdoMetaComposite>(name, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaComposite NewMetaComposite(
            string name,
            params (string Name, object Value)[] pairs)
            => NewMetaComposite<BdoMetaComposite>(name, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaComposite NewMetaComposite(
            params (string Name, object Value)[] pairs)
            => NewMetaComposite<BdoMetaComposite>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaComposite NewMetaComposite(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewMetaComposite<BdoMetaComposite>(name, triplets);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaComposite NewMetaComposite(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewMetaComposite<BdoMetaComposite>(triplets);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewMetaComposite<T>()
            where T : class, IBdoMetaComposite, new()
            => NewSet<T, IBdoMetaData>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaComposite<T>(string name)
            where T : class, IBdoMetaComposite, new()
            => NewMetaComposite<T>().WithName(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaComposite<T>(
            string name,
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaComposite, new()
        {
            var list = NewMetaComposite<T>();
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
        public static T NewMetaComposite<T>(
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaComposite, new()
            => NewMetaComposite<T>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaComposite<T>(
            string name,
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaComposite, new()
        {
            var list = NewMetaComposite<T>()
                .With(pairs)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaComposite<T>(
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaComposite, new()
            => NewMetaComposite<T>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaComposite<T>(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaComposite, new()
        {
            var list = NewMetaComposite<T>(
                triplets.Select(q => NewMeta(q.Name, q.ValueType, q.Value)).ToArray())
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaComposite<T>(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaComposite, new()
            => NewMetaComposite<T>(null, triplets);
    }
}
