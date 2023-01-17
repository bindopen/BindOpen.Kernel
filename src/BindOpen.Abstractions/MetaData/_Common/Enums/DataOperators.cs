using System;
using System.Xml.Serialization;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This enumeration represents the possible data operators.
    /// </summary>
    [Flags]
    [XmlType("DataOperators", Namespace = "https://xsd.bindopen.org")]
    public enum DataOperators
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0x01 << 0,

        /// <summary>
        /// Exists.
        /// </summary>
        Exists = 0x1 << 1,

        /// <summary>
        /// Contains.
        /// </summary>
        Contains = 0x1 << 2,

        /// <summary>
        /// Has.
        /// </summary>
        Has = 0x1 << 3,

        /// <summary>
        /// In.
        /// </summary>
        In = 0x1 << 4,

        /// <summary>
        /// Equal.
        /// </summary>
        Equal = 0x1 << 5,

        /// <summary>
        /// Different.
        /// </summary>
        Different = 0x1 << 6,

        /// <summary>
        /// Lesser.
        /// </summary>
        Lesser = 0x1 << 7,

        /// <summary>
        /// Lesser or equal.
        /// </summary>
        LesserOrEqual = Lesser | Equal,

        /// <summary>
        /// Greater.
        /// </summary>
        Greater = 0x1 << 8,

        /// <summary>
        /// Greater or equal.
        /// </summary>
        GreaterOrEqual = Greater | Equal,

        /// <summary>
        /// Any operator.
        /// </summary>
        Any = Exists | Contains | Has | In | Equal | Different | Lesser | LesserOrEqual | Greater | GreaterOrEqual
    }
}
