using System;
using System.Xml.Serialization;

namespace BindOpen.Kernel
{
    /// <summary>
    /// This enumeration represents the possible update modes.
    /// </summary>
    [Flags]
    [XmlType("UpdateModes", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum UpdateModes
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0x01 << 0,

        /// <summary>
        /// Adds Source items that are not in Target.
        /// </summary>
        /// <remarks>Used in incremental update.</remarks>
        Incremental_AddMissingInTarget = 0x01 << 1,

        /// <summary>
        /// Removes Target items that are not in Source.
        /// </summary>
        /// <remarks>Used in incremental update.</remarks>
        Incremental_RemoveMissingInSource = 0x01 << 2,

        /// <summary>
        /// Updates Target items that are in Source.
        /// </summary>
        /// <remarks>Used in incremental update.</remarks>
        Incremental_UpdateCommon = 0x01 << 3,

        /// <summary>
        /// Full update mode.
        /// </summary>
        /// <remarks>Used in full update.</remarks>
        Full = Incremental_AddMissingInTarget | Incremental_RemoveMissingInSource | Incremental_UpdateCommon
    }
}
