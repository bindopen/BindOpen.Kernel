using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.Extensions.Libraries;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents an application scope helper.
    /// </summary>
    public static class AppScopeHelper
    {
        /// <summary>
        /// Creates a new scope.
        /// </summary>
        /// <returns>The log of check log.</returns>
        public static IAppScope CreateScope(AppDomain appDomain = null) => new AppScope(appDomain);

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="isAppExtensionChecked">Indicates whether the application extension is chekced.</param>
        /// <param name="isScriptInterpreterChecked">Indicates whether the script interpreter is chekced.</param>
        /// <param name="isDataContextChecked">Indicates whether the data context is chekced.</param>
        /// <param name="isDataSourceDepotChecked">Indicates whether the data module manager is chekced.</param>
        /// <returns>The log of check log.</returns>
        public static ILog Check(
        this IAppScope appScope,
        bool isAppExtensionChecked = false,
        bool isScriptInterpreterChecked = false,
        bool isDataContextChecked = false,
        bool isDataSourceDepotChecked = false)
        {
            ILog log = new Log();

            if (appScope == null)
            {
                log.AddError(title: "Application scope missing", description: "No application scope specified.");
            }
            else
            {
                if (isAppExtensionChecked && appScope.Extension == null)
                    log.AddError(title: "Application extension missing", description: "No application extension specified.");
                if (isScriptInterpreterChecked && appScope.Interpreter == null)
                    log.AddError(title: "Script interpreter missing", description: "No script interpreter specified.");
                if (isDataContextChecked && appScope.Context == null)
                    log.AddError(title: "Data context missing", description: "No data context specified.");
                if (isDataSourceDepotChecked && appScope.DataSourceDepot == null)
                    log.AddError(title: "Data module dictionary missing", description: "No data module manager specified.");
            }

            return log;
        }

        // From config -------------------------------

        /// <summary>
        /// Adds the specified libraries.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="config">The application extension configuration to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <returns>Reurns the opeartion log.</returns>
        public static ILog AppendLibraries(
            this IAppScope appScope,
            IAppExtensionConfiguration config,
            string folderPath = null)
        {
            ILog log = new Log();

            if (appScope?.Extension == null) return log;

            if (config != null)
            {
                var libraries = appScope.Extension.GetLibraries();

                foreach (IAppExtensionFilter extensionFilter in config.GetFilters())
                {
                    Log subLog = new Log();

                    string defaultFolderPath = string.IsNullOrEmpty(extensionFilter.FolderPath) ?
                        config.DefaultFolderPath : extensionFilter.FolderPath;
                    if (string.IsNullOrEmpty(defaultFolderPath))
                        defaultFolderPath = folderPath;

                    ILibrary library = LibraryLoader.Load(appScope.Extension.AppDomain, extensionFilter, subLog);

                    if (library != null && !log.HasErrorsOrExceptions()
                        && !libraries.Any(p => p.Definition?.KeyEquals(library.Definition) == true))
                    {
                        appScope.Extension.AddLibrary(library);
                    }

                    if (subLog.HasErrorsOrExceptionsOrWarnings())
                        log.AddSubLog(subLog, title: "Loading library '" + (extensionFilter?.Name ?? "?") + "'");
                    else
                        log.AddMessage("Library '" + (extensionFilter?.Name ?? "?") + "' loaded");
                }
            }

            return log;
        }

        // From file -------------------------------

        /// <summary>
        /// Adds the specifed libraries in the specified way.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="filePaths">The file paths to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public static ILog AppendFromFiles(
            this IAppScope appScope,
            string filePaths,
            string folderPath)
        {
            ILog log = new Log();

            if (appScope?.Extension == null) return log;

            Log subLog = null;

            folderPath = folderPath.GetEndedString(@"\").ToPath();

            if (string.IsNullOrEmpty(filePaths))
            {
                log.AddError("Assembly file path missing");
            }
            else
            {
                var libraries = appScope.Extension.GetLibraries();

                foreach (string subFilePath in filePaths.Split('|'))
                {
                    string completeSubFilePath = (folderPath + subFilePath).ToPath();

                    List<string> completeSubFilePaths = new List<string>();
                    if (completeSubFilePath.Contains('*'))
                    {
                        try
                        {
                            completeSubFilePaths = Directory.GetFiles(
                                Path.GetDirectoryName(completeSubFilePath),
                                Path.GetFileName(completeSubFilePath)).ToList();
                        }
                        catch
                        {
                            log.AddError("Could not find the assembly file path '" + completeSubFilePath + "'");
                        }
                    }
                    else
                    {
                        completeSubFilePaths = new List<string>() { completeSubFilePath };
                    }

                    foreach (string filePath in completeSubFilePaths)
                    {
                        subLog = new Log();

                        ILibrary library = LibraryLoader.Load(
                            appScope.Extension.AppDomain,
                            new AppExtensionFilter(
                                null,
                                Path.GetFileName(filePath),
                                new[] { DataSourceKind.Repository },
                                Path.GetDirectoryName(filePath)),
                            subLog);

                        log.AddSubLog(subLog, p => p.HasErrorsOrExceptionsOrWarnings(), title: "Loading assembly '" + filePath + "'");

                        if (library != null && !log.HasErrorsOrExceptions())
                        {
                            if (!libraries.Any(p => p.Definition?.Id.KeyEquals(library.Definition.Id) == true))
                            {
                                appScope.Extension.AddLibrary(library);
                            }
                        }
                    }
                }
            }

            return log;
        }
    }
}