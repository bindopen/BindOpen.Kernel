using BindOpen.Logging;
using System;
using System.Reflection;

namespace BindOpen.Extensions
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
        /// <param name="appDomain">The application domain to consider.</param>
        /// <param name="assemblyName">The name of the assembly to consider.</param>
        /// <returns></returns>
        public static Assembly GetAsssembly(this AppDomain appDomain, string assemblyName)
        {
            if (appDomain == null) return null;

            Assembly assembly = null;
            int i = appDomain.GetAssemblies().Length;
            if ((appDomain != null) && (assemblyName != null))
            {
                assemblyName = assemblyName.Trim();
                assembly = Array.Find(appDomain.GetAssemblies(), p => p.FullName.Contains(assemblyName));
            }

            return assembly;
        }

        /// <summary>
        /// Creates the instance of the specified extension object instance type.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        /// <param name="obj">The object to consider.</param>
        public static void CreateInstance(
            Type type,
            out object obj,
            IBdoLog log = null)
        {
            obj = null;

            try
            {
                obj = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                log?.AddException(ex);
            }
        }

        /// <summary>
        /// Creates the instance of the specified extension object instance type.
        /// </summary>
        /// <param name="fullyQualifiedName">The type fully qualified name to consider.</param>
        /// <param name="obj">The object to consider.</param>
        public static void CreateInstance(
            string fullyQualifiedName,
            out object obj,
            IBdoLog log = null)
        {
            obj = null;

            try
            {
                Type type = Type.GetType(fullyQualifiedName);
                if (type == null)
                {
                    log?.AddError("Unknown type '" + fullyQualifiedName + "'");
                }
                else
                {
                    obj = Activator.CreateInstance(type);
                }
            }
            catch (Exception ex)
            {
                log?.AddException(ex);
            }
        }

        /// <summary>
        /// Gets the root namespace.
        /// </summary>
        /// <param name="className">The class name to consider.</param>
        /// <returns>Returns the root namspace.</returns>
        public static string GetClassNameWithoutAssembly(this string className)
        {
            return className == null ? string.Empty : (className.Contains(',') ? className[..className.IndexOf(",")] : className);
        }
    }
}
