namespace BindOpen.Scoping
{
    /// <summary>
    /// This interfaces represents a scoped service.
    /// </summary>
    public interface IBdoScoped
    {
        /// <summary>
        /// The scope of the service.
        /// </summary>
        IBdoScope Scope { get; set; }
    }
}