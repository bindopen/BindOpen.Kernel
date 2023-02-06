﻿using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoMetaDataExtensions
    {
        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData ToMetaData(
            this object obj,
            string name = null,
            IBdoScope scope = null,
            IBdoLog log = null)
        {
            var meta = BdoMeta.New(name, obj);
            if (meta is IBdoMetaObject metaObj)
            {
                metaObj.With(
                    obj.ToMetaArray(
                        metaObj.GetClassType(scope, log)));
            }

            return meta;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaData[] ToMetaArray(
            this object obj,
            Type type = null,
            bool onlyMetaAttributes = true)
            => obj.ToMetaList(type, onlyMetaAttributes)?.ToArray();
    }
}