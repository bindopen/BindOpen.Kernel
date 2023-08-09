using BindOpen.System.Data.Meta;
using System;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This static class provides methods to create data els.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaObject NewMetaObject(
            string name = null)
            => NewMetaObject(name, null, null);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaObject NewMetaObject(
            string name,
            object item)
            => NewMetaObject(name, null, item);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="classFullName">The class full name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaObject NewMetaObject(
            string name,
            Type type,
            object item)
        {
            var el = new BdoMetaObject();

            el
                .WithName(name)
                .WithDataType(DataValueTypes.Object, type)
                .WithData(item);

            return el;
        }

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static TBdoMetaObject<T> NewMetaObject<T>(
            string name,
            T item)
            where T : class
        {
            return NewMetaObject<T, TBdoMetaObject<T>>(name, item);
        }

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="item">The items to consider.</param>
        public static TBdoMetaObject<T> NewMetaObject<T>(
            T item = default)
            where T : class
            => NewMetaObject<T, TBdoMetaObject<T>>(null, item);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static TMeta NewMetaObject<TItem, TMeta>(
            string name,
            TItem item)
            where TItem : class
            where TMeta : TBdoMetaObject<TItem>, new()
        {
            var el = new TMeta();

            el
                .WithName(name)
                .WithDataType(DataValueTypes.Object, typeof(TItem))
                .WithData(item);

            return el;
        }
    }
}
