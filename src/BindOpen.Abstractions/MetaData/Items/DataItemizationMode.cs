using System.Xml.Serialization;

namespace BindOpen.MetaData.Items
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

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
