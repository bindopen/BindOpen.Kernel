namespace BindOpen.Data.Common
{
    /// <summary>
    /// This interface defines a storable data.
    /// </summary>
    public interface IStorable
    {
        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        string CreationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        string LastModificationDate
        {
            get;
            set;
        }
    }
}
