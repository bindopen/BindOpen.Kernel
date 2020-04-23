using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This enumeration represents the possible data operators.
    /// </summary>
    [XmlType("DataOperator", Namespace = "https://bindopen.org/xsd")]
    public enum DataOperator
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Exists.
        /// </summary>
        Exists,

        /// <summary>
        /// Contains.
        /// </summary>
        Contains,

        /// <summary>
        /// Has.
        /// </summary>
        Has,

        /// <summary>
        /// In.
        /// </summary>
        In,

        /// <summary>
        /// Equal.
        /// </summary>
        Equal,

        /// <summary>
        /// Different.
        /// </summary>
        Different,

        /// <summary>
        /// Lesser.
        /// </summary>
        Lesser,

        /// <summary>
        /// Lesser or equal.
        /// </summary>
        LesserOrEqual,

        /// <summary>
        /// Greater.
        /// </summary>
        Greater,

        /// <summary>
        /// Greater or equal.
        /// </summary>
        GreaterOrEqual
    }
}
