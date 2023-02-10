using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration represents the data element itemization modes.
    /// </summary>
    [XmlType("DataValueMode", Namespace = "https://xsd.bindopen.org")]
    public enum DataValueMode
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Value.
        /// </summary>
        Value,

        /// <summary>
        /// Reference.
        /// </summary>
        Reference
    }
}
