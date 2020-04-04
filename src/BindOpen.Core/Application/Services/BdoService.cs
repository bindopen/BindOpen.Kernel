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

        // Scope ----------------------

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IBdoScope _scope = null;

        #endregion

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

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
        protected BdoService()
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
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _scope?.Dispose();
            }
        }

        #endregion

    }
}