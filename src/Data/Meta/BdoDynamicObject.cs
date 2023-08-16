using BindOpen.System.Scoping;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public partial class BdoDynamicObject : BdoObject //, IBdoDynamicObject
    {
        [BdoProperty("test1")]
        public string Test1 { get; set; }

        [BdoProperty("test2")]
        public int Test2 { get; set; }

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        public BdoDynamicObject()
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        public BdoDynamicObject(IBdoScope scope, IBdoMetaSet sst)
        {
            Scope = scope;
            MetaSet = sst;
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
        public IBdoMetaSet MetaSet { get; set; }

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public string Key()
        {
            return MetaSet?.Name;
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="name">The name to consider.</param>
        public T GetData<T>(string name)
            where T : class
        {
            return MetaSet?.GetData<T>(name, Scope);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        public object GetData(string name)
        {
            return MetaSet?.GetData(name, Scope);
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
