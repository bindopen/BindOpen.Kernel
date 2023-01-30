using System.Xml.Serialization;

namespace BindOpen.Data
{

    /// <summary>
    /// This enumeration represents the possible process execution statuses.
    /// </summary>
    [XmlType("ProcessExecutionStatus", Namespace = "https://xsd.bindopen.org")]
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
