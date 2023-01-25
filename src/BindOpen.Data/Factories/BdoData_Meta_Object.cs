using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create data els.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewMetaObject(
            string name,
            params object[] items)
            => NewMetaObject<BdoMetaObject>(name, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewMetaObject(
            params object[] items)
            => NewMetaObject<BdoMetaObject>(items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="classFullName">The class full name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewMetaObject(
            string name,
            string classFullName,
            params object[] items)
            => NewMetaObject<BdoMetaObject>(name, classFullName, items);

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewMetaObject<T>(
            string name,
            params object[] items)
            where T : class, IBdoMetaObject, new()
            => NewMetaObject<T>(name, null, null, null, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static T NewMetaObject<T>(
            params object[] items)
            where T : class, IBdoMetaObject, new()
            => NewMetaObject<T>(null, null, null, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="classFullName">The class full name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewMetaObject<T>(
            string name,
            string classFullName,
            params object[] items)
            where T : class, IBdoMetaObject, new()
        {
            var el = new T();
            el.WithName(name);
            el.WithClassFullName(classFullName);
            el.WithItems(items);

            return el;
        }
    }
}
