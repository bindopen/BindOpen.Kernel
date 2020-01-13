using BindOpen.Framework.Application.Scopes;

namespace BindOpen.Framework.Extensions.References
{
    /// <summary>
    /// This class represents an application scope factory.
    /// </summary>
    public static class ExtensionReferenceFactory
    {
        /// <summary>
        /// Creates a reference to the Runtime extension.
        /// </summary>
        /// <returns>Returns the reference to the Runtime extension.</returns>
        public static IBdoExtensionReference CreateRuntime()
        {
            return BdoExtensionReferenceFactory.CreateFrom<IBdoHost>();
        }
    }
}