using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.System.Diagnostics
{

    /// <summary>
    /// This enumeration lists log modes.
    /// </summary>
    [Serializable()]
    [XmlType("LoggerMode", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum LoggerMode
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
