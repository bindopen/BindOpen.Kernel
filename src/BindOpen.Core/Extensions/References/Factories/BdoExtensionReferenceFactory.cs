using BindOpen.System.Assemblies.References;

namespace BindOpen.Extensions.References
{
    /// <summary>
    /// This class represents an extension reference factory.
    /// </summary>
    public static class BdoExtensionReferenceFactory
    {
        /// <summary>
        /// Creates a assembly reference from the specified extension one.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="version">The version to consider.</param>
        /// <returns>Returns the itemized list of the specified library list.</returns>
        public static IBdoExtensionReference Create(string name, string version = null)
            => new BdoExtensionReference(name, version);

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoExtensionReference CreateFrom<T>()
        {
            var assemblyReference = BdoAssemblyReferenceFactory.CreateFrom<T>();
            return new BdoExtensionReference(assemblyReference?.Name, assemblyReference?.Version);
        }

        /// <summary>
        /// Creates a assembly reference that represents all the references.
        /// </summary>
        /// <returns>Returns a assembly reference that represents all the references.</returns>
        public static IBdoExtensionReference CreateAsAll()
            => new BdoExtensionReference("*");
    }
}