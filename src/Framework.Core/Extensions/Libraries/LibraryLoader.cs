using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Libraries
{

    /// <summary>
    /// This class represents a loader of library.
    /// </summary>
    public static class LibraryLoader
    {

        /// <summary>
        /// Loads the library of this instance.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        /// <param name="libraryDefinition">The library definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <param name="dataSourceKinds">The kinds of data sources to consider.</param>
        /// <param name="libraryFolderPath">The library definition to consider.</param>
        /// <returns>The log of the load task.</returns>
        public static Library LoadLibrary(
            this AppDomain appDomain,
            LibraryDefinition libraryDefinition,
            Log log = null,
            List<AppExtensionItemKind> extensionItemKinds = null,
            List<DataSourceKind> dataSourceKinds = null,
            String libraryFolderPath = null)
        {
            if ((libraryDefinition == null) || (appDomain == null))
                return null;

            if (extensionItemKinds == null)
                extensionItemKinds = new List<AppExtensionItemKind>() { AppExtensionItemKind.Any };

            if (dataSourceKinds == null)
                dataSourceKinds = new List<DataSourceKind>()
                {
                    DataSourceKind.Memory,
                    DataSourceKind.Repository
                };

            // we instantiate a new instance of the library
            Library library = new Library(libraryDefinition);

            try
            {
                Assembly assembly = null;

                // first we load the using assemblies
                foreach (String usingAssemblyFileName in library.Definition.UsingAssemblyFileNames)
                    Assembly.LoadFrom(libraryFolderPath + usingAssemblyFileName);

                // we determine the location of this instance.
                foreach (DataSourceKind dataSourceKind in dataSourceKinds)
                {
                    Log subLog = new Log();

                    switch (dataSourceKind)
                    {
                        case DataSourceKind.Memory:
                            log?.AddCheckpoint("Loading assembly '" + library.Definition.AssemblyName + "' from dll");
                            assembly = AppDomainPool.LoadAssembly(appDomain, library.Definition.AssemblyName, subLog);
                            break;
                        case DataSourceKind.Repository:
                            string fileName = library.Definition.FileName;
                            if (string.IsNullOrEmpty(library.Definition.FileName))
                            {
                                fileName = libraryDefinition.AssemblyName + ".dll";
                            }
                            String filePath = libraryFolderPath.GetEndedString(@"\").ToPath() + fileName;
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

                    if (assembly != null)
                    {
                        if (!String.IsNullOrEmpty(libraryDefinition.Name))
                            library.Definition = LibraryLoader.GetLibraryDefinition(assembly, subLog);

                        if (!subLog.HasErrorsOrExceptions())
                        {
                            foreach (AppExtensionItemKind libraryItemKind in new List<AppExtensionItemKind>()
                            {
                                AppExtensionItemKind.Carrier,
                                AppExtensionItemKind.Connector,
                                //AppExtensionItemKind.ContextExtension,
                                AppExtensionItemKind.Entity,
                                AppExtensionItemKind.Handler,
                                AppExtensionItemKind.Metrics,
                                AppExtensionItemKind.RoutineConfiguration,
                                AppExtensionItemKind.ScriptWord,
                                AppExtensionItemKind.Task,
                            })
                            {
                                if ((extensionItemKinds.Contains(AppExtensionItemKind.Any)) || (extensionItemKinds.Contains(libraryItemKind)))
                                {
                                    Log subSubLog = new Log();
                                    int count = library.LoadItemIndex(assembly, libraryItemKind, subSubLog);
                                    if (subSubLog.HasErrorsOrExceptionsOrWarnings())
                                    {
                                        subLog.AddSubLog(
                                            subSubLog,
                                            title: "Loading '" + libraryItemKind.ToString() + "' index");
                                    }
                                    else
                                    {
                                        subLog.AddMessage("Index '" + libraryItemKind.ToString() + "' loaded (" + count.ToString() + " items added)");
                                    }
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

                if (assembly == null)
                {
                    log?.AddError("Could not load library '" +
                           (!String.IsNullOrEmpty(libraryDefinition.FileName) ? libraryDefinition.FileName : libraryDefinition.Name) + "'");
                }
            }
            catch (Exception exception)
            {
                log?.AddException(exception);
            }

            return (library.Assembly == null ? null : library);
        }

        private static LibraryDefinition GetLibraryDefinition(Assembly assembly, Log log = null)
        {
            log = log ?? new Log();
            LibraryDefinition libraryDefinition = LibraryDefinition.Load(assembly, null, log);
                if (libraryDefinition == null)
                    log.AddError("Error while attempting to load the library definition in assembly '" + assembly.GetName().Name + "'");

            return libraryDefinition;
        }

        /// <summary>
        /// Loads the script dictionary from the web service.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        private static Assembly Load_WebService(out Log log)
        {
            log = new Log();
            return null;
        }
    }
}
