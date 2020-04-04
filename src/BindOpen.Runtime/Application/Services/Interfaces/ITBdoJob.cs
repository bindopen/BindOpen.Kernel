using BindOpen.Application.Settings;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface ITBdoJob<S> : IBdoJob
        where S : IBdoSettings, new()
    {
        /// <summary>
        /// The settings.
        /// </summary>
        S Settings { get; set; }

        // Trigger actions --------------------------------------

        /// <summary>
        /// The action that the start of this instance completes.
        /// </summary>
        Action<ITBdoJob<S>> Action_OnStartSuccess { get; set; }

        /// <summary>
        /// The action that the start of this instance fails.
        /// </summary>
        Action<ITBdoJob<S>> Action_OnStartFailure { get; set; }

        /// <summary>
        /// The action that this instance completes.
        /// </summary>
        Action<ITBdoJob<S>> Action_OnExecutionSucess { get; set; }

        /// <summary>
        /// The action that is executed when the instance fails.
        /// </summary>
        Action<ITBdoJob<S>> Action_OnExecutionFailure { get; set; }
    }
}