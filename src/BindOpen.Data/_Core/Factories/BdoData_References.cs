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
        /// <param key="assemblyName">The name to consider.</param>
        /// <param key="assemblyVersion">The version to consider.</param>
        /// <returns>Returns the itemized list of the specified library list.</returns>
        public static BdoAssemblyReference Assembly(
            string assemblyName,
            Version assemblyVersion = null)
            => new(assemblyName, assemblyVersion);

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static BdoAssemblyReference AssemblyFrom(Type type)
        {
            var assembly = type?.Assembly;
            return Assembly(assembly);
        }

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static BdoAssemblyReference AssemblyFrom<T>()
            => AssemblyFrom(typeof(T));

        /// <summary>
        /// Creates a assembly reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static BdoAssemblyReference Assembly(Assembly assembly)
        {
            if (assembly == null) return null;

            var assemblyName = assembly?.GetName().Name;
            var assemblyVersion = assembly?.GetName().Version;
            return new(assemblyName, assemblyVersion);
        }

        /// <summary>
        /// Creates a assembly reference that represents all the references.
        /// </summary>
        /// <returns>Returns a assembly reference that represents all the references.</returns>
        public static BdoAssemblyReference AssemblyAsAll()
            => new(StringHelper.__Star);

        /// <summary>
        /// Gets the distinct list of the specified library reference list.
        /// </summary>
        /// <param key="references">The references to consider.</param>
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
        public static BdoClassReference Class(
            IBdoAssemblyReference assemblyReference,
            string className)
        {
            var assemblyName = assemblyReference?.AssemblyName;
            var assemblyVersion = assemblyReference?.AssemblyVersion;
            return new(assemblyName, assemblyVersion, className);
        }

        /// <summary>
        /// Creates a class reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static BdoClassReference ClassFromEntity(
            string definitionUniqueName)
        {
            return new BdoClassReference()
                .WithDefinition(definitionUniqueName);
        }

        /// <summary>
        /// Creates a class reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static BdoClassReference Class(Type type)
        {
            if (type == null) return null;

            var assemblyReference = Assembly(type.Assembly.GetName().Name, type.Assembly.GetName().Version);
            var className = type.FullName;

            return Class(
                assemblyReference,
                className);
        }

        /// <summary>
        /// Creates a class reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static BdoClassReference Class<T>()
        {
            var type = typeof(T);
            return Class(type);
        }

        /// <summary>
        /// Creates a class reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static BdoDataType NewDataType(
            DataValueTypes valueType,
            Type type = null)
        {
            return new BdoDataType()
                .WithValueType(valueType)
                .WithClassType(type);
        }

        public static BdoDataType NewDataType(
            Type type)
        {
            return new BdoDataType()
                .WithValueType(type.GetValueType())
                .WithClassType(type);
        }
    }
}