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
    [XmlType("DataSortingMode", Namespace = "https://bindopen.org/xsd")]
    public enum DataSortingMode
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Ascending.
        /// </summary>
        Ascending,

        /// <summary>
        /// Descending.
        /// </summary>
        /// 
        Descending,

        /// <summary>
        /// Random.
        /// </summary>
        Random
    }

    #endregion

}
