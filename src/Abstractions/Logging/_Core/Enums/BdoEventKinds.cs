using System;
using System.Xml.Serialization;

namespace BindOpen.Logging
{
    /// <summary>
    /// This enumerates the possible event levels.
    /// </summary>
    [Flags()]
<<<<<<<< HEAD:src/Abstractions/Logging/_Core/Enums/BdoEventKinds.cs
    [XmlType("BdoEventKinds", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum BdoEventKinds
========
    [XmlType("BdoEventLevels", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum BdoEventLevels
>>>>>>>> d60bb9af72a0d6700a60146d59aff41e2c44e792:src/Abstractions/Logging/_Core/Enums/BdoEventLevels.cs
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Always.
        /// </summary>
        Always = 1 << 0,

        /// <summary>
        /// Verbose.
        /// </summary>
        Verbose = 1 << 1,

        /// <summary>
        /// Debug.
        /// </summary>
        Debug = 1 << 2,

        /// <summary>
        /// Information.
        /// </summary>
        Information = 1 << 3,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning = 1 << 4,

        /// <summary>
        /// Error.
        /// </summary>
        Error = 1 << 5,

        /// <summary>
        /// Fatal.
        /// </summary>
        Fatal = 1 << 6,

        /// <summary>
        /// None.
        /// </summary>
<<<<<<<< HEAD:src/Abstractions/Logging/_Core/Enums/BdoEventKinds.cs
        Any = Checkpoint | Error | Exception | Message | Other | Warning
========
        Any = Always | Verbose | Debug | Information | Warning | Error | Fatal,
>>>>>>>> d60bb9af72a0d6700a60146d59aff41e2c44e792:src/Abstractions/Logging/_Core/Enums/BdoEventLevels.cs
    };
}
