using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Databases.Data.Queries.Builders
{
    /// <summary>
    /// This enumerates the possible modes of data field.
    /// </summary>
    [Serializable()]
    [XmlType("DbDataFieldViewMode", Namespace = "https://bindopen.org/xsd")]
    public enum DbDataFieldViewMode
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
