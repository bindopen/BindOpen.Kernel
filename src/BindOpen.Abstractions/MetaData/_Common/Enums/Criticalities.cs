using System.Xml.Serialization;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This enumeration lists the possible event criticalities.
    /// </summary>
    [XmlType("Criticalities", Namespace = "https://xsd.bindopen.org")]
    public enum Criticalities
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
