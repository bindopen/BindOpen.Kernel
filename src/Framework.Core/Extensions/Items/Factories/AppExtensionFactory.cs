using System;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Configuration.Formats;
using BindOpen.Framework.Core.Extensions.Configuration.Metrics;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Entities;
using BindOpen.Framework.Core.Extensions.Definition.Formats;
using BindOpen.Framework.Core.Extensions.Definition.Handlers;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Runtime.Factories
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class AppExtensionFactory
    {
        /// <summary>
        /// Creates the instance of the specified extension item.
        /// </summary>
        /// <param name="definitionName">The unique ID of the definition to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        public ITAppExtensionRuntimeItem<T> CreateItem<T>(
            String definitionName,
            AppExtensionItemConfiguration<T> configuration,
            String name = null,
            ILog log = null) where T : AppExtensionItemDefinition
        {
            if (this.Check(true).HasErrorsOrExceptions())
                return null;

            if (definitionName == null && configuration != null)
                definitionName = configuration.DefinitionUniqueId;

            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();
            AssemblyHelper.ClassReference assemblyReference =
                this.GetItemClassReference(extensionItemKind, definitionName, out AppExtensionItemDefinition definitionObject);
            T definition = definitionObject as T;

            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionName + "' of kind '" + extensionItemKind.ToString() + "'");
            }

            Object extensionObject = null;
            log?.AddEvents(this.AppDomain.CreateInstance(assemblyReference, out extensionObject, name, configuration, this));

            ITAppExtensionRuntimeItem<T> extensionItem = extensionObject as ITAppExtensionRuntimeItem<T>;

            if (extensionItem == null)
            {
                log?.AddError("Could not create '" + extensionItemKind.ToString() + "' object with unique name '" + definitionName + "'");
            }
            else
            {
                if (configuration == null)
                {
                    configuration = this.CreateConfiguration<T>(definitionName, null, log);
                }
                else
                {
                    configuration.DefinitionUniqueId = definitionName;
                    configuration.SetDefinition(definition);
                }

                extensionItem.SetConfiguration(configuration);
            }

            return extensionItem;
        }

        // Configurations --------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="xmlstring">The XML string to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="parameters">The object parameters to consider.</param>
        public AppExtensionItemConfiguration<T> CreateConfiguration<T>(
            string definitionUniqueId,
            string xmlstring = null,
            ILog log = null,
            params object[] parameters) where T : AppExtensionItemDefinition
        {
            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();

            T definition = GetItemDefinitionWithUniqueId<T>(definitionUniqueId);

            AppExtensionItemConfiguration<T> configuration = null;

            if (definition == null)
            {
                if (log != null)
                    log.AddError("Could not retrieve the extension item '" + definitionUniqueId + "' of kind '" + extensionItemKind.Tostring() + "'");
            }
            else if (xmlstring == null)
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Carrier:
                        configuration = new CarrierConfiguration(null, definitionUniqueId) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Task:
                        configuration = new TaskConfiguration(null, definitionUniqueId) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Connector:
                        configuration = new ConnectorConfiguration(null, definitionUniqueId) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Entity:
                        configuration = new EntityConfiguration(null, definitionUniqueId) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Format:
                        configuration = new FormatConfiguration(null, definitionUniqueId) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Metrics:
                        configuration = new MetricsConfiguration(null, definitionUniqueId) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Routine:
                        configuration = new RoutineConfiguration(null, definitionUniqueId) as AppExtensionItemConfiguration<T>;
                        break;
                }
            else
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Carrier:
                        configuration = XmlHelper.LoadFromstring<CarrierConfiguration>(xmlstring, log) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Task:
                        configuration = XmlHelper.LoadFromstring<CarrierConfiguration>(xmlstring, log) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Connector:
                        configuration = XmlHelper.LoadFromstring<ConnectorConfiguration>(xmlstring, log) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Entity:
                        configuration = XmlHelper.LoadFromstring<EntityConfiguration>(xmlstring, log) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Format:
                        configuration = XmlHelper.LoadFromstring<FormatConfiguration>(xmlstring, log) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Metrics:
                        configuration = XmlHelper.LoadFromstring<MetricsConfiguration>(xmlstring, log) as AppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Routine:
                        configuration = XmlHelper.LoadFromstring<RoutineConfiguration>(xmlstring, log) as AppExtensionItemConfiguration<T>;
                        break;
                }

            if (configuration!=null)
            {
                configuration.DefinitionUniqueId = definitionUniqueId;
                configuration.SetDefinition(definition);
            }

            return configuration;
        }
        
        // Accessors --------------------------------

        /// <summary>
        /// Gets the type of the specified library item.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <param name="uniqueName">The unique ID of the object to consider.</param>
        public Type GetItemType(
            AppExtensionItemKind extensionItemKind,
            string uniqueName)
        {
            AppExtensionItemDefinition extensionItemDefinition = null;
            AssemblyHelper.ClassReference assemblyReference =
                GetItemClassReference(extensionItemKind, uniqueName, out extensionItemDefinition);

            return GetTypeFromAssemblyReference(assemblyReference);
        }

        // ------------------------------------------
        // STATIC METHODS
        // ------------------------------------------

        #region Static Methods

        /// <summary>
        /// Gets the dictionary type of the specified extension item kind.
        /// </summary>
        /// <param name="libraryItemKind">The extension item kind to consider.</param>
        /// <returns>The Xml string of this instance.</returns>
        public static Type GetDictionaryType(AppExtensionItemKind libraryItemKind)
        {
            switch (libraryItemKind)
            {
                case AppExtensionItemKind.Carrier:
                    return typeof(CarrierIndex);
                case AppExtensionItemKind.Task:
                    return typeof(TaskIndex);
                //case AppExtensionItemKind.Command:
                //    return typeof(CommandDictionary);
                case AppExtensionItemKind.Connector:
                    return typeof(ConnectorIndex);
                case AppExtensionItemKind.Entity:
                    return typeof(EntityIndex);
                case AppExtensionItemKind.Handler:
                    return typeof(HandlerIndex);
                case AppExtensionItemKind.Metrics:
                    return typeof(MetricsIndex);
                case AppExtensionItemKind.ScriptWord:
                    return typeof(ScriptWordIndex);
                case AppExtensionItemKind.Routine:
                    return typeof(RoutineIndex);
                default:
                    return null;
            }
        }


        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates the relevant property based on the specified object attribute.
        /// </summary>
        /// <param name="dataElement">The object attribute to consider.</param>
        /// <param name="taskEntryKind">The task entry kind to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of the update task.</returns>
        private ILog UpdateProperty(
            IDataElement dataElement,
            TaskEntryKind taskEntryKind,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (dataElement != null)
            {
                try
                {
                    PropertyInfo propertyInfo = GetType().GetProperty(dataElement.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(dataElement.GetItemObject(appScope, scriptVariableSet, log), null);
                    }
                    else
                    {
                        log.AddError(
                           taskEntryKind.GetTitle() + " '" + dataElement.Key() + "' unknown in task '" +
                           (Definition != null ? Definition.Key() : "undefined") + "'");
                    }
                }
                catch
                {
                    log.AddError(
                        "Error occured while setting " + taskEntryKind.GetTitle() +
                        "'" + dataElement.Name + "' unknown in task '" +
                        (Definition != null ? Definition.Key() : "undefined") + "'");
                }
            }

            return log;
        }

        /// <summary>
        /// Updates the input properties from input detail.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of the update task.</returns>
        protected ILog UpdateInputPropertiesFromInputDetail(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            foreach (DataElement dataElement in GetEntries(TaskEntryKind.Input))
                log.AddEvents(UpdateProperty(dataElement, TaskEntryKind.Input));

            return log;
        }

        /// <summary>
        /// Updates the non-value output properties from output detail.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of the update task.</returns>
        protected ILog UpdateNonScalarOutputPropertiesFromOutputDetail(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            foreach (DataElement dataElement in GetEntries(TaskEntryKind.NonScalarOutput))
                log.AddEvents(UpdateProperty(dataElement, TaskEntryKind.NonScalarOutput));

            return log;
        }

        /// <summary>
        /// Updates output detail from the output properties.
        /// </summary>
        /// <returns>The log of the update task.</returns>
        protected ILog UpdateOutputDetailFromOutputProperties()
        {
            ILog log = new Log();

            Type aType = GetType();
            foreach (IDataElement dataElement in OutputDetail.Elements)
            {
                PropertyInfo aInputProperty = aType.GetProperty(dataElement.Name);
                if (aInputProperty != null)
                    try
                    {
                        dataElement.SetItem(aInputProperty.GetValue(this, null));
                    }
                    catch
                    {
                        log.AddError(
                            "Bad value type (sigle value/list) defined for the output property named '" + dataElement.Key() + "' of business task called '" + Key() + "'");
                    }
                else
                    log.AddError(
                        "Output property named '" + dataElement.Key() + "' of business task called '" + Key() + "' not existing");
            }

            return log;
        }

        #endregion
    }
}
