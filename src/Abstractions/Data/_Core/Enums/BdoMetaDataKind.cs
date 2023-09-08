﻿using System.Xml.Serialization;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This enumeration represents the possible kinds of meta data.
    /// </summary>
    [XmlType("BdoMetaDataKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum BdoMetaDataKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Entity.
        /// </summary>
        Entity,

        /// <summary>
        /// Object.
        /// </summary>
        Object,

        /// <summary>
        /// Scalar.
        /// </summary>
        Scalar,

        /// <summary>
        /// Collection.
        /// </summary>
        Collection,

        /// <summary>
        /// Set.
        /// </summary>
        Set
    }
}
