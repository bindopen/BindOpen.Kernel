using BindOpen.Data.Helpers;
using BindOpen.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.Data.Assemblies
{
    /// <summary>
    /// This structure represents a string manager.
    /// </summary>
    public static class AssemblyHelper
    {
        // Assemblies --------------------------------

        /// <summary>
        /// Gets the specified assembly.
        /// </summary>
        /// <param key="appDomain">The application domain to consider.</param>
        /// <param key="assemblyName">The name of the assembly to consider.</param>
        /// <returns></returns>
        public static Assembly GetAssembly(
            this AppDomain appDomain,
            IBdoAssemblyReference reference)
        {
            if (appDomain == null) return null;

            Assembly assembly = null;

            if (appDomain != null && reference.IsEmpty() == false)
            {
                assembly = Array.Find(appDomain.GetAssemblies(),
                    q => BdoData.Assembly(q) == (BdoAssemblyReference)reference);
            }

            return assembly;
        }

        public static List<IBdoAssemblyReference> GetAssemblyReferences(
            this AppDomain appDomain,
            List<IBdoAssemblyReference> references)
        {
            if (references?.Any(q => q.BdoKeyEquals(StringHelper.__Star)) == true)
            {
                return appDomain.GetAssemblies().ToReferences();
            }

            return references;
        }

        public static List<IBdoAssemblyReference> ToReferences(
            this Assembly[] assemblies)
        {
            return assemblies?.Select(q => BdoData.Assembly(q))
                .Cast<IBdoAssemblyReference>().ToList();
        }

        public static List<IBdoAssemblyReference> ToReferences(
            this AssemblyName[] assemblies)
        {
            return assemblies?.Select(q => BdoData.Assembly(q.Name, q.Version.ToString()))
                .Cast<IBdoAssemblyReference>().ToList();
        }

        // Create instance

        /// <summary>
        /// Creates the instance of the specified extension object instance type.
        /// </summary>
        /// <param key="type">The type to consider.</param>
        /// <param key="obj">The object to consider.</param>
        public static object CreateInstance<T>(IBdoLog log = null)
        {
            return CreateInstance(typeof(T), log);
        }

        /// <summary>
        /// Creates the instance of the specified extension object instance type.
        /// </summary>
        /// <param key="type">The type to consider.</param>
        /// <param key="obj">The object to consider.</param>
        public static object CreateInstance(
            this Type type,
            IBdoLog log = null)
        {
            if (type == null)
            {
                log?.AddError("Unknown type");
                return null;
            }

            object obj = null;

            try
            {
                obj = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                log?.AddException(ex);
            }

            return obj;
        }

        /// <summary>
        /// Creates the instance of the specified extension object instance type.
        /// </summary>
        /// <param key="fullyQualifiedName">The type fully qualified name to consider.</param>
        /// <param key="obj">The object to consider.</param>
        public static void CreateInstance(
            string fullyQualifiedName,
            out object obj,
            IBdoLog log = null)
        {
            obj = null;

            try
            {
                Type type = Type.GetType(fullyQualifiedName);
                obj = type.CreateInstance();
            }
            catch (Exception ex)
            {
                log?.AddException(ex);
            }
        }
    }
}
