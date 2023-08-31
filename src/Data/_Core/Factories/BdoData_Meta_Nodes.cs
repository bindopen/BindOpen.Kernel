using BindOpen.System.Data.Meta;
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
        public static BdoMetaNode NewMetaNode()
            => NewMetaNode<BdoMetaNode>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewMetaNode(
            string name)
            => NewMetaNode<BdoMetaNode>(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewMetaNode(
            params IBdoMetaData[] metas)
            => NewMetaNode<BdoMetaNode>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewMetaNode(
            string name,
            params IBdoMetaData[] metas)
            => NewMetaNode<BdoMetaNode>(name, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewMetaNode(
            string name,
            params (string Name, object Value)[] pairs)
            => NewMetaNode<BdoMetaNode>(name, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewMetaNode(
            params (string Name, object Value)[] pairs)
            => NewMetaNode<BdoMetaNode>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewMetaNode(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewMetaNode<BdoMetaNode>(name, triplets);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewMetaNode(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewMetaNode<BdoMetaNode>(triplets);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewMetaNode<T>()
            where T : class, IBdoMetaNode, new()
            => NewSet<T, IBdoMetaData>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaNode<T>(string name)
            where T : class, IBdoMetaNode, new()
            => NewMetaNode<T>().WithName(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaNode<T>(
            string name,
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaNode, new()
        {
            var list = NewMetaNode<T>();
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
        public static T NewMetaNode<T>(
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaNode, new()
            => NewMetaNode<T>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaNode<T>(
            string name,
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaNode, new()
        {
            var list = NewMetaNode<T>()
                .With(pairs)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaNode<T>(
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaNode, new()
            => NewMetaNode<T>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaNode<T>(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaNode, new()
        {
            var list = NewMetaNode<T>(
                triplets.Select(q => NewMeta(q.Name, q.ValueType, q.Value)).ToArray())
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewMetaNode<T>(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaNode, new()
            => NewMetaNode<T>(null, triplets);
    }
}
