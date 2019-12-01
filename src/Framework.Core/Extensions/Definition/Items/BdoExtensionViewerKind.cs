using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
{
    /// <summary>
    /// This enumeration represents the possible kinds of library item viewers.
    /// </summary>
    [Serializable()]
    [XmlType("BdoExtensionViewerKind", Namespace = "https://bindopen.org/xsd")]
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
