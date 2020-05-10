using System.Xml.Serialization;

namespace BindOpen.System.Diagnostics.Events
{
    /// <summary>
    /// This enumeration lists the possible event criticalities.
    /// </summary>
    [XmlType("EventCriticality", Namespace = "https://docs.bindopen.org/xsd")]
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
