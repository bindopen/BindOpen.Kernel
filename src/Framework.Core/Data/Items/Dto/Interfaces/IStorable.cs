namespace BindOpen.Framework.Data.Items
{
    /// <summary>
    /// This interface defines a storable data.
    /// </summary>
    public interface IStorable
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

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

        #endregion
    }
}
