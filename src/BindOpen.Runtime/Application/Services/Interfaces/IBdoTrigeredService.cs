namespace BindOpen.Application.Services
{
    /// <summary>
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface IBdoTrigeredService
    {
        // Trigger actions --------------------------------------

        /// <summary>
        /// Indicates that the start of this instance completes.
        /// </summary>
        void StartSucceeds();

        /// <summary>
        /// Indicates that the start of this instance fails.
        /// </summary>
        void StartFails();

        /// <summary>
        /// Indicates that this instance execution succeeds.
        /// </summary>
        void ExecutionSucceedes();

        /// <summary>
        /// Indicates that this instance execution fails.
        /// </summary>
        void ExecutionFails();
    }
}