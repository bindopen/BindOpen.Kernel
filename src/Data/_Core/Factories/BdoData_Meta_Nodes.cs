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
        public static BdoMetaNode NewNode()
            => NewNode<BdoMetaNode>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewNode(
            string name)
            => NewNode<BdoMetaNode>(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewNode(
            params IBdoMetaData[] metas)
            => NewNode<BdoMetaNode>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewNode(
            string name,
            params IBdoMetaData[] metas)
            => NewNode<BdoMetaNode>(name, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewNode(
            string name,
            params (string Name, object Value)[] pairs)
            => NewNode<BdoMetaNode>(name, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewNode(
            params (string Name, object Value)[] pairs)
            => NewNode<BdoMetaNode>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewNode(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewNode<BdoMetaNode>(name, triplets);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaNode NewNode(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewNode<BdoMetaNode>(triplets);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewNode<T>()
            where T : class, IBdoMetaNode, new()
            => NewItemSet<T, IBdoMetaData>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewNode<T>(string name)
            where T : class, IBdoMetaNode, new()
            => NewNode<T>().WithName(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewNode<T>(
            string name,
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaNode, new()
        {
            var list = NewNode<T>();
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
        public static T NewNode<T>(
            params IBdoMetaData[] metas)
            where T : class, IBdoMetaNode, new()
            => NewNode<T>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewNode<T>(
            string name,
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaNode, new()
        {
            var list = NewNode<T>()
                .With(pairs)
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewNode<T>(
            params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaNode, new()
            => NewNode<T>(null, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewNode<T>(
            string name,
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaNode, new()
        {
            var list = NewNode<T>(
                triplets.Select(q => NewMeta(q.Name, q.ValueType, q.Value)).ToArray())
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewNode<T>(
            params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaNode, new()
            => NewNode<T>(null, triplets);
    }
}
