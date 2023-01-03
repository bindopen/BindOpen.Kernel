namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data els.
    /// </summary>
    public static partial class BdoElements
    {
        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ObjectElement NewObject(
            string name,
            params object[] items)
            => NewObject<ObjectElement>(name, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static ObjectElement NewObject(
            params object[] items)
            => NewObject<ObjectElement>(items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="classFullName">The class full name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ObjectElement NewObject(
            string name,
            string classFullName,
            params object[] items)
            => NewObject<ObjectElement>(name, classFullName, items);

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewObject<T>(
        string name,
        params object[] items)
        where T : class, IObjectElement, new()
        => NewObject<T>(name, null, null, null, items);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static T NewObject<T>(
            params object[] items)
            where T : class, IObjectElement, new()
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
            where T : class, IObjectElement, new()
        {
            var el = new T();
            el.WithName(name);
            el.WithClassFullName(classFullName);
            el.WithItem(items);

            return el;
        }
    }
}
