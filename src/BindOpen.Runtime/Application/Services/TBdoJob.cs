using BindOpen.Application.Settings;
using Microsoft.Extensions.Logging;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents an bot.
    /// </summary>
    public abstract class TBdoJob<S> : BdoJob, ITBdoJob<S>, IBdoTrigeredService
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

        /// <summary>
        /// The logger of this instance.
        /// </summary>
        public ILogger Logger
        {
            get => Scope.Logger;
            set { Scope.Logger = value; }
        }

        // Trigger actions --------------------------------------

        /// <summary>
        /// The action that the start of this instance completes.
        /// </summary>
        public Action<ITBdoJob<S>> Action_OnStartSuccess { get; set; }

        /// <summary>
        /// The action that the start of this instance fails.
        /// </summary>
        public Action<ITBdoJob<S>> Action_OnStartFailure { get; set; }

        /// <summary>
        /// The action that this instance completes.
        /// </summary>
        public Action<ITBdoJob<S>> Action_OnExecutionSucess { get; set; }

        /// <summary>
        /// The action that is executed when the instance fails.
        /// </summary>
        public Action<ITBdoJob<S>> Action_OnExecutionFailure { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoJob class.
        /// </summary>
        protected TBdoJob() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specfied settings
        /// </summary>
        /// <param name="settings">The settings to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoJob<S> WithSettings(S settings)
        {
            Settings = settings;

            return this;
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