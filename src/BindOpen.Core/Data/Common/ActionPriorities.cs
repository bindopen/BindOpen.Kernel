using System;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This enumeration lists the possible kinds of actions.
    /// </summary>
    [Flags]
    public enum ActionPriorities
    {
        /// <summary>
        /// None can access.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Inherited.
        /// </summary>
        Inherited = 0x1 << 0,

        /// <summary>
        /// Low.
        /// </summary>
        Low = 0x01 << 1,

        /// <summary>
        /// Medium.
        /// </summary>
        Medium = 0x01 << 2,

        /// <summary>
        /// High.
        /// </summary>
        High = 0x01 << 3,

        /// <summary>
        /// Very high.
        /// </summary>
        VeryHigh = 0x01 << 4,

        /// <summary>
        /// Any action priority.
        /// </summary>
        Any = Low | Medium | High | VeryHigh
    }

}
