using System.Xml.Serialization;

namespace BindOpen.Meta.Items
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the data element itemization modes.
    /// </summary>
    [XmlType("DataItemizationMode", Namespace = "https://docs.bindopen.org/xsd")]
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
