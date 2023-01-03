using System;
using System.Xml.Serialization;

namespace BindOpen.Logging
{
    /// <summary>
    /// This enumeration lists the possible event kinds.
    /// </summary>
    [Flags()]
    [XmlType("EventKinds", Namespace = "https://docs.bindopen.org/xsd")]
    public enum EventKinds
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
        Any = EventKinds.Checkpoint | EventKinds.Error | EventKinds.Exception | EventKinds.Message | EventKinds.Other | EventKinds.Warning,
    };
}
