namespace BindOpen.Framework.Runtime.Application.Modules
{

    /// <summary>
    /// This enumerates the possible kinds of visitor application module.
    /// </summary>
    public enum ApplicationModuleKind
    {
        /// <summary>
        /// None. Used when the application module is not defined.
        /// </summary>
        None,

        /// <summary>
        /// Website.
        /// </summary>
        Website,

        /// <summary>
        /// Web service.
        /// </summary>
        WebService,

        /// <summary>
        /// Windows desktop.
        /// </summary>
        WindowsDesktop,

        /// <summary>
        /// Windows service.
        /// </summary>
        WindowsService
    }

}
