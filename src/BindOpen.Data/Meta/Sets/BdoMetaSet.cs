using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public partial class BdoMetaSet :
        TBdoItemSet<IBdoMetaItem>,
        IBdoMetaSet
    {
        // ------------------------------------------
        // CONVERTERS
        // ------------------------------------------

        #region Converters

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static explicit operator BdoMetaSet(IBdoMetaItem[] elems)
        {
            return BdoMeta.NewSet(elems);
        }

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static explicit operator IBdoMetaItem[](BdoMetaSet metaSet)
        {
            return metaSet?.ToArray();
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        public BdoMetaSet() : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // IBdoMetaItem Implementation
        // --------------------------------------------------

        #region IBdoMetaItem

        /// <summary>
        /// 
        /// </summary>
        public BdoMetaDataKind Kind
                => BdoMetaDataKind.Set;

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaItem Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Index { get; set; }

        #endregion

        // --------------------------------------------------
        // IBdoMetaSet Implementation
        // --------------------------------------------------

        #region IBdoMetaSet

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public new IBdoMetaSet Add(
            params IBdoMetaItem[] items)
        {
            base.Add(items);

            return this;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public new IBdoMetaSet WithItems(
            params IBdoMetaItem[] items)
        {
            base.WithItems(items);

            return this;
        }

        #endregion

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
            var el = base.Clone(areas) as BdoMetaSet;
            return el;
        }

        #endregion
    }
}
