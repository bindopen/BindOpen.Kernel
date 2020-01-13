using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.System.Processing
{
    /// <summary>
    /// This enumeration represents the possible process execution states.
    /// </summary>
    [Serializable()]
    [XmlType("ProcessExecutionState", Namespace = "https://bindopen.org/xsd")]
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
