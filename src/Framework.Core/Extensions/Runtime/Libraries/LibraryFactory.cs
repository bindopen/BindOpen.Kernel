using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Configuration.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Entities;
using BindOpen.Framework.Core.Extensions.Definition.Handlers;
using BindOpen.Framework.Core.Extensions.Definition.Metrics;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;
using BindOpen.Framework.Core.Extensions.Runtime.Handlers;
using BindOpen.Framework.Core.Extensions.Runtime.Scriptwords;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Libraries
{
    /// <summary>
    /// This class represents an library factory.
    /// </summary>
    public static class LibraryFactory
    {
        /// <summary>
        /// This class represents a library definition factory.
        /// </summary>
        public static class LibraryDefinitionFactory
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
            public static Library CreateLibrary(
                this AppDomain appDomain,
                LibraryDefinition libraryDefinition,
                ILog log = null,
                AppExtensionItemKind[] extensionItemKinds = null,
                DataSourceKind[] dataSourceKinds = null,
                String libraryFolderPath = null)
            {
                if ((libraryDefinition == null) || (appDomain == null))
                    return null;

                if (extensionItemKinds == null)
                {
                    extensionItemKinds = new[] { AppExtensionItemKind.Any };
                }

                if (dataSourceKinds == null)
                {
                    dataSourceKinds = new[]
                    {
                    DataSourceKind.Memory,
                    DataSourceKind.Repository
                };
                }

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
                            if (!string.IsNullOrEmpty(libraryDefinition.Name))
                                library.Definition = LibraryLoader.GetLibraryDefinition(assembly, subLog);

                            if (!subLog.HasErrorsOrExceptions())
                            {
                                foreach (AppExtensionItemKind libraryItemKind in new List<AppExtensionItemKind>()
                            {
                                AppExtensionItemKind.Carrier,
                                AppExtensionItemKind.Connector,
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
                               (!string.IsNullOrEmpty(libraryDefinition.FileName) ? libraryDefinition.FileName : libraryDefinition.Name) + "'");
                    }
                }
                catch (Exception exception)
                {
                    log?.AddException(exception);
                }

                return (library.Assembly == null ? null : library);
            }

            // ------------------------------------------
            // MUTATORS
            // ------------------------------------------

            #region Mutators

            /// <summary>
            /// Loads the specified library item index.
            /// </summary>
            /// <param name="assembly">The assembly to consider.</param>
            /// <param name="libraryItemKind">The extension item kind to consider.</param>
            /// <param name="log">The log to consider.</param>
            /// <returns>Returns the number of items.</returns>
            public int LoadItemIndex(Assembly assembly, AppExtensionItemKind libraryItemKind, ILog log = null)
        {
            if ((this.Definition == null) || (assembly == null))
            {
                if (this.Definition == null)
                {
                    log?.AddError(
                           "Library definition missing",
                           description: "The library '" + this.DefinitionName + "' definition is missing.");
                }

                return 0;
            }

            int count = 0;
            XmlSerializer xmlSerializer = null;
            Stream stream = null;
            try
            {
                String resourceFileFullName = this.Definition.GetItemIndexResourceFullName(libraryItemKind);
                stream = assembly.GetManifestResourceStream(resourceFileFullName);

                if (stream == null)
                {
                    log?.AddWarning(
                                "'" + libraryItemKind.ToString() + "' index not found in assembly",
                                description: "'" + libraryItemKind.ToString() + "' library item index ('" + resourceFileFullName + "') not found in assembly.");
                }
                else
                {
                    Type dictionaryType = AppExtension.GetDictionaryType(libraryItemKind);
                    if (dictionaryType != null)
                    {
                        xmlSerializer = new XmlSerializer(dictionaryType);

                        String itemIndexLibraryId = "";
                        String itemIndexLibraryName = "";
                        List<AppExtensionItemDefinition> itemDefinitions = new List<AppExtensionItemDefinition>();
                        String defaultDefinitionClass = null;

                        switch (libraryItemKind)
                        {
                            case AppExtensionItemKind.Carrier:
                                CarrierIndex extensionItemIndex_Carrier = xmlSerializer.Deserialize(stream) as CarrierIndex;
                                itemIndexLibraryId = extensionItemIndex_Carrier.LibraryId;
                                itemIndexLibraryName = extensionItemIndex_Carrier.LibraryName;
                                itemDefinitions = extensionItemIndex_Carrier.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                                break;
                            case AppExtensionItemKind.Task:
                                TaskIndex extensionItemIndex_Task = xmlSerializer.Deserialize(stream) as TaskIndex;
                                itemIndexLibraryId = extensionItemIndex_Task.LibraryId;
                                itemIndexLibraryName = extensionItemIndex_Task.LibraryName;
                                itemDefinitions = extensionItemIndex_Task.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                                break;
                            case AppExtensionItemKind.Connector:
                                ConnectorIndex extensionItemIndex_Connector = xmlSerializer.Deserialize(stream) as ConnectorIndex;
                                itemIndexLibraryId = extensionItemIndex_Connector.LibraryId;
                                itemIndexLibraryName = extensionItemIndex_Connector.LibraryName;
                                itemDefinitions = extensionItemIndex_Connector.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                                break;
                            //case AppExtensionItemKind.ContextExtension:
                            //    ContextIndex extensionItemIndex_ContextExtension = xmlSerializer.Deserialize(stream) as ContextIndex;
                            //    aItemIndexLibraryId = extensionItemIndex_ContextExtension.LibraryId;
                            //    aItemIndexLibraryName = extensionItemIndex_ContextExtension.LibraryName;
                            //    itemDefinitions = extensionItemIndex_ContextExtension.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                            //    break;
                            case AppExtensionItemKind.Entity:
                                EntityIndex extensionItemIndex_Entity = xmlSerializer.Deserialize(stream) as EntityIndex;
                                itemIndexLibraryId = extensionItemIndex_Entity.LibraryId;
                                itemIndexLibraryName = extensionItemIndex_Entity.LibraryName;
                                itemDefinitions = extensionItemIndex_Entity.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                                break;
                            case AppExtensionItemKind.Metrics:
                                MetricsIndex extensionItemIndex_Metrics = xmlSerializer.Deserialize(stream) as MetricsIndex;
                                itemIndexLibraryId = extensionItemIndex_Metrics.LibraryId;
                                itemIndexLibraryName = extensionItemIndex_Metrics.LibraryName;
                                itemDefinitions = extensionItemIndex_Metrics.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                                break;
                            case AppExtensionItemKind.Handler:
                                HandlerIndex extensionItemIndex_Handler = xmlSerializer.Deserialize(stream) as HandlerIndex;
                                itemIndexLibraryId = extensionItemIndex_Handler.LibraryId;
                                itemIndexLibraryName = extensionItemIndex_Handler.LibraryName;
                                //definitionClassFullName = extensionItemIndex_Reference.DefinitionClass;
                                itemDefinitions = extensionItemIndex_Handler.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                                break;
                            case AppExtensionItemKind.RoutineConfiguration:
                                RoutineIndex extensionItemIndex_RoutineConfiguration = xmlSerializer.Deserialize(stream) as RoutineIndex;
                                itemIndexLibraryId = extensionItemIndex_RoutineConfiguration.LibraryId;
                                itemIndexLibraryName = extensionItemIndex_RoutineConfiguration.LibraryName;
                                itemDefinitions = extensionItemIndex_RoutineConfiguration.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                                break;
                            case AppExtensionItemKind.ScriptWord:
                                ScriptWordIndex extensionItemIndex_ScriptWord = xmlSerializer.Deserialize(stream) as ScriptWordIndex;
                                itemIndexLibraryId = extensionItemIndex_ScriptWord.LibraryId;
                                itemIndexLibraryName = extensionItemIndex_ScriptWord.LibraryName;
                                defaultDefinitionClass = this.UpdateClassFullName(extensionItemIndex_ScriptWord.DefinitionClass, AppExtensionItemKind.ScriptWord);
                                itemDefinitions = extensionItemIndex_ScriptWord.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                                break;
                        }

                        if ((string.IsNullOrEmpty(itemIndexLibraryId) || (string.IsNullOrEmpty(this.Id)) || (!string.Equals(itemIndexLibraryId, this.Id, StringComparison.OrdinalIgnoreCase)))
                            && (string.IsNullOrEmpty(itemIndexLibraryName) || (string.IsNullOrEmpty(this.DefinitionName)) || (!string.Equals(itemIndexLibraryName, this.DefinitionName, StringComparison.OrdinalIgnoreCase))))
                        {
                            log?.AddError("Bad library specified in the '" + libraryItemKind.ToString() + "' index",
                                   description: "The library specified in the '" + libraryItemKind.ToString() + "' index should be '" + this.DefinitionName + "'.");
                        }
                        else
                        {
                            foreach (AppExtensionItemDefinition extensionItemDefinition in itemDefinitions)
                                this.UpdateItemIndex(extensionItemDefinition,assembly, ref count, defaultDefinitionClass, log);

                            log?.AddMessage(
                                    title: count.ToString() + " '" + libraryItemKind.ToString() + "' items loaded",
                                    description: count.ToString() + " '" + libraryItemKind.ToString() + "' items loaded out of " + itemDefinitions.Count + " definitions " +
                                    "(" + (itemDefinitions.Count - count) + " items missing)");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //log.AddCheckpoint("Loading the '" + libraryItemKind.ToString() + "' index of the library '" + this.DefinitionUniqueName + "'");

                log?.AddException(exception);
            }
            finally
            {
                stream?.Close();
            }

            return count;
        }


        /// <summary>
        /// Updates the specified item index.
        /// </summary>
        /// <param name="extensionItemDefinition">The extension item definition to update.</param>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="count">The number of added items to consider.</param>
        /// <param name="defaultDefinitionClass">The default definition class to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="mustAddDefinition">Indicates whether the defitinion must be added.</param>
        private int UpdateItemIndex(
            AppExtensionItemDefinition extensionItemDefinition,
            Assembly assembly,
            ref int count,
            String defaultDefinitionClass = null,
            ILog log = null,
            bool mustAddDefinition = true)
        {
            if (assembly == null || extensionItemDefinition == null) return 0;

            log = log ?? new Log();
            Type type = null;

            extensionItemDefinition.LibraryName = this.DefinitionName;
            extensionItemDefinition.UniqueId = extensionItemDefinition.LibraryName + "$" + extensionItemDefinition.Name;

            switch (extensionItemDefinition.GetExtensionItemKind())
            {
                case AppExtensionItemKind.Carrier:
                    (extensionItemDefinition as CarrierDefinition).ItemClass = this.UpdateClassFullName(
                        (extensionItemDefinition as CarrierDefinition)?.ItemClass, AppExtensionItemKind.Carrier);
                    type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly(
                        (extensionItemDefinition as CarrierDefinition)?.ItemClass));
                    if (type == null)
                    {
                        log.AddError(
                            title: "Carrier '" + (extensionItemDefinition as CarrierDefinition)?.Key() + "' class not found",
                            description: "Could not find the defined connector class '" + (extensionItemDefinition as CarrierDefinition)?.ItemClass.ToLower() + "'.");
                    }
                    else if (mustAddDefinition)
                    {
                        this._carrierDefinitions.Add(extensionItemDefinition as CarrierDefinition);
                        log.AddMessage(title: "Carrier '" + (extensionItemDefinition as CarrierDefinition)?.Key() + "' loaded");
                    }

                    count = this._carrierDefinitions.Count;
                    break;
                case AppExtensionItemKind.Connector:
                    (extensionItemDefinition as ConnectorDefinition).ItemClass = this.UpdateClassFullName(
                        (extensionItemDefinition as ConnectorDefinition)?.ItemClass, AppExtensionItemKind.Connector);
                    type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly(
                        (extensionItemDefinition as ConnectorDefinition)?.ItemClass));
                    if (type == null)
                    {
                        log.AddError(
                            title: "Connector '" + (extensionItemDefinition as ConnectorDefinition)?.Key() + "' class not found",
                            description: "Could not find the defined connector class '" + (extensionItemDefinition as ConnectorDefinition)?.ItemClass.ToLower() + "'.");
                    }
                    else if (mustAddDefinition)
                    {
                        this._connectorDefinitions.Add(extensionItemDefinition as ConnectorDefinition);
                        log.AddMessage(title: "Connector '" + (extensionItemDefinition as ConnectorDefinition)?.Key() + "' loaded");
                    }

                    count = this._connectorDefinitions.Count;
                    break;
                //case AppExtensionItemKind.ContextExtension:
                //    (extensionItemDefinition as DataContextExtensionDefinition).ItemClass = this.UpdateClassFullName(
                //        (extensionItemDefinition as DataContextExtensionDefinition).ItemClass, libraryItemKind);
                //    aType = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly(
                //        (extensionItemDefinition as DataContextExtensionDefinition).ItemClass));
                //    if (aType == null)
                //        log.AddWarning(
                //            title: "Context extension '" + (extensionItemDefinition as DataContextExtensionDefinition).UniqueName + "' class not found",
                //            description: "Could not find the defined context extension class '" + (extensionItemDefinition as DataContextExtensionDefinition).ItemClass.ToLower() + "'.");
                //    else
                //    {
                //        this._ContextExtensionDefinitions.Add(extensionItemDefinition as DataContextExtensionDefinition);
                //        log.AddMessage(title: "Context extension '" + (extensionItemDefinition as DataContextExtensionDefinition).UniqueName + "' loaded");
                //    }

                //    count = this._ContextExtensionDefinitions.Count;
                //    break;
                case AppExtensionItemKind.Entity:
                    (extensionItemDefinition as EntityDefinition).ItemClass = this.UpdateClassFullName(
                        (extensionItemDefinition as EntityDefinition)?.ItemClass, AppExtensionItemKind.Entity);
                    type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly(
                        (extensionItemDefinition as EntityDefinition)?.ItemClass));
                    if (type == null)
                    {
                        log.AddError(
                            title: "Entity '" + (extensionItemDefinition as EntityDefinition)?.Key() + "' class not found",
                            description: "Could not find the defined entity class '" + (extensionItemDefinition as EntityDefinition)?.ItemClass.ToLower() + "'.");
                    }
                    else if (mustAddDefinition)
                    {
                        this._entityDefinitions.Add(extensionItemDefinition as EntityDefinition);
                        log.AddMessage(title: "Entity '" + (extensionItemDefinition as EntityDefinition)?.Key() + "' loaded");
                    }

                    count = this._entityDefinitions.Count;
                    break;
                case AppExtensionItemKind.Handler:
                    (extensionItemDefinition as HandlerDefinition).CallingClass = this.UpdateClassFullName(
                        (extensionItemDefinition as HandlerDefinition)?.CallingClass, AppExtensionItemKind.Handler);
                    type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly((extensionItemDefinition as HandlerDefinition)?.CallingClass));
                    if (type == null)
                    {
                        log.AddWarning(
                            title: "Data handler '" + (extensionItemDefinition as HandlerDefinition)?.Key() + "' class not found",
                            description: "Could not find the defined handler class '" + (extensionItemDefinition as HandlerDefinition)?.CallingClass.ToLower() + "'.");
                    }
                    else if (mustAddDefinition && this.AssignRuntimeFunction(assembly, extensionItemDefinition, log))
                    {
                        this._handlerDefinitions.Add(extensionItemDefinition as HandlerDefinition);
                        log.AddMessage(title: "Data handler '" + (extensionItemDefinition as HandlerDefinition)?.Key() + "' loaded");
                    }

                    count = this._handlerDefinitions.Count;
                    break;
                case AppExtensionItemKind.Metrics:
                    (extensionItemDefinition as MetricsDefinition).ItemClass = this.UpdateClassFullName(
                        (extensionItemDefinition as MetricsDefinition)?.ItemClass, AppExtensionItemKind.Metrics);
                    type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly(
                        (extensionItemDefinition as MetricsDefinition)?.ItemClass));
                    if (type == null)
                    {
                        log.AddError(
                            title: "Metrics '" + (extensionItemDefinition as MetricsDefinition)?.Key() + "' class not found",
                            description: "Could not find the defined metrics class '" + (extensionItemDefinition as MetricsDefinition)?.ItemClass.ToLower() + "'.");
                    }
                    else if (mustAddDefinition)
                    {
                        this._metricsDefinitions.Add(extensionItemDefinition as MetricsDefinition);
                        log.AddMessage(title: "Metrics '" + (extensionItemDefinition as MetricsDefinition)?.Key() + "' loaded");
                    }

                    count = this._metricsDefinitions.Count;
                    break;
                case AppExtensionItemKind.RoutineConfiguration:
                    (extensionItemDefinition as RoutineDefinition).ItemClass = this.UpdateClassFullName(
                        (extensionItemDefinition as RoutineDefinition)?.ItemClass, AppExtensionItemKind.RoutineConfiguration);
                    type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly(
                        (extensionItemDefinition as RoutineDefinition)?.ItemClass));
                    if (type == null)
                    {
                        log.AddError(
                            title: "RoutineConfiguration class '" + (extensionItemDefinition as RoutineDefinition)?.Key() + "' not found",
                            description: "Could not find the defined routine class '" + (extensionItemDefinition as RoutineDefinition)?.ItemClass.ToLower() + "'.");
                    }
                    else if (mustAddDefinition)
                    {
                        this._routineDefinitions.Add(extensionItemDefinition as RoutineDefinition);
                        log.AddMessage(title: "RoutineConfiguration class '" + (extensionItemDefinition as RoutineDefinition)?.Key() + "' loaded");
                    }

                    count = this._routineDefinitions.Count;
                    break;
                case AppExtensionItemKind.ScriptWord:
                    ScriptWordDefinition scriptWordDefinition = extensionItemDefinition as ScriptWordDefinition;

                    scriptWordDefinition.CallingClass = this.UpdateClassFullName(
                        scriptWordDefinition.CallingClass, AppExtensionItemKind.ScriptWord, defaultDefinitionClass);
                    type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly(
                        scriptWordDefinition.CallingClass));
                    if (type == null)
                    {
                        log.AddError(
                            title: "Script word '" + scriptWordDefinition.Key() + "' class not found",
                            description: "Could not find the defined script word class '" + (scriptWordDefinition.CallingClass ?? "").ToLower() + "'.");
                    }
                    else
                    {
                        Log subLog = new Log();

                        scriptWordDefinition.Name = (scriptWordDefinition.Name ?? "");
                        //if ((!scriptWordDefinition.Name.StartsWith("$")) || (!scriptWordDefinition.Name.StartsWith("%")))
                        //    scriptWordDefinition.Name = scriptWordDefinition.Name.Substring(1);
                        //switch (scriptWordDefinition.Kind)
                        //{
                        //    case ScriptItemKind.Function:
                        //        scriptWordDefinition.Name = scriptWordDefinition.Name+ "(";
                        //        break;
                        //    case ScriptItemKind.Variable:
                        //        scriptWordDefinition.Name = ScriptParsingHelper.Symbol_Var + scriptWordDefinition.Name + ")";
                        //        break;
                        //}

                        scriptWordDefinition.LibraryName = this.DefinitionName;

                        // if the current script word is a reference then
                        if (string.IsNullOrEmpty(scriptWordDefinition.ReferenceUniqueName))
                        {
                            if (string.IsNullOrEmpty(scriptWordDefinition.RuntimeFunctionName))
                                scriptWordDefinition.RuntimeFunctionName = scriptWordDefinition.GetDefaultRuntimeFunctionName();
                            //scriptWordDefinition.UniqueName = this.GetScriptWordUniqueName(
                            //    scriptWordDefinition, scriptWordDefinition.RuntimeFunctionName);

                            if (!this.AssignRuntimeFunction(assembly, scriptWordDefinition, subLog))
                            {
                                log.AddSubLog(
                                   subLog, p => p.HasErrorsOrExceptionsOrWarnings(),
                                   title: "Could not assign runtime function for script word '" + scriptWordDefinition.Key() + "'",
                                   description: "Could not assign runtime function '" +
                                       scriptWordDefinition.RuntimeFunctionName +
                                       "' for script word '" + scriptWordDefinition.Key() + "'.");
                            }
                        }
                        //else
                        //    scriptWordDefinition.ReferenceUniqueName = this.GetScriptWordUniqueName(
                        //        scriptWordDefinition, scriptWordDefinition.ReferenceUniqueName);

                        if (!log.Append(subLog, p => p.HasErrorsOrExceptionsOrWarnings()).HasErrorsOrExceptions())
                        {
                            count++;
                            if (mustAddDefinition)
                            {
                                this._scriptWordDefinitions.Add(scriptWordDefinition);
                                log.AddMessage(title: "Script word '" + scriptWordDefinition.Key() + "' loaded");
                            }

                            foreach (ScriptWordDefinition currentScriptWordDefinition in scriptWordDefinition.Children)
                                this.UpdateItemIndex(currentScriptWordDefinition, assembly, ref count, defaultDefinitionClass, log, false);
                        }
                    }
                    break;
                case AppExtensionItemKind.Task:
                    (extensionItemDefinition as TaskDefinition).ItemClass = this.UpdateClassFullName(
                        (extensionItemDefinition as TaskDefinition)?.ItemClass, AppExtensionItemKind.Task);
                    type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly((extensionItemDefinition as TaskDefinition)?.ItemClass));
                    if (type == null)
                    {
                        log.AddError(
                            title: "Task '" + (extensionItemDefinition as TaskDefinition)?.Key() + "' class not found",
                            description: "Could not find the defined task class '" + (extensionItemDefinition as TaskDefinition)?.ItemClass.ToLower() + "'.");
                    }
                    else if (mustAddDefinition)
                    {
                        this._taskDefinitions.Add(extensionItemDefinition as TaskDefinition);
                        log.AddMessage(title: "Task '" + (extensionItemDefinition as TaskDefinition)?.Key() + "' loaded");
                    }

                    count = this._taskDefinitions.Count;
                    break;
                    //case AppExtensionItemKind.Command:
                    //    (extensionItemDefinition as CommandDefinition).Class = this.UpdateClassFullName(
                    //        (extensionItemDefinition as CommandDefinition).Class, libraryItemKind);
                    //    this._CommandDefinitions.Add(extensionItemDefinition as CommandDefinition);
                    //    break;

            }

            return count;
        }


        /// <summary>
        /// Updates the specified class full name considering the assembly name of this instance and the specified extension item kind.
        /// </summary>
        /// <param name="classFullName">The class full name to update.</param>
        /// <param name="libraryItemKind">The extension item kind to consider.</param>
        /// <param name="defaultClassFullName">The default class full name to consider.</param>
        private String UpdateClassFullName(String classFullName, AppExtensionItemKind libraryItemKind, String defaultClassFullName = null)
        {
            if (this.Definition == null) return classFullName;

            if (string.IsNullOrEmpty(classFullName))
            {
                if (!string.IsNullOrEmpty(defaultClassFullName))
                    classFullName = defaultClassFullName;
                else
                    classFullName = this.Definition.GetDefaultClassNameSpace(libraryItemKind) + "." + this.GetDefaultClassName(libraryItemKind);
            }
            else if (classFullName.StartsWith("."))
            {
                classFullName = (this.Definition.GetRootNamespace() + ".").Concatenate(classFullName, ".");
            }
            else
            {
                classFullName = (this.Definition.GetDefaultClassNameSpace(libraryItemKind) + ".").Concatenate(classFullName, ".");
            }

            classFullName = (classFullName ?? "");

            if (!classFullName.Contains(","))
                classFullName += ", " + this.Definition.AssemblyName;

            return classFullName;
        }

        /// <summary>
        /// Gets the default class name of the specified item kind.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <returns>Returns the class of the specified dictionary.</returns>
        private string GetDefaultClassName(AppExtensionItemKind extensionItemKind)
        {
            switch (extensionItemKind)
            {
                case AppExtensionItemKind.ContextExtension:
                    return "ContextExtensionDefinition_" + this.GetNormalizedName();
                case AppExtensionItemKind.Handler:
                    return "HandlerDefinition_" + this.GetNormalizedName();
                case AppExtensionItemKind.ScriptWord:
                    return "ScriptDefinition_" + this.GetNormalizedName();
            }

            return "<missing class name>";
        }

        /// <summary>
        /// Assigns runtime function.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="extensionItemDefinition">The extension item definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the class of the specified dictionary.</returns>
        private bool AssignRuntimeFunction(Assembly assembly, AppExtensionItemDefinition extensionItemDefinition, ILog log = null)
        {
            bool isAssigned = false;

            MethodInfo methodInfo = null;
            try
            {
                if (assembly != null)
                {
                    //Type[][] types = new Type[0][];
                    Type type = null;

                    ScriptWordDefinition scriptWordDefinition = null;
                    HandlerDefinition dataHandlerDefinition = null;

                    if (extensionItemDefinition is ScriptWordDefinition)
                    {
                        scriptWordDefinition = extensionItemDefinition as ScriptWordDefinition;
                        String runtimeFunctionName = scriptWordDefinition.RuntimeFunctionName;
                        if (string.IsNullOrEmpty(runtimeFunctionName) && string.IsNullOrEmpty(scriptWordDefinition.CallingClass))
                        {
                            log?.AddError(
                                        title: "Class and runtime function missing");
                        }
                        else if (string.IsNullOrEmpty(runtimeFunctionName))
                        {
                            log?.AddError(
                                title: "Runtime function of class '" + scriptWordDefinition.CallingClass + "' missing");
                        }
                        else if (string.IsNullOrEmpty(scriptWordDefinition.CallingClass))
                        {
                            log?.AddError(
                                        title: "Calling class of function '" + runtimeFunctionName + "' missing");
                        }
                        else
                        {
                            type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly(scriptWordDefinition.CallingClass));
                            if (type != null)
                            {
                                Type[] types = new Type[4];
                                types[0] = typeof(AppScope);
                                types[1] = typeof(ScriptVariableSet);
                                types[2] = typeof(ScriptWord);
                                types[3] = typeof(Object).MakeArrayType();

                                methodInfo = type.GetMethod(runtimeFunctionName, types);
                                if (methodInfo == null)
                                {
                                    log?.AddError(
                                            title: "Calling function '" + runtimeFunctionName + "' not found",
                                            description: "Could not find the defined runtime function '" + runtimeFunctionName + "' in class '" + scriptWordDefinition.CallingClass + "'.");
                                }
                                else
                                {
                                    scriptWordDefinition.RuntimeFunction += methodInfo.CreateDelegate(typeof(ScriptWordFunction), scriptWordDefinition.RuntimeFunction) as ScriptWordFunction;
                                    isAssigned = scriptWordDefinition.RuntimeFunction != null;
                                }
                            }
                        }
                    }
                    else if (extensionItemDefinition is HandlerDefinition)
                    {
                        dataHandlerDefinition = extensionItemDefinition as HandlerDefinition;
                        String runtimeFunctionName_Get = dataHandlerDefinition.GetFunctionName;
                        String runtimeFunctionName_Post = dataHandlerDefinition.PostFunctionName;

                        if (string.IsNullOrEmpty(dataHandlerDefinition.CallingClass))
                        {
                            log?.AddError(title: "Calling class missing");
                        }
                        else
                        {
                            type = assembly.GetType(LibraryDefinition.GetClassNameWithoutAssembly(dataHandlerDefinition.CallingClass));
                            if (type != null)
                            {
                                Type[] types = new Type[5];
                                types[0] = typeof(DataElement);
                                types[1] = typeof(DataElementSet);
                                types[2] = typeof(AppScope);
                                types[3] = typeof(ScriptVariableSet);
                                types[4] = typeof(Log);
                                if (string.IsNullOrEmpty(runtimeFunctionName_Get))
                                {
                                    log?.AddError(title: "Runtime function of class '" + dataHandlerDefinition.CallingClass + "' missing");
                                }
                                else
                                {
                                    methodInfo = type.GetMethod(runtimeFunctionName_Get, types);
                                    if (methodInfo == null)
                                    {
                                        log?.AddError(
                                            title: "Calling function '" + runtimeFunctionName_Get + "' not found",
                                            description: "Could not find the defined runtime function '" + runtimeFunctionName_Get + "' in class '" + dataHandlerDefinition.CallingClass + "'.");
                                    }
                                    else
                                    {
                                        dataHandlerDefinition.RuntimeFunction_Get += methodInfo.CreateDelegate(typeof(HandlerGetFunction), dataHandlerDefinition.RuntimeFunction_Get) as HandlerGetFunction;
                                        isAssigned |= dataHandlerDefinition.RuntimeFunction_Get != null;
                                    }
                                }

                                types = new Type[5];
                                types[0] = typeof(Object);
                                types[1] = typeof(DataElement).MakeByRefType();
                                types[2] = typeof(AppScope);
                                types[3] = typeof(ScriptVariableSet);
                                types[4] = typeof(Log);

                                if (string.IsNullOrEmpty(runtimeFunctionName_Post))
                                {
                                    log?.AddError(
                                        title: "Runtime function of class '" + dataHandlerDefinition.CallingClass + "' missing");
                                }
                                else
                                {
                                    methodInfo = type.GetMethod(runtimeFunctionName_Post, types);
                                    //aMethodInfo = aType.GetMethod(runtimeFunctionName_Post, BindingFlags.Public | BindingFlags.Static, null, types, null);
                                    if (methodInfo == null)
                                    {
                                        log?.AddError(
                                            title: "Calling function '" + runtimeFunctionName_Post + "' not found",
                                            description: "Could not find the defined runtime function '" + runtimeFunctionName_Post + "' in class '" + dataHandlerDefinition.CallingClass + "'.");
                                    }
                                    else
                                    {
                                        //MethodInfo aGenericMethodInfo = aMethodInfo.MakeGenericMethod(new Type[] { aType });
                                        dataHandlerDefinition.RuntimeFunction_Post += methodInfo.CreateDelegate(typeof(HandlerPostFunction), dataHandlerDefinition.RuntimeFunction_Post) as HandlerPostFunction;
                                        isAssigned &= dataHandlerDefinition.RuntimeFunction_Post != null;
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                log?.AddException(exception);
            }

            return isAssigned;
        }

            #endregion

            // ------------------------------------------
            // LOADING
            // ------------------------------------------

            #region Loading

            // From index -------------------------------

            /// <summary>
            /// Loads the specified library.
            /// </summary>
            /// <param name="extensionConfiguration">The extension loading order to consider.</param>
            /// <returns></returns>
            public ILog LoadLibrary(AppExtensionConfiguration extensionConfiguration)
            {
                ILog log = new Log();
                List<Library> loadedLibraries = new List<Library>();

                if (extensionConfiguration != null)
                {
                    foreach (AppExtensionFilter extensionFilter in extensionConfiguration.GetFilters())
                    {
                        string defaultFolderPath = string.IsNullOrEmpty(extensionFilter.FolderPath) ?
                            extensionConfiguration.DefaultFolderPath : extensionFilter.FolderPath;

                        Library library = _appDomain.LoadLibrary(
                                extensionFilter.ToDefinition(),
                                log,
                                null,
                                extensionFilter.SourceKinds,
                                defaultFolderPath);

                        if (library != null && !log.HasErrorsOrExceptions()
                            && !_libraries.Any(p => p.Definition?.Id.KeyEquals(library.Definition?.Id) == true))
                        {
                            loadedLibraries.Add(library);
                        }
                    }
                }

                _libraries.AddRange(loadedLibraries);

                return log;
            }

            /// <summary>
            /// Loads the specified library.
            /// </summary>
            /// <param name="libraryIndex">The library index to consider.</param>
            /// <param name="libraryName">The name of libraries to consider.</param>
            /// <param name="libraryFolderPath">The path of the library folder to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <param name="dataSourceKinds">The kinds of data sources to consider.</param>
            /// <returns>The log of the load task.</returns>
            public ILog LoadLibrary(
                LibraryIndex libraryIndex,
                string libraryName,
                string libraryFolderPath = null,
                List<AppExtensionItemKind> extensionItemKinds = null,
                List<DataSourceKind> dataSourceKinds = null)
            {
                List<Library> loadedLibraries = new List<Library>();
                return LoadLibrary(
                    libraryIndex,
                    out loadedLibraries,
                    new List<string>() { libraryName },
                    extensionItemKinds,
                    dataSourceKinds,
                    libraryFolderPath);
            }

            /// <summary>
            /// Loads the specified library.
            /// </summary>
            /// <param name="libraryIndex">The library index to consider.</param>
            /// <param name="libraryNames">The names of libraries to consider.</param>
            /// <param name="libraryFolderPath">The path of the library folder to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <param name="dataSourceKinds">The kinds of data sources to consider.</param>
            /// <returns>The log of the load task.</returns>
            public ILog LoadLibrary(
                LibraryIndex libraryIndex,
                string[] libraryNames = null,
                string libraryFolderPath = null,
                List<AppExtensionItemKind> extensionItemKinds = null,
                List<DataSourceKind> dataSourceKinds = null)
            {
                List<Library> loadedLibraries = new List<Library>();
                return LoadLibrary(libraryIndex, out loadedLibraries,
                    libraryNames, extensionItemKinds, dataSourceKinds, libraryFolderPath);
            }

            /// <summary>
            /// Loads the specified library.
            /// </summary>
            /// <param name="libraryIndex">The library index to consider.</param>
            /// <param name="loadedLibraries">The loaded libraries to consider.</param>
            /// <param name="libraryNames">The names of libraries to consider.</param>
            /// <param name="libraryFolderPath">The path of the library folder to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <param name="dataSourceKinds">The kinds of data sources to consider.</param>
            /// <returns>The log of the load task.</returns>
            public ILog LoadLibrary(
                LibraryIndex libraryIndex,
                out List<Library> loadedLibraries,
                string[] libraryNames = null,
                List<AppExtensionItemKind> extensionItemKinds = null,
                List<DataSourceKind> dataSourceKinds = null,
                string libraryFolderPath = null)
            {
                ILog log = new Log();
                loadedLibraries = new List<Library>();

                if (libraryIndex != null)
                    foreach (LibraryDefinition currentLibraryDefinition in libraryIndex.GetDefinitions(libraryNames))
                        if (!_libraries.Any(p => p.Id.KeyEquals(currentLibraryDefinition.Id)))
                        {
                            Library library = _appDomain.LoadLibrary(
                                currentLibraryDefinition,
                                log,
                                extensionItemKinds, dataSourceKinds,
                                libraryFolderPath);

                            if (library != null && !log.HasErrorsOrExceptions())
                                if (!_libraries.Any(p => p.Definition != null && p.Definition.Id.KeyEquals(library.Definition.Id)))
                                    loadedLibraries.Add(library);
                        }

                _libraries.AddRange(loadedLibraries);

                return log;
            }

            // From file -------------------------------

            /// <summary>
            /// Loads the specifed libraries in the specified way.
            /// </summary>
            /// <param name="completeFilePath">The file path to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <param name="folderPath">The folder path to consider.</param>
            /// <returns>The log of the load task.</returns>
            /// <remarks>If null then we load the existing library names.</remarks>
            public virtual ILog LoadLibraryFromFile(
                string completeFilePath,
                List<AppExtensionItemKind> extensionItemKinds = null,
                string folderPath = "")
            {
                List<Library> loadedLibraries = new List<Library>();
                return LoadLibraryFromFile(completeFilePath, out loadedLibraries, extensionItemKinds, folderPath);
            }

            /// <summary>
            /// Loads the specifed libraries in the specified way.
            /// </summary>
            /// <param name="completeFilePath">The file path to consider.</param>
            /// <param name="loadedLibrary">The loaded library to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <param name="folderPath">The folder path to consider.</param>
            /// <returns>The log of the load task.</returns>
            /// <remarks>If null then we load the existing library names.</remarks>
            public virtual ILog LoadLibraryFromFile(
                string completeFilePath,
                out Library loadedLibrary,
                List<AppExtensionItemKind> extensionItemKinds = null,
                string folderPath = "")
            {
                List<Library> loadedLibraries = new List<Library>();
                Log log = LoadLibraryFromFile(completeFilePath, out loadedLibraries, extensionItemKinds, folderPath);

                loadedLibrary = (loadedLibraries.Count > 0 ? loadedLibraries[0] : null);

                return log;
            }

            /// <summary>
            /// Loads the specifed libraries in the specified way.
            /// </summary>
            /// <param name="completeFilePath">The file path to consider.</param>
            /// <param name="loadedLibraries">The loaded libraries to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <param name="folderPath">The folder path to consider.</param>
            /// <returns>The log of the load task.</returns>
            /// <remarks>If null then we load the existing library names.</remarks>
            public virtual ILog LoadLibraryFromFile(
                string completeFilePath,
                out List<Library> loadedLibraries,
                List<AppExtensionItemKind> extensionItemKinds = null,
                string folderPath = "")
            {
                ILog log = new Log();
                loadedLibraries = new List<Library>();

                Log subLog = null;

                folderPath = folderPath.GetEndedstring(@"\").ToPath();

                if (string.IsNullOrEmpty(completeFilePath))
                {
                    log.AddError("Assembly file path missing");
                }
                else
                {
                    foreach (string subFilePath in completeFilePath.Split('|'))
                    {
                        string completeSubFilePath = (subFilePath.Contains(@"\") ? subFilePath : folderPath + subFilePath).ToPath();

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
                            Library library = _appDomain.LoadLibrary(
                                new LibraryDefinition() { FileName = Path.GetFileName(filePath) },
                                subLog,
                                extensionItemKinds, new List<DataSourceKind>() { DataSourceKind.Repository },
                                Path.GetDirectoryName(filePath));
                            log.AddSubLog(subLog, (p => p.HasErrorsOrExceptionsOrWarnings()), title: "Loading assembly '" + filePath + "'");

                            if (library != null && !log.HasErrorsOrExceptions())
                                if (!_libraries.Any(p => p.Definition != null && p.Definition.Id.KeyEquals(library.Definition.Id)))
                                    loadedLibraries.Add(library);
                        }
                    }
                }

                _libraries.AddRange(loadedLibraries);

                return log;
            }

            // Direct -----------------------------------

            /// <summary>
            /// Loads the specifed libraries in the specified way.
            /// </summary>
            /// <param name="libraryAssemblyName">The library assembly name to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <returns>The log of the load task.</returns>
            /// <remarks>If null then we load the existing library names.</remarks>
            public virtual ILog LoadLibrary(
                string libraryAssemblyName,
                List<AppExtensionItemKind> extensionItemKinds = null)
            {
                Library loadedLibrary = null;
                return LoadLibrary(libraryAssemblyName, out loadedLibrary, extensionItemKinds);
            }

            /// <summary>
            /// Loads the specifed libraries in the specified way.
            /// </summary>
            /// <param name="libraryAssemblyName">The library assembly name to consider.</param>
            /// <param name="loadedLibrary">The loaded library to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <returns>The log of the load task.</returns>
            /// <remarks>If null then we load the existing library names.</remarks>
            public virtual ILog LoadLibrary(
                string libraryAssemblyName,
                out Library loadedLibrary,
                List<AppExtensionItemKind> extensionItemKinds = null)
            {
                List<Library> loadedLibraries = new List<Library>();
                Log log = LoadLibrary(new List<string>() { libraryAssemblyName }, out loadedLibraries, extensionItemKinds);

                loadedLibrary = (loadedLibraries.Count > 0 ? loadedLibraries[0] : null);

                return log;
            }

            /// <summary>
            /// Loads the specifed libraries in the specified way.
            /// </summary>
            /// <param name="libraryAssemblyNames">The library assembly names to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <returns>The log of the load task.</returns>
            /// <remarks>If null then we load the existing library names.</remarks>
            public virtual ILog LoadLibrary(
                List<string> libraryAssemblyNames,
                List<AppExtensionItemKind> extensionItemKinds = null)
            {
                List<Library> loadedLibraries = new List<Library>();
                return LoadLibrary(libraryAssemblyNames, out loadedLibraries, extensionItemKinds);
            }

            /// <summary>
            /// Loads the specifed libraries in the specified way.
            /// </summary>
            /// <param name="libraryAssemblyNames">The library assembly names to consider.</param>
            /// <param name="loadedLibraries">The loaded libraries to consider.</param>
            /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
            /// <returns>The log of the load task.</returns>
            /// <remarks>If null then we load the existing library names.</remarks>
            public virtual ILog LoadLibrary(
                List<string> libraryAssemblyNames,
                out List<Library> loadedLibraries,
                List<AppExtensionItemKind> extensionItemKinds = null)
            {
                ILog log = new Log();
                loadedLibraries = new List<Library>();

                Log subLog = null;

                foreach (string libraryAssemblyName in libraryAssemblyNames)
                {
                    Assembly assembly = AppDomainPool.LoadAssembly(AppDomain.CurrentDomain, libraryAssemblyName, log);

                    if (assembly == null)
                        log.AddError("Could not load assembly '" + libraryAssemblyName + "'");
                    else
                    {
                        LibraryDefinition libraryDefinition = LibraryDefinition.Load(assembly, null, log);

                        if (libraryDefinition == null)
                            log.AddError("Error while attempting to load the library definition in assembly '" + libraryAssemblyName + "'");
                        else
                        {
                            subLog = new Log();
                            Library library = _appDomain.LoadLibrary(
                                new LibraryDefinition() { AssemblyName = libraryAssemblyName },
                                subLog,
                                extensionItemKinds, new List<DataSourceKind>() { DataSourceKind.Memory });

                            log.AddSubLog(subLog, (p => p.HasErrorsOrExceptionsOrWarnings()), title: "Loading assembly '" + libraryAssemblyName + "'");

                            if (library != null && !subLog.HasErrorsOrExceptions())
                                if (!_libraries.Any(p => p.Definition != null && p.Definition.Id.KeyEquals(library.Definition.Id)))
                                    loadedLibraries.Add(library);
                        }
                    }
                }

                _libraries.AddRange(loadedLibraries);

                return log;
            }

        }
    }
