using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Scoping;
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
        public static IEnumerable<BdoAssemblyReference> GetDistinctReferences(
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
            var reference = new BdoClassReference(assemblyName, assemblyVersion, className)
                .WithAlias(assemblyReference?.Alias)
                .WithAssemblyFileName(assemblyReference?.AssemblyFileName);

            return reference;
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

        // DataType

        /// <summary>
        /// Creates a class reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static BdoDataType NewDataType(DataValueTypes valueType = DataValueTypes.Any)
        {
            return new BdoDataType()
                .WithValueType(valueType);
        }

        public static BdoDataType NewDataType(Type type)
        {
            var valueType = type.GetValueType();

            return new BdoDataType(Class(type))
                .WithValueType(valueType);
        }

        /// <summary>
        /// Creates a class reference of the spcefied type.
        /// </summary>
        /// <returns>Returns the itemized list of the spcefied type</returns>
        public static BdoDataType NewDataType<T>()
        {
            var type = typeof(T);
            return NewDataType(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static BdoDataType NewDataType(
            BdoExtensionKinds definitionExtensionKind,
            string definitionUniqueName = null)
        {
            return new BdoDataType()
                .WithDefinition(definitionExtensionKind, definitionUniqueName);
        }
    }
}