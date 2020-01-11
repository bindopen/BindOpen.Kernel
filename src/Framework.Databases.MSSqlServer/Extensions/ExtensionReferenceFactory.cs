using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.Extensions.References;
using System.Collections.Generic;

namespace BindOpen.Framework.Databases.MSSqlServer.Extensions
{
    /// <summary>
    /// This class represents an application scope factory.
    /// </summary>
    public static class ExtensionReferenceFactory
    {
        /// <summary>
        /// Creates a reference to the MSSqlServer extension.
        /// </summary>
        /// <returns>Returns the reference to the MSSqlServer extension.</returns>
        public static IBdoExtensionReference CreateMSSqlServer()
        {
            return BdoExtensionReferenceFactory.CreateFrom<DbQueryBuilder_MSSqlServer>();
        }

        /// <summary>
        /// Adds a MSSqlServer extension reference to a specified list of references.
        /// </summary>
        /// <returns>Returns the updated list of references.</returns>
        public static List<IBdoExtensionReference> AddMSSqlServer(this List<IBdoExtensionReference> references)
        {
            references?.Add(BdoExtensionReferenceFactory.CreateFrom<DbQueryBuilder_MSSqlServer>());
            return references;
        }
    }
}