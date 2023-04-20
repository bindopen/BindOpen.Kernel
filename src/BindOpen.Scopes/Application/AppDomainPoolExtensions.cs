using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using System;
using System.IO;
using System.Reflection;

namespace BindOpen.Scopes.Application
{
    /// <summary>
    /// This class represents an assembly pool.
    /// </summary>
    public static class AppDomainPoolExtensions
    {
        /// <summary>
        /// Gets the assembly of this instance from file.
        /// </summary>
        /// <param key="appDomain">Application domain to consider.</param>
        /// <param key="filePath">Path of the file to use.</param>
        /// <param key="log">The loading log to consider.</param>
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
                foreach (var currentAssembly in appDomain.GetAssemblies())
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
                        log?.AddEvent(EventKinds.Exception,
                            "Error while attempting to load assembly from file '" + filePath + "'",
                            ex.ToString());
                    }
                }
            }

            return assembly;
        }

        /// <summary>
        /// Gets the assembly of this instance from embed resource.
        /// </summary>
        /// <param key="appDomain">Application domain to consider.</param>
        /// <param key="assemblyName">The assembly name to use.</param>
        /// <param key="log">The loading log to consider.</param>
        /// <returns>The assembly of this instance.</returns>
        public static Assembly LoadAssembly(
            this AppDomain appDomain,
            IBdoAssemblyReference reference,
            IBdoLog log = null)
        {
            Assembly assembly = null;

            if (reference?.IsEmpty() != false)
            {
                log?.AddEvent(EventKinds.Warning, "Assembly name missing");
                return null;
            }

            if (appDomain != null)
            {
                assembly = appDomain.GetAssembly(reference);

                if (assembly == null)
                {
                    try
                    {
                        var assemblyName = new AssemblyName()
                        {
                            Name = reference.AssemblyName,
                            Version = reference.AssemblyVersion
                        };
                        assembly = Assembly.Load(assemblyName);
                    }
                    catch (FileNotFoundException)
                    {
                        log?.AddEvent(EventKinds.Error, "Could not find the assembly '" + reference.ToString() + "'");
                    }
                    catch (Exception ex)
                    {
                        log?.AddEvent(EventKinds.Exception,
                            "Error while attempting to load assembly '" + reference.ToString() + "'",
                            ex.ToString());
                    }
                }
            }

            return assembly;
        }
    }
}
