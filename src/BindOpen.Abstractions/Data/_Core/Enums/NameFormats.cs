using System;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration lists all the possible name formats.
    /// </summary>
    [Flags]
    [XmlType("NameFormats", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    public enum NameFormats
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Only name.
        /// </summary>
        OnlyName,

        /// <summary>
        /// Name with value.
        /// </summary>
        NameWithValue,

        /// <summary>
        /// Name then value.
        /// </summary>
        NameThenValue,

        /// <summary>
        /// Only value.
        /// </summary>
        OnlyValue,
    }
}