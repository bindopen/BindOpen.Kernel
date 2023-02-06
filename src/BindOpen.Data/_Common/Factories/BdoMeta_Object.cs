﻿using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;

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
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            string name = null)
            => NewObject(name, null, null);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            string name,
            object item)
            => NewObject(name, null, item);

        /// <summary>
        /// Initializes a new object el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="classFullName">The class full name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaObject NewObject(
            string name,
            IBdoClassReference reference,
            object item)
        {
            var el = new BdoMetaObject();
            el.WithName(name);
            el.WithClassReference(reference);
            el.WithData(item);

            //el.ClassReference .ClassFilter.AddedValues.Add(meta.GetType().ToString());

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
        /// <param name="item">The items to consider.</param>
        public static BdoMetaObject NewObject<T>(
            T item)
            where T : class, new()
            => NewObject<T>(null, item);
    }
}