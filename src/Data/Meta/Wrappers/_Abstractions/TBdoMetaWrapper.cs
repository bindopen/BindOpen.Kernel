using BindOpen.Data.Helpers;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Scoping;

namespace BindOpen.Data.Meta
{
    public abstract class TBdoMetaWrapper<TDetail> : BdoObject, ITBdoMetaWrapper<TDetail>
        where TDetail : IBdoMetaSet, new()
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        public TBdoMetaWrapper() : this(null, default)
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        public TBdoMetaWrapper(IBdoScope scope, TDetail detail)
        {
            Scope = scope;
            Detail = detail;

            Detail = new TDetail();
        }

        #endregion

        // -------------------------------------------------------
        // IBdoMetaWrapper Implementation
        // -------------------------------------------------------

        #region IBdoMetaWrapper

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; set; }

        IBdoMetaSet IBdoDetailed.Detail { get => Detail; set { Detail = value.As<TDetail>(); } }

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public TDetail Detail { get; set; }

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public virtual string Key()
        {
            return Detail?.Identifier;
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="name">The name to consider.</param>
        public T GetData<T>(string name)
        {
            return Detail == null ? default : Detail.GetData<T>(name, Scope);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        public object GetData(string name)
        {
            return Detail?.GetData(name, Scope);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="propertyName">The calling property name to consider.</param>
        public void UpdateDetail(
            IBdoMetaSet detail = null,
            bool onlyMetaAttributes = true)
        {
            Detail ??= new TDetail();

            detail ??= BdoData.NewSet(this.ToMeta(null, onlyMetaAttributes, false)?.ToArray());
            Detail.Update(detail);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="propertyName">The calling property name to consider.</param>
        public void UpdateProperties(
            bool onlyMetaAttributes = true)
        {
            this.UpdateFromMeta(Detail, onlyMetaAttributes);
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion    
    }
}