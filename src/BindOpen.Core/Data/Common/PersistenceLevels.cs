using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This enumeration represents the possible persistence levels.
    /// </summary>
    [XmlType("PersistenceLevels", Namespace = "https://docs.bindopen.org/xsd")]
    [Flags]
    public enum PersistenceLevels
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// None.
        /// </summary>
        None = 0x01 << 0,

        /// <summary>
        /// The information remains alive.
        /// </summary>
        Singleton = 0x01 << 1,

        /// <summary>
        /// The information remains alive until the session ends.
        /// </summary>
        Scoped = 0x01 << 2,

        /// <summary>
        /// The information remains alive until the request ends.
        /// </summary>
        Transient = 0x01 << 3,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Singleton | Scoped | Transient
    }
}
