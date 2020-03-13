using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This enumerates the possible modes of database field.
    /// </summary>
    [Serializable()]
    [XmlType("DbFieldViewMode", Namespace = "https://bindopen.org/xsd")]
    public enum DbFieldViewMode
    {
        /// <summary>
        /// Only name.
        /// </summary>
        OnlyName,

        /// <summary>
        /// Complete name.
        /// </summary>
        CompleteName,

        /// <summary>
        /// Complete name or value.
        /// </summary>
        CompleteNameOrValue,

        /// <summary>
        /// Name equals value.
        /// </summary>
        NameEqualsValue,

        /// <summary>
        /// Only value.
        /// </summary>
        OnlyValue,

        /// <summary>
        /// Complete name as alias.
        /// </summary>
        CompleteNameAsAlias,

        /// <summary>
        /// Only name as alias.
        /// </summary>
        OnlyNameAsAlias
    }
}
