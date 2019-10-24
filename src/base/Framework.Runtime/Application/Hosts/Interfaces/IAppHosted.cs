namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines a BDO application hosted item.
    /// </summary>
    public interface IAppHosted
    {
        // Execution ---------------------------------

        /// <summary>
        /// The application host.
        /// </summary>
        IAppHost Host { get; set; }
    }
}