using BindOpen.System.Data.Assemblies;
using System;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents an extension reference extension.
    /// </summary>
    public static class BdoAssemblyReferenceExtension
    {
        /// <summary>
        /// Creates a assembly reference from the specified extension one.
        /// </summary>
        /// <param key="references">The reference set to consider.</param>
        /// <param key="name">The name to consider.</param>
        /// <param key="version">The version to consider.</param>
        /// <returns>Returns the itemized list of the specified library list.</returns>
        public static IBdoAssemblyReference AddAssembly(
            this IBdoAssemblyReferenceCollection references,
            string name,
            Version version = null)
        {
            var reference = BdoData.Assembly(name, version);
            references.Add(reference);
            return reference;
        }

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <param key="references">The reference set to consider.</param>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoAssemblyReference AddAssembly<T>(this IBdoAssemblyReferenceCollection references)
        {
            var reference = BdoData.AssemblyFrom<T>();
            references.Add(reference);
            return reference;
        }

        /// <summary>
        /// Creates a assembly reference that represents all the references.
        /// </summary>
        /// <param key="references">The reference set to consider.</param>
        /// <returns>Returns a assembly reference that represents all the references.</returns>
        public static IBdoAssemblyReference AddAllAssemblies(this IBdoAssemblyReferenceCollection references)
        {
            var reference = BdoData.AssemblyAsAll();
            references.Add(reference);
            return reference;
        }
    }
}