using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.System.Diagnostics.Events
{
    /// <summary>
    /// This enumeration lists the possible event criticalities.
    /// </summary>
    [Serializable()]
    [XmlType("EventCriticality", Namespace = "https://bindopen.org/xsd")]
    public enum BdoEventCriticality
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Low.
        /// </summary>
        Low,

        /// <summary>
        /// Medium.
        /// </summary>
        Medium,

        /// <summary>
        /// High.
        /// </summary>
        High,

        /// <summary>
        /// Very high.
        /// </summary>
        VeryHigh
    };

}
