using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create data els.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            string name,
            params object[] items)
            => NewObject(name, null, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            params object[] items)
            => NewObject(null, null, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="classFullName">The class full name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            string name,
            IBdoClassReference reference,
            params object[] items)
        {
            var el = new BdoMetaObject();
            el.WithName(name);
            el.WithClassReference(reference);
            el.WithItems(items);

            return el;
        }

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject<T>(
            string name,
            params T[] items)
            where T : class, new()
        {
            var classReference = BdoData.Class<T>();
            var el = NewObject(name, classReference, items?.Cast<object>().ToArray());
            return el;
        }

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject<T>(
            params T[] items)
            where T : class, new()
            => NewObject<T>(null, items);
    }
}
