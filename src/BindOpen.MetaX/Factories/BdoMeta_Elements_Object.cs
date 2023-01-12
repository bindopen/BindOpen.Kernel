using BindOpen.Meta.Elements;

namespace BindOpen.Meta
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
            => NewObject<BdoMetaObject>(name, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            params object[] items)
            => NewObject<BdoMetaObject>(items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="classFullName">The class full name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            string name,
            string classFullName,
            params object[] items)
            => NewObject<BdoMetaObject>(name, classFullName, items);

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewObject<T>(
        string name,
        params object[] items)
        where T : class, IBdoMetaObject, new()
        => NewObject<T>(name, null, null, null, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static T NewObject<T>(
            params object[] items)
            where T : class, IBdoMetaObject, new()
            => NewObject<T>(null, null, null, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="classFullName">The class full name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewObject<T>(
            string name,
            string classFullName,
            params object[] items)
            where T : class, IBdoMetaObject, new()
        {
            var el = new T();
            el.WithName(name);
            el.WithClassFullName(classFullName);
            el.WithItem(items);

            return el;
        }
    }
}
