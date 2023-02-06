using System.Xml.Serialization;

namespace BindOpen.Extensions
{

    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration lists all the possible kinds of carriers.
    /// </summary>
    [XmlType("EntityKind", Namespace = "https://xsd.bindopen.org")]
    public enum BdoEntityKind
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
        /// Data entity.
        /// </summary>
        Entity,

        /// <summary>
        /// Data zone.
        /// </summary>
        Zone,

        /// <summary>
        /// Simple item.
        /// </summary>
        SimpleItem
    }

    #endregion

}
