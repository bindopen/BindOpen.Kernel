﻿using System.Xml.Serialization;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This enumeration represents the possible kinds of library items.
    /// </summary>
    [XmlType("BdoExtensionKind", Namespace = "https://xsd.bindopen.org")]
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
        /// Task.
        /// </summary>
        Task,

        /// <summary>
        /// Format.
        /// </summary>
        Format,

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