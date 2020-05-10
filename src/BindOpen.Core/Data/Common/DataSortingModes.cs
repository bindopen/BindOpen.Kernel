using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the data sorting modes.
    /// </summary>
    [XmlType("DataSortingModes", Namespace = "https://docs.bindopen.org/xsd")]
    [Flags]
    public enum DataSortingModes
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// None.
        /// </summary>
        None = 0x01 << 0,

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
        /// Any data operator.
        /// </summary>
        Any = Ascending | Descending | Random
    }

    #endregion
}
