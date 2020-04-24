using System.Xml.Serialization;

namespace BindOpen.System.Processing
{

    /// <summary>
    /// This enumeration represents the possible process execution statuses.
    /// </summary>
    [XmlType("ProcessExecutionStatus", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
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
        /// Stopped with errors (configuration error).
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
