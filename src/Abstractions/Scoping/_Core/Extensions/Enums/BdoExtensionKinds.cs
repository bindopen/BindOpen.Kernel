using System;
using System.Xml.Serialization;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This enumeration represents the possible kinds of library items.
    /// </summary>
    [Flags]
    [XmlType("BdoExtensionKind", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    public enum BdoExtensionKinds
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any = None | Connector | Entity | Function | Task | Scriptword,

        /// <summary>
        /// None.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Connector.
        /// </summary>
        Connector = 0x1 << 0,

        /// <summary>
        /// Entity.
        /// </summary>
        Entity = 0x1 << 1,

        /// <summary>
        /// Function.
        /// </summary>
        Function = 0x1 << 2,

        /// <summary>
        /// Task.
        /// </summary>
        Task = 0x1 << 3,

        /// <summary>
        /// Script word.
        /// </summary>
        Scriptword = 0x1 << 4
    }
}
