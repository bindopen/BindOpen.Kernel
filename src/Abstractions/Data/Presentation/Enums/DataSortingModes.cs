using System;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration represents the data sorting modes.
    /// </summary>
    [Flags]
    [XmlType("DataSortingModes", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum DataSortingModes
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
        /// Ascending.
        /// </summary>
        Ascending = 0x1 << 1,

        /// <summary>
        /// Descending.
        /// </summary>
        Descending = 0x1 << 2,

        /// <summary>
        /// Random.
        /// </summary>
        Random = 0x1 << 3,

        /// <summary>
        /// Any data sorting mode.
        /// </summary>
        Any = Ascending | Descending | Random
    }
}
