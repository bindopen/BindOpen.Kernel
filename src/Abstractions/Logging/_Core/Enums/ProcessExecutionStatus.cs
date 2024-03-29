﻿using System.Xml.Serialization;

namespace BindOpen.Logging
{
    /// <summary>
    /// This enumeration represents the possible process execution statuses.
    /// </summary>
    [XmlType("ProcessExecutionStatus", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum ProcessExecutionStatus
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Nothing done.
        /// </summary>
        NothingDone,

        /// <summary>
        /// Processing.
        /// </summary>
        Processing,

        /// <summary>
        /// Waiting.
        /// </summary>
        Waiting,

        /// <summary>
        /// Paused.
        /// </summary>
        Paused,

        /// <summary>
        /// Queueing.
        /// </summary>
        Queueing,

        /// <summary>
        /// Stopped.
        /// </summary>
        Stopped,

        /// <summary>
        /// Stopped with exceptions (system error).
        /// </summary>
        Stopped_Exception,

        /// <summary>
        /// Stopped with errors (config error).
        /// </summary>
        Stopped_Error,

        /// <summary>
        /// Stopped by user.
        /// </summary>
        Stopped_User,

        /// <summary>
        /// Completed.
        /// </summary>
        Completed
    }
}
