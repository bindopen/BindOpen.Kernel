using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Items
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the data element itemization modes.
    /// </summary>
    [Serializable()]
    [XmlType("DataItemizationMode", Namespace = "https://bindopen.org/xsd")]
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
        /// Valued.
        /// </summary>
        Valued,

        /// <summary>
        /// Referenced.
        /// </summary>
        Referenced,

        /// <summary>
        /// Script.
        /// </summary>
        Script
    }

    #endregion

}
