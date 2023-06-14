using System.Xml.Serialization;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This enumeration lists the possible event criticalities.
    /// </summary>
    [XmlType("Criticalities", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
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
