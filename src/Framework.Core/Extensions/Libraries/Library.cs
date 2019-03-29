using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
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
using BindOpen.Framework.Core.Extensions.Indexes;
using BindOpen.Framework.Core.Extensions.Runtime.Handlers;
using BindOpen.Framework.Core.Extensions.Runtime.Scriptwords;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    /// <summary>
    /// This class represents an library.
    /// </summary>
    [Serializable()]
    [XmlType("Library", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public class Library : NamedDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>        
        protected DataSourceKind _sourceKind = DataSourceKind.Any;

        // Definitions ----------------------------------

        /// <summary>
        /// The carrier definitions of this instance.
        /// </summary>
        protected List<CarrierDefinition> _carrierDefinitions = new List<CarrierDefinition>();

        /// <summary>
        /// The task definitions of this instance.
        /// </summary>
        protected List<TaskDefinition> _taskDefinitions = new List<TaskDefinition>();

        ///// <summary>
        ///// The command definitions of this instance.
        ///// </summary>
        //protected List<CommandDefinition> _CommandDefinitions = new List<CommandDefinition>();

        /// <summary>
        /// The connector definitions of this instance.
        /// </summary>
        protected List<ConnectorDefinition> _connectorDefinitions = new List<ConnectorDefinition>();

        ///// <summary>
        ///// The context extension definitions of this instance.
        ///// </summary>
        //protected List<DataContextExtensionDefinition> _ContextExtensionDefinitions = new List<DataContextExtensionDefinition>();

        /// <summary>
        /// The class definitions of this instance.
        /// </summary>
        protected List<EntityDefinition> _entityDefinitions = new List<EntityDefinition>();

        /// <summary>
        /// The reference definitions of this instance.
        /// </summary>
        protected List<HandlerDefinition> _handlerDefinitions = new List<HandlerDefinition>();

        /// <summary>
        /// The routine definitions of this instance.
        /// </summary>
        protected List<RoutineDefinition> _routineDefinitions = new List<RoutineDefinition>();

        /// <summary>
        /// The metrics definitions of this instance.
        /// </summary>
        protected List<MetricsDefinition> _metricsDefinitions = new List<MetricsDefinition>();

        /// <summary>
        /// The script word definitions of this instance.
        /// </summary>
        protected List<ScriptWordDefinition> _scriptWordDefinitions = new List<ScriptWordDefinition>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Definition -------------------------------

        /// <summary>
        /// Definition unique name of this instance.
        /// </summary>
        [XmlElement("definition")]
        public String DefinitionName
        {
            get { return Definition?.Name; }
        }

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        [XmlIgnore()]
        public LibraryDefinition Definition { get; set; } = null;

        // Assemblies -------------------------------

        /// <summary>
        /// The assembly of this instance.
        /// </summary>
        [XmlIgnore()]
        public Assembly Assembly { get; set; } = null;

        /// <summary>
        /// The ExtensionDataContext kind of this instance.
        /// </summary>
        [XmlElement("dataSourceKind")]
        public DataSourceKind SourceKind
        {
            get { return this._sourceKind; }
            set { this._sourceKind = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of Library class.
        /// </summary>
        public Library()
        {
        }

        /// <summary>
        /// Instantiates a new instance of Library class from a library instance.
        /// </summary>
        /// <param name="definition">Business library definition to consider.</param>
        public Library(LibraryDefinition definition)
        {
            this.Definition = definition;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this._taskDefinitions = new List<TaskDefinition>();
            this._carrierDefinitions = new List<CarrierDefinition>();
            //this._CommandDefinitions = new List<CommandDefinition>();
            this._connectorDefinitions = new List<ConnectorDefinition>();
            //this._ContextExtensionDefinitions = new List<DataContextExtensionDefinition>();
            this._entityDefinitions = new List<EntityDefinition>();
            this._handlerDefinitions = new List<HandlerDefinition>();
            this._routineDefinitions = new List<RoutineDefinition>();
            this._metricsDefinitions = new List<MetricsDefinition>();
            this._scriptWordDefinitions = new List<ScriptWordDefinition>();
        }

        /// <summary>
        /// Loads the specified library item index.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="libraryItemKind">The extension item kind to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the number of items.</returns>
        public int LoadItemIndex(Assembly assembly, AppExtensionItemKind libraryItemKind, Log log = null)
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
                                CarrierConfigurationIndex extensionItemIndex_Carrier = xmlSerializer.Deserialize(stream) as CarrierConfigurationIndex;
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
                                DataHandlerIndex extensionItemIndex_Handler = xmlSerializer.Deserialize(stream) as DataHandlerIndex;
                                itemIndexLibraryId = extensionItemIndex_Handler.LibraryId;
                                itemIndexLibraryName = extensionItemIndex_Handler.LibraryName;
                                //definitionClassFullName = extensionItemIndex_Reference.DefinitionClass;
                                itemDefinitions = extensionItemIndex_Handler.Definitions.Select(p => p as AppExtensionItemDefinition).ToList();
                                break;
                            case AppExtensionItemKind.RoutineConfiguration:
                                RoutineConfigurationIndex extensionItemIndex_RoutineConfiguration = xmlSerializer.Deserialize(stream) as RoutineConfigurationIndex;
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
            Log log = null,
            Boolean mustAddDefinition = true)
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
        private Boolean AssignRuntimeFunction(Assembly assembly, AppExtensionItemDefinition extensionItemDefinition, Log log = null)
        {
            Boolean isAssigned = false;

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
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the normalized name of this instance.
        /// </summary>
        /// <returns>Returns the normalized name of this instance.</returns>
        /// <remarks>The normalized name is the name in which only the alphanumeric characters and _ are allowed.</remarks>
        public String GetNormalizedName()
        {
            return this.DefinitionName.GetNormalizedName();
        }

        // Item definitions ----------------------------------

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <returns>The item words of specified library names.</returns>
        public List<T> GetItemDefinitions<T>() where T : AppExtensionItemDefinition
        {
            List<T> itemDefinitions = new List<T>();

            switch (typeof(T).GetExtensionItemKind())
            {
                case AppExtensionItemKind.Carrier:
                    return this._carrierDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Task:
                    return this._taskDefinitions.ToList() as List<T>;
                //case AppExtensionItemKind.Command:
                //    return this._CommandDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Connector:
                    return this._connectorDefinitions.ToList() as List<T>;
                //case AppExtensionItemKind.ContextExtension:
                //    return this._ContextExtensionDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Entity:
                    return this._entityDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Format:
                    return this._entityDefinitions.SelectMany(p => p.FormatDefinitions).Distinct().ToList() as List<T>;
                case AppExtensionItemKind.Metrics:
                    return this._metricsDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Handler:
                    return this._handlerDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.RoutineConfiguration:
                    return this._routineDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.ScriptWord:
                    return this._scriptWordDefinitions.ToList() as List<T>;
            }

            return itemDefinitions;
        }

        /// <summary>
        /// Returns the item definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">The unique name of item to return.</param>
        /// <returns>The item with the specified unique name.</returns>
        public T GetItemDefinitionWithUniqueId<T>(String uniqueName) where T : AppExtensionItemDefinition
        {
            T aItemDefinition = null;

            if (uniqueName != null)
            {
                switch (typeof(T).GetExtensionItemKind())
                {
                    case AppExtensionItemKind.Carrier:
                        return this._carrierDefinitions.Find(p => p.KeyEquals(uniqueName)) as T;
                    case AppExtensionItemKind.Task:
                        return this._taskDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName)) as T;
                    //case AppExtensionItemKind.Command:
                    //    return this._CommandDefinitions.ToList() as List<T>;
                    case AppExtensionItemKind.Connector:
                        return this._connectorDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName)) as T;
                    //case AppExtensionItemKind.ContextExtension:
                    //    return this._ContextExtensionDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName)) as T;
                    case AppExtensionItemKind.Entity:
                        return this._entityDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName)) as T;
                    case AppExtensionItemKind.Format:
                        return this._entityDefinitions.SelectMany(p => p.FormatDefinitions).FirstOrDefault(p => p.KeyEquals(uniqueName)) as T;
                    case AppExtensionItemKind.Metrics:
                        return this._metricsDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName)) as T;
                    case AppExtensionItemKind.Handler:
                        return this._handlerDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName)) as T;
                    case AppExtensionItemKind.RoutineConfiguration:
                        return this._routineDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName)) as T;
                    case AppExtensionItemKind.ScriptWord:
                        return this.GetScriptWordDefinitionWithUniqueName(uniqueName) as T;
                    default:
                        break;
                }
            }

            return aItemDefinition;
        }

        // Script words ------------------------------------------------

        /// <summary>
        /// Returns the script word definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">The unique name of script word to return.</param>
        /// <param name="parentDefinition">The parent definition to return.</param>
        /// <returns>The script word with the specified unique name.</returns>
        public ScriptWordDefinition GetScriptWordDefinitionWithUniqueName(
            String uniqueName, ScriptWordDefinition parentDefinition = null)
        {
            if (uniqueName == null)
                return null;

            List<ScriptWordDefinition> definitions =
                (parentDefinition == null ? this._scriptWordDefinitions : parentDefinition.Children);

            ScriptWordDefinition scriptWordDefinition = null;
            foreach (ScriptWordDefinition currentDefinition in definitions)
            {
                if (currentDefinition.KeyEquals(uniqueName))
                {
                    return currentDefinition;
                }
                else
                   if ((scriptWordDefinition = this.GetScriptWordDefinitionWithUniqueName(uniqueName, currentDefinition)) != null)
                {
                    break;
                }
            }

            return scriptWordDefinition;
        }

        #endregion

        // ------------------------------------------
        // REBUILDING
        // ------------------------------------------

        #region Rebuilding

        /// <summary>
        /// Inializes the specified library.
        /// </summary>
          /// <param name="appExtension">The application extension to consider.</param>
        public void Initialize(AppExtension appExtension)
        {
            if (appExtension == null) return;

            this.InitializeScriptTree(appExtension);
        }

        private void InitializeScriptTree(AppExtension appExtension, ScriptWordDefinition parentScriptWordDefinition = null)
        {
            if (appExtension == null) return;

            List<ScriptWordDefinition> scriptWordDefinitions = new List<ScriptWordDefinition>();

            if (parentScriptWordDefinition == null)
                scriptWordDefinitions = this._scriptWordDefinitions;
            else
                scriptWordDefinitions = parentScriptWordDefinition.Children;

            // we recursively retrieve the sub script words
            int i = 0;
            while (i < scriptWordDefinitions.Count)
            {
                ScriptWordDefinition currentScriptWordDefinition = scriptWordDefinitions[i];

                // if the current script word is a reference then
                if (!string.IsNullOrEmpty(currentScriptWordDefinition.ReferenceUniqueName))
                {
                    // we retrieve the reference script word
                    ScriptWordDefinition referenceScriptWordDefinition = this.GetScriptWordDefinitionWithUniqueName(currentScriptWordDefinition.ReferenceUniqueName);
                    if (referenceScriptWordDefinition == null)
                    {
                        referenceScriptWordDefinition = appExtension.GetItemDefinitionWithUniqueId<ScriptWordDefinition>(
                           currentScriptWordDefinition.ReferenceUniqueName,
                           appExtension.GetLibraryNames().Excluding(this.Name));
                    }

                    if (referenceScriptWordDefinition != null)
                        scriptWordDefinitions[i] = referenceScriptWordDefinition;
                }
                else
                {
                    this.InitializeScriptTree(appExtension, currentScriptWordDefinition);
                }

                i++;
            }
        }

        //private String GetScriptWordUniqueName(ScriptWordDefinition scriptWordDefinition, String uniqueName)
        //{
        //    if (scriptWordDefinition == null) return "";

        //    switch (scriptWordDefinition.Kind)
        //    {
        //        case ScriptItemKind.Function:
        //            uniqueName = (uniqueName ?? "").GetStartedString("Fun_");
        //            break;
        //        case ScriptItemKind.Variable:
        //            uniqueName = (uniqueName ?? "").GetStartedString("Var_");
        //            break;
        //    }
        //    return (uniqueName.Contains("$") ? uniqueName : scriptWordDefinition.LibraryName + "$" + uniqueName);
        //}

        #endregion
    }
}
