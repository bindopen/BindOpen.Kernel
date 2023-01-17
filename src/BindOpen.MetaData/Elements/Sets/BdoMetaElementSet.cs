using BindOpen.MetaData.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public partial class BdoMetaElementSet : TBdoItemSet<IBdoMetaElement>, IBdoMetaElementSet
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Conversions -----------------------------

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elements">The elements to consider.</param>
        public static implicit operator BdoMetaElementSet(IBdoMetaElement[] elements)
        {
            return BdoMeta.NewSet(elements);
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoElementSet class.
        /// </summary>
        public BdoMetaElementSet()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Elements -----------------------------

        /// <summary>
        /// Returns the item with the specified name and group ID.
        /// </summary>
        /// <param name="key">The name of the item to return.</param>
        /// <param name="groupId">The ID of the group of the item to return.</param>
        /// <returns>Returns the item with the specified name and group ID.</returns>
        public IBdoMetaElement Get(string key, string specId = null)
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
            BdoMetaElementSet elemSet = base.Clone(areas) as BdoMetaElementSet;

            return elemSet;
        }

        #endregion
    }
}

