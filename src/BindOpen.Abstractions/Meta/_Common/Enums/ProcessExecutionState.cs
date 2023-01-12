using System.Xml.Serialization;

namespace BindOpen.Meta
{
    /// <summary>
    /// This enumeration represents the possible process execution states.
    /// </summary>
    [XmlType("ProcessExecutionState", Namespace = "https://docs.bindopen.org/xsd")]
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
