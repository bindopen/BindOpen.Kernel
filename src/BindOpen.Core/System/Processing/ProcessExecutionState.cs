using System.Xml.Serialization;

namespace BindOpen.System.Processing
{
    /// <summary>
    /// This enumeration represents the possible process execution states.
    /// </summary>
    [XmlType("ProcessExecutionState", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    public enum ProcessExecutionState
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Pending.
        /// </summary>
        Pending,

        /// <summary>
        /// Ended.
        /// </summary>
        Ended
    }

}
