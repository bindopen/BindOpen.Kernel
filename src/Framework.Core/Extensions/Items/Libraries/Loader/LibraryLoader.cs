using System;
using System.IO;
using System.Reflection;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition.Libraries;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Libraries
{
    /// <summary>
    /// This class represents an library factory.
    /// </summary>
    public static class LibraryLoader
    {
        /// <summary>
        /// Loads the specified application extension filter.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        /// <param name="extensionFilter">The extension filter to consider.</param>
        /// <param name="log">The operation log to consider.</param>
        /// <returns>Returns the loaded library.</returns>
        public static ILibrary Load(
            AppDomain appDomain,
            IAppExtensionFilter extensionFilter,
            ILog log)
        {
            ILibrary library = default;

            if (extensionFilter != null)
            {
                try
                {
                    Assembly assembly = null;

                    // first we load the assembly

                    foreach (DataSourceKind dataSourceKind in extensionFilter.SourceKinds)
                    {
                        ILog subLog = new Log();

                        switch (dataSourceKind)
                        {
                            case DataSourceKind.Memory:
                                log?.AddCheckpoint("Loading assembly '" + extensionFilter.Name + "' from dll");
                                if (!string.IsNullOrEmpty(extensionFilter.Name))
                                {
                                    assembly = AppDomainPool.LoadAssembly(appDomain, extensionFilter.Name, subLog);
                                }
                                else
                                {
                                    log?.AddWarning("File name missing");
                                }
                                break;
                            case DataSourceKind.Repository:
                                string fileName = extensionFilter.FileName;
                                if (string.IsNullOrEmpty(fileName))
                                {
                                    fileName = extensionFilter.Name + ".dll";
                                }
                                String filePath = extensionFilter.FolderPath.GetEndedString(@"\").ToPath() + fileName;
                                if (!File.Exists(filePath))
                                {
                                    log?.AddError("Could not find the assembly file path '" + filePath + "'");
                                }
                                else
                                {
                                    assembly = AppDomainPool.LoadAssemblyFromFile(appDomain, filePath, subLog);

                                    if (assembly == null)
                                    {
                                        log?.AddError("Could not load assembly '" + filePath + "'");
                                    }
                                    else
                                    {
                                        log?.AddCheckpoint("Loading assembly from file '" + filePath + "'");
                                        assembly = Assembly.LoadFrom(filePath);
                                    }
                                }
                                break;
                            case DataSourceKind.RestApi:
                                break;
                        }

                        // if we have an assembly then we index library items

                        if (assembly != null)
                        {
                            library.Definition = LibraryDefinitionLoader.LoadFrom(assembly, null, subLog);

                            if (!subLog.HasErrorsOrExceptions())
                            {
                                foreach (AppExtensionItemKind kind in new[]
                                    {
                                        AppExtensionItemKind.Carrier,
                                        AppExtensionItemKind.Connector,
                                        AppExtensionItemKind.Entity,
                                        AppExtensionItemKind.Handler,
                                        AppExtensionItemKind.Metrics,
                                        AppExtensionItemKind.Routine,
                                        AppExtensionItemKind.Scriptword,
                                        AppExtensionItemKind.Task,
                                    })
                                {
                                    ILog subSubLog = new Log();
                                    int count = library.IndexItems(assembly, kind, false, subSubLog);

                                    if (subSubLog.HasErrorsOrExceptionsOrWarnings())
                                    {
                                        subLog.AddSubLog(
                                            subSubLog,
                                            title: "Loading '" + kind.ToString() + "' index");
                                    }
                                    else
                                    {
                                        subLog.AddMessage("Index '" + kind.ToString() + "' loaded (" + count.ToString() + " items added)");
                                    }
                                }
                            }

                            if (!subLog.HasErrorsOrExceptions())
                                library.Assembly = assembly;
                        }
                        //else
                        //    subLog.AddMessage("Could not find '" + dataSourceKind.ToString() + "' assembly");

                        log?.AddSubLog(subLog, p => p.HasErrorsOrExceptionsOrWarnings());

                        if (assembly != null) break;
                    }
                }
                catch (Exception exception)
                {
                    log?.AddException(exception);
                }
            }

            return library?.Assembly == null ? null : library;
        }
    }
}