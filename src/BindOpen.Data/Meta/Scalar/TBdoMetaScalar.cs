using BindOpen.Data.Helpers;
using BindOpen.Logging;
using BindOpen.Scopes.Scopes;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a scalar meta that is an meta whose items are scalars.
    /// </summary>
    public class TBdoMetaScalar<TItem> :
        TBdoMetaData<TItem>,
        ITBdoMetaScalar<TItem>
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TBdoMetaScalar class.
        /// </summary>
        public TBdoMetaScalar() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TBdoMetaScalar class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="id">The ID to consider.</param>
        public TBdoMetaScalar(string name = null, string id = null)
            : base(name, "scalar_", id)
        {
        }

        #endregion

        // --------------------------------------------------
        // IBdoMetaScalar Implementation
        // --------------------------------------------------

        #region IBdoMetaScalar

        IBdoSpec IBdoMetaData.NewSpec()
        {
            return NewSpec();
        }

        // Items ----------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _data.ToString(DataValueType);
        }

        // Data ----------------------------

        /// <summary>
        /// Sets the item of this instance.
        /// </summary>
        /// <param key="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public ITBdoMetaScalar<TItem> WithData(object obj)
        {
            _data = obj.ToBdoData();
            return this;
        }

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public override TItem GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList<TItem>(scope, varSet, log);

            if (list == null)
            {
                return default;
            }

            return list.FirstOrDefault();
        }

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public override Q GetData<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList<Q>(scope, varSet, log);
            if (list == null)
            {
                return default;
            }

            return list.FirstOrDefault();
        }

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public TItem GetData(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = GetData<TItem>(index, scope, varSet, log); ;
            return obj;
        }

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q GetData<Q>(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList<Q>(scope, varSet, log); ;
            var obj = list.GetAt(index);
            return obj;
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
            var scalarElement = base.Clone(areas) as BdoMetaScalar;
            return scalarElement;
        }

        #endregion
    }
}
