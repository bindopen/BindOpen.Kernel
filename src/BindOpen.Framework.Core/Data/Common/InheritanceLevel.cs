using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Common
{

    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the possible levels of inheritance.
    /// </summary>
    [Serializable()]
    [XmlType("InheritanceLevel", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum InheritanceLevel
    {
        /// <summary>
        /// None.
        /// </summary>
        None,
        /// <summary>
        /// Heritable.
        /// </summary>
        Heritable,
        /// <summary>
        /// Inherited.
        /// </summary>
        Inherited
    }

    #endregion

}
