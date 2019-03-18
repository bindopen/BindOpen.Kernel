using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Common
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the possible data operators.
    /// </summary>
    [Serializable()]
    [XmlType("DataOperator", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum DataOperator
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Declaration of list.
        /// </summary>
        ListDeclaration,

        /// <summary>
        /// Contains.
        /// </summary>
        Contains,

        /// <summary>
        /// Has.
        /// </summary>
        Has,

        /// <summary>
        /// In.
        /// </summary>
        In,

        /// <summary>
        /// Equal.
        /// </summary>
        Equal,

        /// <summary>
        /// Different.
        /// </summary>
        Different,

        /// <summary>
        /// Lesser.
        /// </summary>
        Lesser,

        /// <summary>
        /// Lesser or equal.
        /// </summary>
        LesserOrEqual,

        /// <summary>
        /// Greater.
        /// </summary>
        Greater,

        /// <summary>
        /// Greater or equal.
        /// </summary>
        GreaterOrEqual
    }

    #endregion
}
