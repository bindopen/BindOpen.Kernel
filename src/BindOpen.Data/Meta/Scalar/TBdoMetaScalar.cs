using BindOpen.Data.Helpers;
using BindOpen.Logging;
using BindOpen.Scopes;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a scalar meta that is an meta whose items are scalars.
    /// </summary>
    public class TBdoMetaScalar<TItem> : BdoMetaScalar,
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
            : base(name, id)
        {
        }

        #endregion

        // --------------------------------------------------
        // IBdoMetaScalar Implementation
        // --------------------------------------------------

        #region IBdoMetaScalar

        // Items ----------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _data.ToString(this.GetSpec()?.DataType.ValueType ?? DataValueTypes.None);
        }

        // Data ----------------------------

        public ITBdoMetaScalar<TItem> WithData(TItem obj)
        {
            base.WithData(obj);
            return this;
        }

        public new TItem GetData(
            IBdoScope scope,
            IBdoMetaSet varSet,
            IBdoLog log = null)
        {
            return base.GetData<TItem>(scope, varSet, log);
        }

        public new TItem GetData(
            int index,
            IBdoScope scope,
            IBdoMetaSet varSet,
            IBdoLog log = null)
        {
            return base.GetData<TItem>(index, scope, varSet, log);
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
