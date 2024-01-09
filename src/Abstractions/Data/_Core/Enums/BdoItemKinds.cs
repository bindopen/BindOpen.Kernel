using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration lists the possible item kinds.
    /// </summary>
    [XmlType("ItemKinds", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum BdoItemKinds
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Dictionary | Expression | Merger | Reference,

        /// <summary>
        /// Requirement.
        /// </summary>
        Dictionary = 1,

        /// <summary>
        /// Expression.
        /// </summary>
        Expression = 2,

        /// <summary>
        /// Merger.
        /// </summary>
        Merger = 4,

        /// <summary>
        /// Reference.
        /// </summary>
        Reference = 8
    };

}
