using BindOpen.Application.Scopes;
using BindOpen.Data.Items;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents an bot.
    /// </summary>
    public abstract class BdoService : IdentifiedDataItem, IBdoScoped
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private bool _isDisposed = false;

        // Scope ----------------------

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IBdoScope _scope = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES   
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        public IBdoScope Scope
        {
            get { return _scope; }
        }

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Initializes a new instance of the BdoService class.
        /// </summary>
        protected BdoService() : base("")
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoService class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        protected BdoService(IBdoScope scope)
        {
            _scope = scope;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns this instance.</returns>
        public virtual IBdoScoped WithScope(IBdoScope scope)
        {
            _scope = scope;

            return this;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _scope?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}