using BindOpen.Framework.Core.Extensions.References;
using BindOpen.Framework.Databases.PostgreSql.Data.Queries.Builders;
using System.Collections.Generic;

namespace BindOpen.Framework.Databases.PostgreSql.Extensions
{
    /// <summary>
    /// This class represents an application scope factory.
    /// </summary>
    public static class ExtensionReferenceFactory
    {
        /// <summary>
        /// Creates a reference to the PostgreSql extension.
        /// </summary>
        /// <returns>Returns the reference to the PostgreSql extension.</returns>
        public static IBdoExtensionReference CreatePostgreSql()
        {
            return BdoExtensionReferenceFactory.CreateFrom<DbQueryBuilder_PostgreSql>();
        }

        /// <summary>
        /// Adds a PostgreSql extension reference to a specified list of references.
        /// </summary>
        /// <returns>Returns the updated list of references.</returns>
        public static List<IBdoExtensionReference> AddPostgreSql(this List<IBdoExtensionReference> references)
        {
            references?.Add(BdoExtensionReferenceFactory.CreateFrom<DbQueryBuilder_PostgreSql>());
            return references;
        }
    }
}