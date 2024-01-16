using System;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration lists the possible actions of priorities.
    /// </summary>
    [Flags]
    [XmlType("ActionPriorities", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum ActionPriorities
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Inherited.
        /// </summary>
        Inherited = 0x1 << 0,

        /// <summary>
        /// Low.
        /// </summary>
        Low = 0x01 << 1,

        /// <summary>
        /// Medium.
        /// </summary>
        Medium = 0x01 << 2,

        /// <summary>
        /// High.
        /// </summary>
        High = 0x01 << 3,

        /// <summary>
        /// Very high.
        /// </summary>
        VeryHigh = 0x01 << 4,

        /// <summary>
        /// Any action priority.
        /// </summary>
        Any = Low | Medium | High | VeryHigh
    }

}
