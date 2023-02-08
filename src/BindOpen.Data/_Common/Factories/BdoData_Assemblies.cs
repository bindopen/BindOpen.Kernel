using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an application scope factory.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Creates a assembly reference from the specified extension one.
        /// </summary>
        /// <param name="assemblyName">The name to consider.</param>
        /// <param name="assemblyVersion">The version to consider.</param>
        /// <returns>Returns the itemized list of the specified library list.</returns>
        public static IBdoAssemblyReference Assembly(
            string assemblyName,
            string assemblyVersion = null)
            => new BdoAssemblyReference(assemblyName, assemblyVersion);

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoAssemblyReference AssemblyFrom(Type type)
        {
            var assembly = type?.Assembly;
            return Assembly(assembly);
        }

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoAssemblyReference AssemblyFrom<T>()
            => AssemblyFrom(typeof(T));

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoAssemblyReference Assembly(Assembly assembly)
        {
            string assemblyName = assembly?.GetName().Name;
            string assemblyVersion = assembly?.GetName().Version.ToString();
            return new BdoAssemblyReference(assemblyName, assemblyVersion);
        }

        /// <summary>
        /// Creates a assembly reference that represents all the references.
        /// </summary>
        /// <returns>Returns a assembly reference that represents all the references.</returns>
        public static IBdoAssemblyReference AssemblyAsAll()
            => new BdoAssemblyReference(StringHelper.__Star);

        /// <summary>
        /// Gets the distinct list of the specified library reference list.
        /// </summary>
        /// <param name="references">The references to consider.</param>
        /// <returns>Returns the itemized list of the specified library list.</returns>
        public static List<BdoAssemblyReference> GetDistinctReferences(
            this IEnumerable<BdoAssemblyReference> references)
        {
            return references.GroupBy(
                l => l.Key(),
                (key, l) => l.FirstOrDefault()).ToList();
        }

        // Class

        /// <summary>
        /// Creates a class reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoClassReference Class(
            IBdoAssemblyReference assemblyReference,
            string className)
        {
            string assemblyName = assemblyReference?.AssemblyName;
            string assemblyVersion = assemblyReference?.AssemblyVersion;
            return new BdoClassReference(assemblyName, assemblyVersion, className);
        }

        /// <summary>
        /// Creates a class reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoClassReference ClassFromEntity(
            string definitionUniqueId)
        {
            return new BdoClassReference()
                .WithDefinitionUniqueId(definitionUniqueId);
        }

        /// <summary>
        /// Creates a class reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static IBdoClassReference Class<T>()
        {
            var type = typeof(T);
            var assemblyReference = Assembly(
                type.Assembly.GetName().FullName,
                type.Assembly.GetName().Version.ToString());
            var className = type.FullName;
            return Class(
                assemblyReference,
                className);
        }
    }
}