using System;
using System.Xml.Serialization;

namespace BindOpen.Logging
{
    /// <summary>
    /// This enumerates the possible event kinds.
    /// </summary>
    [Flags()]
    [XmlType("BdoEventKinds", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum BdoEventKinds
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Message.
        /// </summary>
        Message = 1 << 0,

        /// <summary>
        /// Checkpoint.
        /// </summary>
        Checkpoint = 1 << 1,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning = 1 << 2,

        /// <summary>
        /// Error.
        /// </summary>
        Error = 1 << 3,

        /// <summary>
        /// Exception.
        /// </summary>
        Exception = 1 << 4,

        /// <summary>
        /// Other.
        /// </summary>
        Other = 1 << 5,

        /// <summary>
        /// None.
        /// </summary>
        Any = Checkpoint | Error | Exception | Message | Other | Warning
    };
}