﻿using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Data.Schema;
using BindOpen.Scoping;
using System;

namespace BindOpen.Data.Meta.Reflection
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        // Metadata

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData ToMeta(
            this object obj,
            IBdoScope scope,
            Type type,
            string name = null,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
        {
            IBdoMetaData meta;

            if (obj == null && !includeNullValues) return null;

            if (type != null && typeof(IBdoMetaData).IsAssignableFrom(type) == true && obj != null)
            {
                meta = obj as IBdoMetaData;

                if (meta != null)
                {
                    meta.Name = name;
                }
            }
            else
            {
                meta = BdoData.NewMeta(name, type, obj);
            }

            meta?
                .WithScope(scope)
                .UpdateTree(onlyMetaAttributes, includeNullValues);

            return meta;
        }

        public static IBdoMetaData ToMeta(
            this object obj,
            Type type,
            string name = null,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
            => obj.ToMeta(null, type, name, onlyMetaAttributes, includeNullValues);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData ToMeta(
            this object obj,
            IBdoScope scope,
            string name = null,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
        {
            return obj.ToMeta(scope, null, name, onlyMetaAttributes, includeNullValues);
        }

        public static IBdoMetaData ToMeta(
            this object obj,
            string name = null,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
            => obj.ToMeta(null as IBdoScope, name, onlyMetaAttributes, includeNullValues);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T ToMeta<T>(
            this object obj,
            IBdoScope scope,
            string name = null,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
            where T : IBdoMetaData
            => obj.ToMeta(scope, null, name, onlyMetaAttributes, includeNullValues).As<T>();

        public static T ToMeta<T>(
            this object obj,
            string name = null,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
            where T : IBdoMetaData
            => obj.ToMeta(null, null, name, onlyMetaAttributes, includeNullValues).As<T>();

        // Specification

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T ToSpec<T>(
            this Type type,
            string name = null,
            bool onlyMetaAttributes = true)
            where T : IBdoSchema, new()
        {
            var obj = AssemblyHelper.CreateInstance(type);

            var schema = obj.ToMeta(name, onlyMetaAttributes).ToSpec<T>();

            return schema;
        }

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoSchema ToSpec(
            this Type type,
            string name = null,
            bool onlyMetaAttributes = true)
            => type.ToSpec<BdoSchema>(name, onlyMetaAttributes);
    }
}
