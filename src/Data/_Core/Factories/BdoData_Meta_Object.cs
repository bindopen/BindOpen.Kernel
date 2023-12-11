using BindOpen.Kernel.Data.Meta;
using System;

namespace BindOpen.Kernel.Data
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
            Type type,
            object item,
            DataValueTypes valueType = DataValueTypes.Object)
        {
            var el = new BdoMetaObject();

            el
                .WithName(name)
                .WithDataType(type)
                .WithData(item);

            if (el?.DataType?.ValueType == DataValueTypes.None)
            {
                el.DataType.ValueType = DataValueTypes.Object;
            }

            return el;
        }

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static TBdoMetaObject<T> NewObject<T>(
            string name,
            T item)
            where T : class
        {
            return NewObject<T, TBdoMetaObject<T>>(name, item);
        }

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param key="item">The items to consider.</param>
        public static TBdoMetaObject<T> NewObject<T>(
            T item = default)
            where T : class
            => NewObject<T, TBdoMetaObject<T>>(null, item);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static TMeta NewObject<TItem, TMeta>(
            string name,
            TItem item)
            where TItem : class
            where TMeta : TBdoMetaObject<TItem>, new()
        {
            var el = new TMeta();

            el
                .WithName(name)
                .WithDataType(typeof(TItem))
                .WithData(item);

            if (el?.DataType?.ValueType == DataValueTypes.None)
            {
                el.DataType.ValueType = DataValueTypes.Object;
            }

            return el;
        }
    }
}
