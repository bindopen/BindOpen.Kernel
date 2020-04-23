using BindOpen.Application.Scopes;
using BindOpen.Application.Services;
using BindOpen.Extensions.References;

namespace BindOpen.Extensions.References
{
    /// <summary>
    /// This class represents an extension reference extension.
    /// </summary>
    public static class BdoExtensionReferenceExtension
    {
        /// <summary>
        /// Adds a Runtime extension reference to a specified list of references.
        /// </summary>
        /// <returns>Returns the updated list of references.</returns>
        public static IBdoExtensionReferenceCollection AddRuntime(this IBdoExtensionReferenceCollection references)
        {
            references?.Add(BdoExtensionReferenceFactory.CreateFrom<IBdoHost>());
            return references;
        }
    }
}