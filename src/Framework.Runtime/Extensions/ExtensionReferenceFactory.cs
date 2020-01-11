using BindOpen.Framework.Extensions.References;
using BindOpen.Framework.Runtime.Application.Hosts;
using System.Collections.Generic;

namespace BindOpen.Framework.Runtime.Extensions
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

        /// <summary>
        /// Adds a Runtime extension reference to a specified list of references.
        /// </summary>
        /// <returns>Returns the updated list of references.</returns>
        public static List<IBdoExtensionReference> AddRuntime(this List<IBdoExtensionReference> references)
        {
            references?.Add(BdoExtensionReferenceFactory.CreateFrom<IBdoHost>());
            return references;
        }
    }
}