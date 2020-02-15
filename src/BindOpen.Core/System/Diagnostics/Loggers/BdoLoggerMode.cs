using System;
using System.Xml.Serialization;

namespace BindOpen.System.Diagnostics
{
    /// <summary>
    /// This enumeration lists logger modes.
    /// </summary>
    [Serializable()]
    [XmlType("LoggerMode", Namespace = "https://bindopen.org/xsd")]
    public enum BdoLoggerMode
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Off.
        /// </summary>
        Off,

        /// <summary>
        /// Manual.
        /// </summary>
        Manual,

        /// <summary>
        /// Automatic.
        /// </summary>
        Auto
    }
}
