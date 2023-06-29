using System;
using System.Xml.Serialization;

namespace Fidal.MarkManager.Domain.Models.Enums
{
    /// <summary>
    /// This enumeration lists the possible modes of query result.
    /// </summary>
    [Flags]
    [XmlType("QueryResultModes", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    public enum QueryResultModes
    {
        /// <summary>
        /// Full.
        /// </summary>
        Full = 0,

        /// <summary>
        /// Partial.
        /// </summary>
        Partial = 0x01 << 0,

        /// <summary>
        /// Quick.
        /// </summary>
        Quick = 0x01 << 1,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Quick | Partial | Full
    }
}