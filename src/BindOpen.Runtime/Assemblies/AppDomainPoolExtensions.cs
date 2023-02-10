using BindOpen.Data.Helpers;
using BindOpen.Logging;
using System;
using System.IO;
using System.Reflection;

namespace BindOpen.Runtime.Assemblies
{
    /// <summary>
    /// This class represents an assembly pool.
    /// </summary>
    public static class AppDomainPoolExtensions
    {
        /// <summary>
        /// Gets the assembly of this instance from file.
        /// </summary>
        /// <param name="appDomain">Application domain to consider.</param>
        /// <param name="filePath">Path of the file to use.</param>
        /// <param name="log">The loading log to consider.</param>
        /// <returns>The assembly of this instance.</returns>
        public static Assembly LoadAssemblyFromFile(
            this AppDomain appDomain,
            string filePath,
            IBdoLog log = null)
        {
            Assembly assembly = null;

            if (((appDomain != null) && (!string.IsNullOrEmpty(filePath)))
                && (File.Exists(filePath)))
            {
                string assemblyName = Path.GetFileNameWithoutExtension(filePath);
                foreach (Assembly currentAssembly in appDomain.GetAssemblies())
                {
                    if (!currentAssembly.IsDynamic)
                    {
                        string assemblyCodeBasePath = currentAssembly.Location.ToLower();
                        string assemblyFilePath = filePath.ToLower().Replace(@"\", "/");

                        if (assemblyCodeBasePath.Contains(assemblyFilePath))
                        {
                            assembly = currentAssembly;
                            break;
                        }
                    }
                }

                if (assembly == null)
                {
                    try
                    {
                        assembly = Assembly.LoadFrom(filePath);
                    }
                    catch (Exception ex)
                    {
                        log?.AddException(
                            title: "Error while attempting to load assembly from file '" + filePath + "'",
                            description: ex.ToString());
                    }
                }
            }

            return assembly;
        }

        /// <summary>
        /// Gets the assembly of this instance from embed resource.
        /// </summary>
        /// <param name="appDomain">Application domain to consider.</param>
        /// <param name="assemblyName">The assembly name to use.</param>
        /// <param name="log">The loading log to consider.</param>
        /// <returns>The assembly of this instance.</returns>
        public static Assembly LoadAssembly(
            this AppDomain appDomain,
            string assemblyName,
            IBdoLog log = null)
        {
            Assembly assembly = null;

            if ((appDomain != null) && (!string.IsNullOrEmpty(assemblyName)))
            {
                assembly = Array.Find(appDomain.GetAssemblies(), p => p.GetName().Name.BdoKeyEquals(assemblyName));
                if (assembly == null)
                {
                    try
                    {
                        assembly = Assembly.Load(assemblyName);
                    }
                    catch (FileNotFoundException)
                    {
                        log?.AddError("Could not find the assembly '" + assemblyName + "'");
                    }
                    catch (Exception ex)
                    {
                        log?.AddException("Error while attempting to load assembly '" + assemblyName + "'", description: ex.ToString());
                    }
                }
            }

            return assembly;
        }
    }
}
