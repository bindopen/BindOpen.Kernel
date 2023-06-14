using System.Xml.Serialization;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This enumeration represents the possible kinds of library items.
    /// </summary>
    [XmlType("BdoExtensionKind", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    public enum BdoExtensionKind
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
        /// Connector.
        /// </summary>
        Connector,

        /// <summary>
        /// Entity.
        /// </summary>
        Entity,

        /// <summary>
        /// Function.
        /// </summary>
        Function,

        /// <summary>
        /// Task.
        /// </summary>
        Task
    }
}
