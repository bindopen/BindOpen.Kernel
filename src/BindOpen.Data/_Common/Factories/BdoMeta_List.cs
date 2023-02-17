using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create data metaent list.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList()
            => NewList<BdoMetaList>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(string name)
            => NewList<BdoMetaList>(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(
            string name,
            params IBdoMetaData[] metas)
            => NewList<BdoMetaList>(name, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(params IBdoMetaData[] metas)
            => NewList<BdoMetaList>(metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(
            string name,
            params (string Name, object Value)[] pairs)
            => NewList<BdoMetaList>(name, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(
            params (string Name, object Value)[] pairs)
            => NewList<BdoMetaList>(pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewList<BdoMetaList>(name, triplets);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewList<BdoMetaList>(triplets);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>()
            where T : class, IBdoMetaList, new()
        {
            return BdoData.NewList<T, IBdoMetaData>();
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(string name)
            where T : class, IBdoMetaList, new()
        {
            return BdoData.NewList<T, IBdoMetaData>().WithName(name);
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(
            string name,
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaList, new()
        {
            var list = NewList<T>();
            list
                .With(metas)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaList, new()
            => NewList<T>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(
            string name,
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaList, new()
        {
            var list = NewList<T>(
                pairs.Select(q => New(q.Name, q.Value)).ToArray())
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaList, new()
            => NewList<T>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaList, new()
        {
            var list = NewList<T>(
                triplets.Select(q => New(q.Name, q.ValueType, q.Value)).ToArray())
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaList, new()
            => NewList<T>(null, triplets);
    }
}
