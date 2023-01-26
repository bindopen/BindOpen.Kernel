using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public partial class BdoMetaSet : TBdoItemSet<IBdoMetaData>, IBdoMetaSet
    {
        // ------------------------------------------
        // CONVERTERS
        // ------------------------------------------

        #region Converters

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static explicit operator BdoMetaSet(IBdoMetaData[] elems)
        {
            return BdoMeta.NewSet(elems);
        }

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static explicit operator IBdoMetaData[](BdoMetaSet elemSet)
        {
            return elemSet?.Items?.ToArray();
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        public BdoMetaSet()
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoMetaSet Implementation
        // ------------------------------------------

        #region IBdoMetaSet

        // Elements -----------------------------

        /// <summary>
        /// Returns the item with the specified name and group ID.
        /// </summary>
        /// <param name="key">The name of the item to return.</param>
        /// <param name="groupId">The ID of the group of the item to return.</param>
        /// <returns>Returns the item with the specified name and group ID.</returns>
        public IBdoMetaData Get(string key, string specId = null)
        {
            if (specId == null)
            {
                return Items?.FirstOrDefault(p => p.Name.BdoKeyEquals(key));
            }

            return Items?.FirstOrDefault(p =>
                p.Name.BdoKeyEquals(key)
                && p.Specs.Any(q => q.BdoKeyEquals(specId) == true));
        }

        // Groups -------------------------------

        /// <summary>
        /// Gets all the element groups IDs.
        /// </summary>
        /// <returns>Returns all the element group IDs.</returns>
        public List<string> GetSpecIds()
        {
            if (Items == null) return new List<string>();
            return Items.SelectMany(p => p.Specs.Select(q => q.Id)).Distinct().ToList();
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
            BdoMetaSet elemSet = base.Clone(areas) as BdoMetaSet;

            return elemSet;
        }

        #endregion
    }
}

