using BindOpen.Data.Queries;

namespace BindOpen.Extensions.References
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
    }
}