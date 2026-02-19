using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides extension methods for <see cref="AppDomain"/> 
/// to handle assembly discovery and loading.
/// </summary>
public static class AppDomainPoolExtensions
{
    /// <summary>
    /// Loads or retrieves an assembly from the specified file path.
    /// </summary>
    /// <param name="appDomain">The application domain to use for loading.</param>
    /// <param name="filePath">The full path to the assembly file.</param>
    /// <param name="log">The log to populate with loading events.</param>
    /// <returns>Returns the loaded <see cref="Assembly"/>; otherwise, null.</returns>
    public static Assembly LoadAssemblyFromFile(
        this AppDomain appDomain,
        string filePath,
        IBdoLog log = null)
    {
        if (appDomain == null || string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            return null;
        }

        Assembly assembly = null;
        string targetPath = filePath.Replace(@"\", "/").ToLower();

        // 1. Check if the assembly is already loaded in the AppDomain
        foreach (var currentAssembly in appDomain.GetAssemblies().Where(q => !q.IsDynamic))
        {
            try
            {
                string currentPath = currentAssembly.Location.Replace(@"\", "/").ToLower();
                if (currentPath.Equals(targetPath, StringComparison.OrdinalIgnoreCase))
                {
                    assembly = currentAssembly;
                    break;
                }
            }
            catch { /* Some assemblies may not have a location */ }
        }

        // 2. If not found, attempt to load it from the file system
        if (assembly == null)
        {
            try
            {
                assembly = Assembly.LoadFrom(filePath);
            }
            catch (Exception ex)
            {
                log?.AddEvent(BdoEventKinds.Exception,
                    $"Error while attempting to load assembly from file '{filePath}'",
                    ex.ToString());
            }
        }

        return assembly;
    }

    /// <summary>
    /// Loads or retrieves an assembly based on a BindOpen assembly reference.
    /// </summary>
    /// <param name="appDomain">The application domain to use for loading.</param>
    /// <param name="reference">The assembly reference containing name and version.</param>
    /// <param name="log">The log to populate with loading events.</param>
    /// <returns>Returns the loaded <see cref="Assembly"/>; otherwise, null.</returns>
    public static Assembly LoadAssembly(
        this AppDomain appDomain,
        IBdoAssemblyReference reference,
        IBdoLog log = null)
    {
        if (appDomain == null) return null;

        if (reference?.IsEmpty() != false)
        {
            log?.AddEvent(BdoEventKinds.Warning, "Assembly reference is missing or empty.");
            return null;
        }

        // 1. Attempt to retrieve from already loaded assemblies
        Assembly assembly = appDomain.GetAssembly(reference);

        // 2. If not found, attempt a standard assembly load
        if (assembly == null)
        {
            try
            {
                var assemblyName = new AssemblyName
                {
                    Name = reference.AssemblyName,
                    Version = reference.AssemblyVersion
                };
                assembly = Assembly.Load(assemblyName);
            }
            catch (FileNotFoundException)
            {
                log?.AddEvent(BdoEventKinds.Error, $"Could not find the assembly '{reference}'");
            }
            catch (Exception ex)
            {
                log?.AddEvent(BdoEventKinds.Exception,
                    $"Error while attempting to load assembly '{reference}'",
                    ex.ToString());
            }
        }

        return assembly;
    }
}