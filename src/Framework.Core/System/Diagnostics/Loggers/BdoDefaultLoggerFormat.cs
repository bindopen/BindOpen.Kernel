using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{
    /// <summary>
    /// This enumeration lists the possible logger formats.
    /// </summary>
    [Serializable()]
    [XmlType("LoggerFormat", Namespace = "https://bindopen.org/xsd")]
    public enum BdoDefaultLoggerFormat
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
        /// Custom.
        /// </summary>
        Custom,

        /// <summary>
        /// Json.
        /// </summary>
        Json,

        /// <summary>
        /// Snap.
        /// </summary>
        Snap,

        /// <summary>
        /// Report.
        /// </summary>
        Report,

        /// <summary>
        /// YAML.
        /// </summary>
        Yaml,

        /// <summary>
        /// XML.
        /// </summary>
        Xml,
    };
}
