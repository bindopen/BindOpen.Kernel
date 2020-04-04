using BindOpen.Application.Scopes;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This interfaces represents a scoped service.
    /// </summary>
    public interface IBdoScoped
    {
        /// <summary>
        /// The scope of the service.
        /// </summary>
        IBdoScope Scope { get; }
    }
}