using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Configuration.Routines
{

    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the basic routine kinds.
    /// </summary>
    [Serializable()]
    [XmlType("BasicRoutineKind", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum BasicRoutineKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,
        /// <summary>
        /// RoutineConfiguration that checks that item is not empty.
        /// </summary>
        ItemMustNotBeEmpty,
        /// <summary>
        /// RoutineConfiguration that checks that item is in list.
        /// </summary>
        ItemMustBeInList
    }

    #endregion

}
