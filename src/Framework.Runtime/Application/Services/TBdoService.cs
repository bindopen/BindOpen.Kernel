using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Application.Settings;
using BindOpen.Framework.System.Diagnostics.Loggers;
using System;

namespace BindOpen.Framework.Application.Services
{
    /// <summary>
    /// This class represents an bot.
    /// </summary>
    public abstract class TBdoService<S> : BdoService, ITBdoService<S>, IBdoTrigeredService
        where S : IBdoSettings, new()
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public S Settings { get; set; }

        // Trigger actions --------------------------------------

        /// <summary>
        /// The action that the start of this instance completes.
        /// </summary>
        public Action<ITBdoService<S>> Action_OnStartSuccess { get; set; }

        /// <summary>
        /// The action that the start of this instance fails.
        /// </summary>
        public Action<ITBdoService<S>> Action_OnStartFailure { get; set; }

        /// <summary>
        /// The action that this instance completes.
        /// </summary>
        public Action<ITBdoService<S>> Action_OnExecutionSucess { get; set; }

        /// <summary>
        /// The action that is executed when the instance fails.
        /// </summary>
        public Action<ITBdoService<S>> Action_OnExecutionFailure { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoService class.
        /// </summary>
        protected TBdoService(
            IBdoScope scope,
            S settings = default,
            params IBdoLogger[] loggers) : base(scope, loggers)
        {
            Settings = settings;
        }

        #endregion

        // ------------------------------------------
        // PROCESSING
        // ------------------------------------------

        #region Processing

        // Events ------------------------------

        /// <summary>
        /// Indicates that this instance has successfully started.
        /// </summary>
        public virtual void StartSucceeds()
        {
            Action_OnStartSuccess?.Invoke(this);
        }

        /// <summary>
        /// Indicates that this instance has not successfully started.
        /// </summary>
        public virtual void StartFails()
        {
            Action_OnStartFailure?.Invoke(this);
        }

        /// <summary>
        /// Indicates that this instance completes.
        /// </summary>
        public virtual void ExecutionSucceedes()
        {
            Action_OnExecutionSucess?.Invoke(this);
        }

        /// <summary>
        /// Indicates that this instance fails.
        /// </summary>
        public virtual void ExecutionFails()
        {
            Action_OnExecutionFailure?.Invoke(this);
        }

        #endregion
    }
}