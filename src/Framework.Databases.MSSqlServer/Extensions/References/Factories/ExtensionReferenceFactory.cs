using BindOpen.Framework.Data.Queries;

namespace BindOpen.Framework.Extensions.References

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
    }
}