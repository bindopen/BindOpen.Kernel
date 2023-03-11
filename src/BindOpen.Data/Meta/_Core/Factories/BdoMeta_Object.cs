using BindOpen.Data.Assemblies;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This static class provides methods to create data els.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            string name = null)
            => NewObject(name, null, null);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            string name,
            object item)
            => NewObject(name, null, item);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="classFullName">The class full name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            string name,
            IBdoClassReference reference,
            object item)
        {
            var el = new BdoMetaObject();

            el
                .WithName(name)
                .WithClassReference(reference)
                .WithData(item);

            return el;
        }

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaObject NewObject<T>(
            string name,
            T item)
            where T : class, new()
        {
            var classRef = BdoData.Class<T>();
            var el = NewObject(name, classRef, item);
            return el;
        }

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="item">The items to consider.</param>
        public static BdoMetaObject NewObject<T>(
            T item)
            where T : class, new()
            => NewObject<T>(null, item);
    }
}
