using System.Xml.Serialization;

namespace BindOpen.Runtime
{
    /// <summary>
    /// This enumeration represents the possible kinds of library item viewers.
    /// </summary>
    [XmlType("BdoExtensionViewerKind", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
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
        /// Function.
        /// </summary>
        Function,

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
