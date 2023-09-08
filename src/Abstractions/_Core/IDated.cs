using System;

namespace BindOpen.Kernel
{
    /// <summary>
    /// This interface defines an object tracked with creation and last modification date.
    /// </summary>
    public interface IDated
    {
        /// <summary>
        /// The creation date of this object.
        /// </summary>
        DateTime? CreationDate { get; set; }

        /// <summary>
        /// The last modification date of this object.
        /// </summary>
        DateTime? LastModificationDate { get; set; }
    }
}
