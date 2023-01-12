using BindOpen.Meta;
using BindOpen.Runtime.References;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Runtime
{
    /// <summary>
    /// This class represents an application scope factory.
    /// </summary>
    public static partial class BdoRuntime
    {
        /// <summary>
        /// Creates a assembly reference from the specified extension one.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="version">The version to consider.</param>
        /// <returns>Returns the itemized list of the specified library list.</returns>
        public static IBdoAssemblyReference Assembly(string name, string version = null)
            => new BdoAssemblyReference(name, version);

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoAssemblyReference AssemblyFrom<T>()
        {
            var assembly = typeof(T).Assembly;
            string name = assembly.GetName().Name;
            string version = assembly.GetName().Version.ToString();
            return new BdoAssemblyReference(name, version);
        }

        /// <summary>
        /// Creates a assembly reference that represents all the references.
        /// </summary>
        /// <returns>Returns a assembly reference that represents all the references.</returns>
        public static IBdoAssemblyReference AssemblyAsAll()
            => new BdoAssemblyReference("*");

        /// <summary>
        /// Gets the distinct list of the specified library reference list.
        /// </summary>
        /// <param name="references">The references to consider.</param>
        /// <returns>Returns the itemized list of the specified library list.</returns>
        public static List<BdoAssemblyReference> GetDistinctReferences(this IEnumerable<BdoAssemblyReference> references)
        {
            return references.GroupBy(l =>
                !string.IsNullOrEmpty(l.FileName) ?
                    (l.FileName + string.Empty, string.Empty) : (string.Empty, l.Name + StringHelper.__UniqueToken + l.Version),
                (key, l) => l.FirstOrDefault()).ToList();
        }
    }
}