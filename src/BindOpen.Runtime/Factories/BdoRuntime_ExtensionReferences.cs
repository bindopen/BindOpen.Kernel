using BindOpen.Runtime.References;

namespace BindOpen.Runtime
{
    /// <summary>
    /// This class represents an extension reference factory.
    /// </summary>
    public static partial class BdoRuntime
    {
        /// <summary>
        /// Creates a assembly reference from the specified extension one.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="version">The version to consider.</param>
        /// <returns>Returns the itemized list of the specified library list.</returns>
        public static IBdoExtensionReference Extension(string name, string version = null)
            => new BdoExtensionReference(name, version);

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoExtensionReference ExtensionFrom<T>()
        {
            var assemblyReference = BdoRuntime.AssemblyFrom<T>();
            return new BdoExtensionReference(assemblyReference?.Name, assemblyReference?.Version);
        }

        /// <summary>
        /// Creates a assembly reference that represents all the references.
        /// </summary>
        /// <returns>Returns a assembly reference that represents all the references.</returns>
        public static IBdoExtensionReference ExtensionFromAll()
            => new BdoExtensionReference("*");
    }
}