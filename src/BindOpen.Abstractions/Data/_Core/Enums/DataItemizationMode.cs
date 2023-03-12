using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration represents the data element itemization modes.
    /// </summary>
    [XmlType("DataMode", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    public enum DataMode
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
