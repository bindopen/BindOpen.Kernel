namespace BindOpen.Application.Modules
{

    /// <summary>
    /// This enumeration lists the possible sub kinds of an application module.
    /// </summary>
    public enum ApplicationModuleSubKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,
        /// <summary>
        /// Parent module.
        /// </summary>
        Parent,
        /// <summary>
        /// The sub module. A parent module can be composed by technical sub modules of different tiers.
        /// </summary>
        SubModule,
        /// <summary>
        /// The universe. A universe is a functional subdivision of a parent module.
        /// </summary>
        Universe
    }

}
