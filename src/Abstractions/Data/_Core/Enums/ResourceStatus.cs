using System;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This enumeration lists all the possible statuses of resource.
    /// </summary>
    [Flags]
    public enum ResourceStatus
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
        /// Created.
        /// </summary>
        Created = 0x01 << 1,

        /// <summary>
        /// Updated.
        /// </summary>
        Updated = 0x01 << 2,

        /// <summary>
        /// Patched.
        /// </summary>
        Patched = 0x01 << 3,

        /// <summary>
        /// Deleted.
        /// </summary>
        Deleted = 0x01 << 4,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Created | Updated | Patched | Deleted,
    }
}
