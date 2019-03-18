using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions
{

    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the possible basic extension unique names.
    /// </summary>
    [Serializable()]
    [XmlType("BasicExtensionUniqueName", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum BasicExtensionUniqueName
    {
        /// <summary>
        /// Standard.
        /// </summary>
        Standard,
        /// <summary>
        /// Database.
        /// </summary>
        Database,
        /// <summary>
        /// Session.
        /// </summary>
        Session,
        /// <summary>
        /// Forms and grids.
        /// </summary>
        FormsGrids,
        /// <summary>
        /// Messages.
        /// </summary>
        Messages,
        /// <summary>
        /// BPM.
        /// </summary>
        Bpm
    }

    #endregion

}
