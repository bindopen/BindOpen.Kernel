using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This enumeration represents the possible kinds of library item viewers.
    /// </summary>
    [XmlType("BdoExtensionViewerKind", Namespace = "https://xsd.bindopen.org")]
    public enum BdoExtensionViewerKind
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
        /// Metrics.
        /// </summary>
        Metrics,

        /// <summary>
        /// Script word.
        /// </summary>
        Scriptword,

        /// <summary>
        /// Task.
        /// </summary>
        Task,

        /// <summary>
        /// Data query.
        /// </summary>
        DataQuery,

        /// <summary>
        /// Data class.
        /// </summary>
        DataClass,

        /// <summary>
        /// Data format.
        /// </summary>
        FormatConfiguration
    }
}
