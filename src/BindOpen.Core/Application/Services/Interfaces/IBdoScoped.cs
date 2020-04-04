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

        /// <summary>
        /// Sets the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        IBdoScoped WithScope(IBdoScope scope);
    }
}