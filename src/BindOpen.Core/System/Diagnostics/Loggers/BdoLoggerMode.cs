using System.Xml.Serialization;

namespace BindOpen.System.Diagnostics
{
    /// <summary>
    /// This enumeration lists logger modes.
    /// </summary>
    [XmlType("LoggerMode", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
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
