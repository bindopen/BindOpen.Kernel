using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Common
{
    /// <summary>
    /// This enumeration represents the possible persistence levels.
    /// </summary>
    [Serializable()]
    [XmlType("PersistenceLevel", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum PersistenceLevel
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// The information remains alive.
        /// </summary>
        Singleton,

        /// <summary>
        /// The information remains alive until the session ends.
        /// </summary>
        Scoped,

        /// <summary>
        /// The information remains alive until the request ends.
        /// </summary>
        Transient
    }
}
