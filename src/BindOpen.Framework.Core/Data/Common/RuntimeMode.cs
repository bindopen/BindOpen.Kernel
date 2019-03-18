namespace BindOpen.Framework.Core.Data.Common
{

    /// <summary>
    /// This enumerates the possible modes of runtime.
    /// </summary>
    public enum RuntimeMode
    {
        /// <summary>
        /// Normal. No exception and error messages are displayed.
        /// </summary>
        Normal,
        /// <summary>
        /// Debug. Exception and error messages are displayed.
        /// </summary>
        Debug,
        /// <summary>
        /// Simulation. Everything is executed empty.
        /// </summary>
        Simulation
    }

}
