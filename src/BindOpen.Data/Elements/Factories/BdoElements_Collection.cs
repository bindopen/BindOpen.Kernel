using System.Linq;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data els.
    /// </summary>
    public static partial class BdoElements
    {
        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static CollectionElement NewCollection(
            params IBdoElement[] els)
            => NewCollection<CollectionElement>(els);

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static CollectionElement NewCollection(
            params IBdoElement[][] els)
            => NewCollection<CollectionElement>(els.SelectMany(q => q).ToArray());

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static CollectionElement NewCollection(
            string name,
            params IBdoElement[] els)
            => NewCollection<CollectionElement>(name, els);

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static CollectionElement NewCollection(
            string name,
            params IBdoElement[][] els)
            => NewCollection<CollectionElement>(name, els.SelectMany(q => q).ToArray());

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new collection el.
        /// </summary>
        /// <param name="els">The els to consider.</param>
        public static T NewCollection<T>(
            params IBdoElement[] els)
            where T : class, ICollectionElement, new()
            => NewCollection<T>(null as string, els);

        /// <summary>
        /// Initializes a new collection el.
        /// </summary>
        /// <param name="els">The els to consider.</param>
        public static T NewCollection<T>(
            params IBdoElement[][] els)
            where T : class, ICollectionElement, new()
            => NewCollection<T>(null as string, els.SelectMany(q => q).ToArray());

        /// <summary>
        /// Initializes a new collection el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="els">The els to consider.</param>
        public static T NewCollection<T>(
            string name,
            params IBdoElement[] els)
            where T : class, ICollectionElement, new()
        {
            var el = new T();
            el.WithName(name);
            el.WithItem(els);

            return el;
        }

        /// <summary>
        /// Initializes a new collection el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="els">The els to consider.</param>
        public static T NewCollection<T>(
            string name,
            params IBdoElement[][] els)
            where T : class, ICollectionElement, new()
            => NewCollection<T>(name, els.SelectMany(q => q).ToArray());
    }
}
