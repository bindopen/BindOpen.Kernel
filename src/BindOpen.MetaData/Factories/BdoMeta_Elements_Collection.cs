using BindOpen.Meta.Elements;
using System.Linq;

namespace BindOpen.Meta
{
    /// <summary>
    /// This static class provides methods to create data elems.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static BdoMetaCollection NewCollection(
            params IBdoMetaElement[] elems)
            => NewCollection<BdoMetaCollection>(elems);

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static BdoMetaCollection NewCollection(
            params IBdoMetaElement[][] elems)
            => NewCollection<BdoMetaCollection>(elems.SelectMany(q => q).ToArray());

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static BdoMetaCollection NewCollection(
            string name,
            params IBdoMetaElement[] elems)
            => NewCollection<BdoMetaCollection>(name, elems);

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static BdoMetaCollection NewCollection(
            string name,
            params IBdoMetaElement[][] elems)
            => NewCollection<BdoMetaCollection>(name, elems.SelectMany(q => q).ToArray());

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new collection el.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static T NewCollection<T>(
            params IBdoMetaElement[] elems)
            where T : class, IBdoMetaCollection, new()
            => NewCollection<T>(null as string, elems);

        /// <summary>
        /// Initializes a new collection el.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static T NewCollection<T>(
            params IBdoMetaElement[][] elems)
            where T : class, IBdoMetaCollection, new()
            => NewCollection<T>(null as string, elems.SelectMany(q => q).ToArray());

        /// <summary>
        /// Initializes a new collection el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="elems">The elems to consider.</param>
        public static T NewCollection<T>(
            string name,
            params IBdoMetaElement[] elems)
            where T : class, IBdoMetaCollection, new()
        {
            var el = new T();
            el.WithName(name);
            el.WithItem(elems);

            return el;
        }

        /// <summary>
        /// Initializes a new collection el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="elems">The elems to consider.</param>
        public static T NewCollection<T>(
            string name,
            params IBdoMetaElement[][] elems)
            where T : class, IBdoMetaCollection, new()
            => NewCollection<T>(name, elems.SelectMany(q => q).ToArray());
    }
}
