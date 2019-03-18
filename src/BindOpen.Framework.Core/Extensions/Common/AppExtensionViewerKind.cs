using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Common
{
    /// <summary>
    /// This enumeration represents the possible kinds of library item viewers.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtensionViewerKind", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum AppExtensionViewerKind
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
        ScriptWord,

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
