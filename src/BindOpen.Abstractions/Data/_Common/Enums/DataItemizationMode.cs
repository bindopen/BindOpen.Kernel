using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration represents the data element itemization modes.
    /// </summary>
    [XmlType("DataItemizationMode", Namespace = "https://xsd.bindopen.org")]
    public enum DataItemizationMode
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
        Reference,

        /// <summary>
        /// Expression.
        /// </summary>
        Expression
    }
}
