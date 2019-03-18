namespace BindOpen.Framework.Runtime.System
{

    /// <summary>
    /// This enumerates the possible levels of execution.
    /// </summary>
    public enum ApplicationExecutionLevel
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// Development.
        /// </summary>
        DEV,
        /// <summary>
        /// Operation Acceptance Testing.
        /// </summary>
        OAT,
        /// <summary>
        /// User Acceptance Testing.
        /// </summary>
        UAT,
        /// <summary>
        /// Production.
        /// </summary>
        PROD,
        /// <summary>
        /// Demonstration.
        /// </summary>
        DEM
    }

}
