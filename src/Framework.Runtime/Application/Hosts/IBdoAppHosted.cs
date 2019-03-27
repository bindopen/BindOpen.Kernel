namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines an application hosted item.
    /// </summary>
    public interface IBdoAppHosted
    {
        // Execution ---------------------------------

        /// <summary>
        /// The application host.
        /// </summary>
        BdoAppHost Host { get; set; }
    }
}