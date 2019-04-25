using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.System.Diagnostics.Events
{

    /// <summary>
    /// This enumeration lists the possible event criticalities.
    /// </summary>
    [Serializable()]
    [XmlType("EventCriticality", Namespace = "https://bindopen.org/xsd")]
    public enum EventCriticality
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
