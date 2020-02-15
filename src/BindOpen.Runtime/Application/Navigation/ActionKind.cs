using System;
using System.Xml.Serialization;

namespace BindOpen.Application.Navigation
{
    /// <summary>
    /// This enumerates the possible kinds of action.
    /// </summary>
    [Serializable()]
    [XmlType("ActionKinds", Namespace = "https://bindopen.org/xsd")]
    [Flags]
    public enum ActionKinds
    {
        /// <summary>
        /// None can access.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Access.
        /// </summary>
        Access = 0x1 << 1,

        /// <summary>
        /// View.
        /// </summary>
        View = 0x1 << 2,

        /// <summary>
        /// Download.
        /// </summary>
        Download = 0x1 << 3,

        /// <summary>
        /// Print.
        /// </summary>
        Print = 0x1 << 4,

        /// <summary>
        /// Search.
        /// </summary>
        Search = 0x1 << 5,

        /// <summary>
        /// Create.
        /// </summary>
        Create = 0x1 << 6,

        /// <summary>
        /// Edit.
        /// </summary>
        Edit = 0x1 << 7,

        /// <summary>
        /// Full edit.
        /// </summary>
        FullEdit = 0x1 << 8,

        /// <summary>
        /// Delete.
        /// </summary>
        Delete = 0x1 << 9,

        /// <summary>
        /// MergeDbQuery.
        /// </summary>
        MergeDbQuery = 0x1 << 10,

        /// <summary>
        /// Import.
        /// </summary>
        Import = 0x1 << 11,

        /// <summary>
        /// Export.
        /// </summary>
        Export = 0x1 << 12,

        /// <summary>
        /// Custom.
        /// </summary>
        Custom = 0x1 << 0
    }
}
