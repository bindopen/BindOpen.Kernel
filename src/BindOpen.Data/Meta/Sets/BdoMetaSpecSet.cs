using BindOpen.Data.Items;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a set of data element specifications.
    /// </summary>
    public class BdoMetaSpecSet : TBdoItemSet<IBdoMetaSpec>,
        IBdoMetaSpecSet
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoElementSpecSet class.
        /// </summary>
        public BdoMetaSpecSet() : base()
        {
        }

        #endregion

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public new IBdoMetaSpecSet Add(
            params IBdoMetaSpec[] items)
        {
            base.Add(items);

            return this;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public new IBdoMetaSpecSet WithItems(
            params IBdoMetaSpec[] items)
        {
            base.WithItems(items);

            return this;
        }

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            return base.Clone();
        }

        #endregion
    }

}
