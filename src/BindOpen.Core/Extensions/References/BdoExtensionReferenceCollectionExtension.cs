namespace BindOpen.Extensions.References
{
    /// <summary>
    /// This class represents an extension reference extension.
    /// </summary>
    public static class BdoExtensionReferenceExtension
    {
        /// <summary>
        /// Creates a assembly reference from the specified extension one.
        /// </summary>
        /// <param name="references">The reference collection to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="version">The version to consider.</param>
        /// <returns>Returns the itemized list of the specified library list.</returns>
        public static IBdoExtensionReference AddAssembly(this IBdoExtensionReferenceCollection references, string name, string version = null)
        {
            var reference = BdoExtensionReferenceFactory.Create(name, version);
            references.Add(reference);
            return reference;
        }

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <param name="references">The reference collection to consider.</param>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoExtensionReference AddAssembly<T>(this IBdoExtensionReferenceCollection references)
        {

            var reference = BdoExtensionReferenceFactory.CreateFrom<T>();
            references.Add(reference);
            return reference;
        }

        /// <summary>
        /// Creates a assembly reference that represents all the references.
        /// </summary>
        /// <param name="references">The reference collection to consider.</param>
        /// <returns>Returns a assembly reference that represents all the references.</returns>
        public static IBdoExtensionReference AddAllAssemblies(this IBdoExtensionReferenceCollection references)
        {

            var reference = BdoExtensionReferenceFactory.CreateAsAll();
            references.Add(reference);
            return reference;
        }
    }
}