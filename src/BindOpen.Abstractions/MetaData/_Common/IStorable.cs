using System;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This interface defines a storable data.
    /// </summary>
    public interface IStorable
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
