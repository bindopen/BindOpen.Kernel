using BindOpen.Data.Queries;

namespace BindOpen.Extensions.References
{
    /// <summary>
    /// This class represents an extension reference extension.
    /// </summary>
    public static class BdoExtensionReferenceCollectionExtension
    {
        /// <summary>
        /// Adds a MSSqlServer extension reference to a specified list of references.
        /// </summary>
        /// <returns>Returns the updated list of references.</returns>
        public static IBdoExtensionReferenceCollection AddMSSqlServer(this IBdoExtensionReferenceCollection references)
        {
            references?.Add(BdoExtensionReferenceFactory.CreateFrom<DbQueryBuilder_MSSqlServer>());
            return references;
        }
    }
}