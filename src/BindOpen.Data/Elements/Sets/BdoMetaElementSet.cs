using BindOpen.Logging;
using BindOpen.Meta.Items;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public partial class BdoMetaElementSet : TBdoItemSet<IBdoMetaElement>, IBdoElementSet
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

        // Items ------------------------

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="key">The element key to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual object GetItem(
            string key,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            IBdoMetaElement element = base.Get(key);
            if (element != null)
            {
                return element.GetItem(scope, varElementSet, log);
            }

            return null;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="key">The element key to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual T GetItem<T>(
            string key,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            IBdoMetaElement element = base.Get(key);
            if (element != null)
            {
                return element.GetItem<T>(scope, varElementSet, log);
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">The element key to consider.</param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public List<object> GetItemList(
            string key,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            IBdoMetaElement element = base.Get(key);
            if (element != null)
            {
                return element.GetItemList(scope, varElementSet, log);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="key">The element key to consider.</param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public List<T> GetItemList<T>(
            string key,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            IBdoMetaElement element = base.Get(key);
            if (element != null)
            {
                return element.GetItemList<T>(scope, varElementSet, log);
            }

            return null;
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
            BdoMetaElementSet elementSet = base.Clone(areas) as BdoMetaElementSet;

            return elementSet;
        }

        #endregion
    }
}

