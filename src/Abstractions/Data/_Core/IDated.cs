using System;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines a storable data.
    /// </summary>
    public interface IDated
    {
        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        DateTime? CreationDate { get; set; }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        DateTime? LastModificationDate { get; set; }
    }
}
