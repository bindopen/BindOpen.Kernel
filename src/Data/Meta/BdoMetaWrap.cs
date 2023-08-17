using BindOpen.System.Scoping;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class BdoMetaWrap : BdoObject, IBdoMetaWrap
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        public BdoMetaWrap()
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        public BdoMetaWrap(IBdoScope scope, IBdoMetaSet sst)
        {
            Scope = scope;
            Detail = sst;
        }

        #endregion

        // -------------------------------------------------------
        // IBdoSettings Implementation
        // -------------------------------------------------------

        #region IBdoSettings

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; set; }

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IBdoMetaSet Detail { get; set; }

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public string Key()
        {
            return Detail?.Name;
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
