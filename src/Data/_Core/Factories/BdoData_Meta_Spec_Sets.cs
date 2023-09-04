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
        public static BdoSpecSet NewSpecSet()
            => NewSpecSet<BdoSpecSet>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSpecSet NewSpecSet(string name)
            => NewSpecSet<BdoSpecSet>(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSpecSet NewSpecSet(
            params IBdoSpec[] metas)
            => NewSpecSet<BdoSpecSet>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSpecSet NewSpecSet(
            string name,
            params IBdoSpec[] metas)
            => NewSpecSet<BdoSpecSet>(name, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSpecSet NewSpecSet(
            string name,
            params (string Name, DataValueTypes ValueType)[] pairs)
            => NewSpecSet<BdoSpecSet>(name, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSpecSet NewSpecSet(
            params (string Name, DataValueTypes ValueType)[] pairs)
            => NewSpecSet<BdoSpecSet>(pairs);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewSpecSet<T>()
            where T : class, IBdoSpecSet, new()
            => NewItemSet<T, IBdoSpec>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSpecSet<T>(string name)
            where T : class, IBdoSpecSet, new()
            => NewSpecSet<T>().WithName(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSpecSet<T>(
            string name,
            params IBdoSpec[] metas)
            where T : class, IBdoSpecSet, new()
        {
            var list = NewSpecSet<T>();
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
        public static T NewSpecSet<T>(
            params IBdoSpec[] metas)
            where T : class, IBdoSpecSet, new()
            => NewSpecSet<T>(null, metas);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSpecSet<T>(
            string name,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : class, IBdoSpecSet, new()
        {
            var list = NewSpecSet<T>(
                pairs.Select(q => NewSpec(q.Name, q.ValueType)).ToArray())
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSpecSet<T>(
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : class, IBdoSpecSet, new()
            => NewSpecSet<T>(null, pairs);
    }
}
