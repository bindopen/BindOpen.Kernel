using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Entities
{

    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration lists all the possible kinds of entities.
    /// </summary>
    [Serializable()]
    [XmlType("EntityKind", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum EntityKind
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
        /// Data carrier.
        /// </summary>
        Carrier,
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
