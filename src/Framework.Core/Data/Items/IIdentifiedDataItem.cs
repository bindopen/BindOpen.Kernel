namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents an identified data item.
    /// </summary>
    public interface IIdentifiedDataItem : IDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        string Id { get; set; }

        #endregion

        string Key();
    }
}
