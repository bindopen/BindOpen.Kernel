using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This enumeration represents the possible kinds of library items.
    /// </summary>
    [XmlType("BdoExtensionItemKind", Namespace = "https://xsd.bindopen.org")]
    public enum BdoExtensionItemKind
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
        /// Task.
        /// </summary>
        Task,

        /// <summary>
        /// Carrier.
        /// </summary>
        Carrier,

        /// <summary>
        /// Connector.
        /// </summary>
        Connector,

        /// <summary>
        /// Entity.
        /// </summary>
        Entity,

        /// <summary>
        /// Format.
        /// </summary>
        Format,

        /// <summary>
        /// Handler.
        /// </summary>
        Handler,

        /// <summary>
        /// Metrics.
        /// </summary>
        Metrics,

        /// <summary>
        /// RoutineConfiguration.
        /// </summary>
        Routine,

        /// <summary>
        /// Script word.
        /// </summary>
        Scriptword,
    }
}
