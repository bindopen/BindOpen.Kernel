using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create data metaent schema.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static BdoSchemaSet NewSchemaSet()
            => NewSchemaSet<BdoSchemaSet>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSchemaSet NewSchemaSet(string name)
            => NewSchemaSet<BdoSchemaSet>(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSchemaSet NewSchemaSet(
        params IBdoSchema[] specs)
            => NewSchemaSet<BdoSchemaSet>(null, specs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSchemaSet NewSchemaSet(
            string name,
            params IBdoSchema[] specs)
            => NewSchemaSet<BdoSchemaSet>(name, specs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSchemaSet NewSchemaSet(
            string name,
            params (string Name, DataValueTypes ValueType)[] pairs)
            => NewSchemaSet<BdoSchemaSet>(name, pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoSchemaSet NewSchemaSet(
            params (string Name, DataValueTypes ValueType)[] pairs)
            => NewSchemaSet<BdoSchemaSet>(pairs);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewSchemaSet<T>()
            where T : class, IBdoSchemaSet, new()
            => NewItemSet<T, IBdoSchema>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSchemaSet<T>(string name)
            where T : class, IBdoSchemaSet, new()
            => NewSchemaSet<T>().WithName(name);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="metas">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSchemaSet<T>(
            string name,
            params IBdoSchema[] metas)
            where T : class, IBdoSchemaSet, new()
        {
            var list = NewSchemaSet<T>();
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
        public static T NewSchemaSet<T>(
            params IBdoSchema[] specs)
            where T : class, IBdoSchemaSet, new()
            => NewSchemaSet<T>(null, specs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSchemaSet<T>(
            string name,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : class, IBdoSchemaSet, new()
        {
            var list = NewSchemaSet<T>(
                pairs.Select(q => NewSchema(q.Name, q.ValueType)).ToArray())
                .WithName(name);

            return list;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param key="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSchemaSet<T>(
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : class, IBdoSchemaSet, new()
            => NewSchemaSet<T>(null, pairs);
    }
}
