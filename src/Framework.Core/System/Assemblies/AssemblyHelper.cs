using BindOpen.Framework.System.Diagnostics;
using System;
using System.Reflection;

namespace BindOpen.Framework.System.Assemblies
{
    /// <summary>
    /// This structure represents a string manager.
    /// </summary>
    public static class AssemblyHelper
    {
        // --------------------------------------------------
        // ENUMERATIONS
        // --------------------------------------------------

        #region Enumerations

        /// <summary>
        /// This structure represents an class reference.
        /// </summary>
        public struct ClassReference
        {
            /// <summary>
            /// Library name.
            /// </summary>
            public string AssemblyName;

            /// <summary>
            /// Class name.
            /// </summary>
            public string ClassName;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

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
        /// <param name="object1">The object to consider.</param>
        /// <param name="attributes">The attributes to consider.</param>
        public static IBdoLog CreateInstance(
            Type type,
            out Object object1,
            params object[] attributes)
        {
            var log = new BdoLog();
            object1 = null;

            try
            {
                object1 = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                log.AddException(ex);
            }

            return log;
        }

        /// <summary>
        /// Creates the instance of the specified extension object instance type.
        /// </summary>
        /// <param name="fullyQualifiedName">The type fully qualified name to consider.</param>
        /// <param name="object1">The object to consider.</param>
        /// <param name="attributes">The attributes to consider.</param>
        public static IBdoLog CreateInstance(
            string fullyQualifiedName,
            out Object object1,
            params object[] attributes)
        {
            var log = new BdoLog();
            object1 = null;

            try
            {
                Type type = Type.GetType(fullyQualifiedName);
                if (type == null)
                {
                    log.AddError("Unknown type '" + fullyQualifiedName + "'");
                }
                else
                {
                    object1 = Activator.CreateInstance(type);
                }
            }
            catch (Exception ex)
            {
                log.AddException(ex);
            }

            return log;
        }

        /// <summary>
        /// Gets the root namespace.
        /// </summary>
        /// <param name="className">The class name to consider.</param>
        /// <returns>Returns the root namspace.</returns>
        public static string GetClassNameWithoutAssembly(this string className)
        {
            return className == null ? "" : (className.Contains(",") ? className.Substring(0, className.IndexOf(",")) : className);
        }

        #endregion
    }
}
