using System;
using System.IO;
using System.Reflection;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definitions.Libraries;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Extensions.Libraries
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
            ILibrary library = new Library();

            if (extensionFilter != null)
            {
                try
                {
                    Assembly assembly = null;

                    // first we load the assembly

                    ILog firstLog = new Log() { Title = "Loading library '" + extensionFilter.Name + "'" };

                    foreach (DataSourceKind dataSourceKind in extensionFilter.SourceKinds)
                    {
                        ILog subLog = null;
                        switch (dataSourceKind)
                        {
                            case DataSourceKind.Memory:
                                subLog = new Log() { Title = "Loading assembly '" + extensionFilter.Name + "' from dll" };
                                if (!string.IsNullOrEmpty(extensionFilter.Name))
                                {
                                    assembly = AppDomainPool.LoadAssembly(appDomain, extensionFilter.Name, subLog);
                                }
                                else
                                {
                                    subLog?.AddWarning("File name missing");
                                }
                                break;
                            case DataSourceKind.Repository:
                                string fileName = extensionFilter.FileName;
                                if (string.IsNullOrEmpty(fileName))
                                {
                                    fileName = extensionFilter.Name + ".dll";
                                }
                                subLog = new Log() { Title = "Loading assembly '" + extensionFilter.Name + "' from file '" + fileName + "'" };

                                String filePath = extensionFilter.FolderPath.GetEndedString(@"\").ToPath() + fileName;
                                if (!File.Exists(filePath))
                                {
                                    subLog?.AddError("Could not find the assembly file path '" + filePath + "'");
                                }
                                else
                                {
                                    assembly = AppDomainPool.LoadAssemblyFromFile(appDomain, filePath, subLog);

                                    if (assembly == null)
                                    {
                                        subLog?.AddError("Could not load assembly '" + filePath + "'");
                                    }
                                    else
                                    {
                                        subLog?.AddCheckpoint("Loading assembly from file '" + filePath + "'");
                                        assembly = Assembly.LoadFrom(filePath);
                                    }
                                }
                                break;
                            case DataSourceKind.RestApi:
                                break;
                        }

                        firstLog.AddSubLog(subLog);

                        if (assembly != null) break;
                    }

                    // if we have an assembly then we index library items

                    if (assembly == null)
                    {
                        log?.AddSubLog(firstLog, p => p.HasErrorsOrExceptionsOrWarnings());
                    }
                    else
                    {
                        firstLog.GetEvents(true, EventKind.Error, EventKind.Exception).ForEach(p => p.Kind = EventKind.Warning);
                        log?.AddSubLog(firstLog);

                        library.Definition = LibraryDefinitionLoader.LoadFrom(assembly, null, log);
                        library.Name = library.DefinitionName;
                        library.Assembly = assembly;

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
                                log.AddSubLog(
                                    subSubLog,
                                    title: "Index '" + kind.ToString() + "' loaded (" + count.ToString() + " items added)");
                            }
                            else
                            {
                                log.AddMessage("Index '" + kind.ToString() + "' loaded (" + count.ToString() + " items added)");
                            }
                        }

                        if (!log.HasErrorsOrExceptions())
                            library.Assembly = assembly;
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