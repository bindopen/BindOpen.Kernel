using System;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This enumerates the possible modes of runtime.
    /// </summary>
    [Flags]
    public enum RuntimeModes
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
        /// Normal. No exception and error messages are displayed.
        /// </summary>
        Normal = 0x01 << 1,

        /// <summary>
        /// Debug. Exception and error messages are displayed.
        /// </summary>
        Debug = 0x01 << 2,

        /// <summary>
        /// Simulation. Everything is executed empty.
        /// </summary>
        Simulation = 0x01 << 3,

        /// <summary>
        /// Any runtime mode.
        /// </summary>
        Any = Normal | Debug | Simulation,
    }
}