using BindOpen.Logging;
using BindOpen.Scopes;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are carriers.
    /// </summary>
    public partial class TBdoMetaObject<TItem> :
        BdoMetaObject,
        ITBdoMetaObject<TItem>
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        public TBdoMetaObject() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="id">The ID to consider.</param>
        public TBdoMetaObject(
            string name = null,
            string namePreffix = "object_",
            string id = null)
            : base(name, namePreffix, id)
        {
        }

        #endregion

        // ------------------------------------------
        // TBdoMetaObject Implementation
        // ------------------------------------------

        #region TBdoMetaObject

        public void SetData(TItem obj)
        {
            base.SetData(obj);
        }

        public new TItem GetData(
            IBdoScope scope,
            IBdoMetaSet varSet,
            IBdoLog log = null)
        {
            return base.GetData<TItem>(scope, varSet, log);
        }

        #endregion
    }
}
