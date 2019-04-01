using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Configuration;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Routines
{
    /// <summary>
    /// This class represents a routine.
    /// </summary>
    [Serializable()]
    [XmlType("Routine", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("routine", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public abstract class Routine : RoutineConfiguration, ITAppExtensionRuntimeItem<RoutineDefinition>
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        [XmlIgnore()]
        public IAppScope AppScope { get; set; } = null;

        #endregion

        //------------------------------------------
        // CONSTRUCTORS
        //-----------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Routine class.
        /// </summary>
        public Routine() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Routine class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public Routine(
            String name,
            String definitionName,
            RoutineConfiguration configuration = null,
            AppScope appScope = null)
            : base(name, definitionName)
        {
            this.AppScope = appScope;
            this.SetConfiguration(configuration);
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specififed configuration.
        /// </summary>
        /// <param name="configuration">The configuration to consider.</param>
        public void SetConfiguration(TAppExtensionItemConfiguration<RoutineDefinition> configuration)
        {
            if (configuration == null
                || (this.AppScope != null && configuration.KeyEquals(this.DefinitionUniqueId)))
            {
                configuration = this.AppScope.AppExtension.CreateConfiguration<RoutineDefinition>(this.DefinitionUniqueId) as RoutineConfiguration;
            }

            if (configuration != null)
                this.Update(configuration);
        }

        /// <summary>
        /// Sets the specified value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyName">The calling property name to consider.</param>
        public void Set(object value, [CallerMemberName] String propertyName = null)
        {
            if (propertyName != null)
            {
                DataElementAttribute attribute = null;
                PropertyInfo propertyInfo = this.GetPropertyInfo(
                    this.GetType(),
                    propertyName,
                new Type[] { typeof(ParameterAttribute) },
                ref attribute,
                this.AppScope);

                if (attribute is ParameterAttribute)
                {
                    if (this.ParameterDetail == null)
                        this.ParameterDetail = new DataElementSet();

                    this.ParameterDetail.AddElement(attribute.Name, value, propertyInfo.PropertyType.GetValueType());
                }
            }
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param name="propertyName">The calling property name to consider.</param>
        public T Get<T>([CallerMemberName] String propertyName = null)
        {
            if (propertyName != null)
            {
                DataElementAttribute attribute = null;
                PropertyInfo propertyInfo = this.GetPropertyInfo(
                    this.GetType(),
                    propertyName,
                new Type[] { typeof(DetailPropertyAttribute) },
                ref attribute,
                this.AppScope);

                if (attribute is DetailPropertyAttribute)
                {
                    Object value = this.ParameterDetail?.GetElementItemObject(attribute.Name, this.AppScope);
                    if (value is T)
                        return (T)value;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param name="propertyName">The calling property name to consider.</param>
        /// <param name="defaultValue">The default value to consider.</param>
        public T Get<T>(T defaultValue, [CallerMemberName] String propertyName = null) where T : struct, IConvertible
        {
            if (propertyName != null)
            {
                DataElementAttribute attribute = null;
                PropertyInfo propertyInfo = this.GetPropertyInfo(
                    this.GetType(),
                    propertyName,
                    new Type[] { typeof(DetailPropertyAttribute) },
                    ref attribute,
                    this.AppScope);

                if (attribute is DetailPropertyAttribute)
                    return (this.ParameterDetail.GetElementItemObject(attribute.Name, this.AppScope) as string).ToEnum<T>(defaultValue);
            }

            return default(T);
        }

        #endregion

        // --------------------------------------------------
        // EXECUTION
        // --------------------------------------------------

        #region Execution

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="item">The item to use.</param>
        /// <param name="dataElement">The element to use.</param>
        /// <param name="objects">The objects to use.</param>
        /// <returns>The log of check log.</returns>
        public ILog Execute(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            Object aItem = null,
            IDataElement dataElement = null,
            params object[] objects)
        {
            ILog log = new Log();

            log.AddEvents(appScope.Check(
                isAppExtensionChecked: !string.IsNullOrEmpty(this.DefinitionUniqueId),
                isScriptInterpreterChecked: this.CommandSet.Items.Any(p => p.Kind == CommandKind.Script)));
            //,
            //    isConnectionManagerChecked: (dataElement!=null)&& (dataElement.ItemReference!=null)&& (dataElement.ItemReference.GetDataSourceKind() != DataSourceKind.None )));

            if (!log.HasErrorsOrExceptions())
            {
                scriptVariableSet = (scriptVariableSet ?? new ScriptVariableSet());

                // we launch the runtime check (the one defined in code)
                log.AddEvents(this.CustomExecute(appScope, scriptVariableSet, aItem, dataElement, objects));

                // then we launch commands
                foreach (Command command in this.CommandSet.Items.Where(p => p.Kind != CommandKind.Script))
                {
                    string result = "";
                    if (!log.Append(command.ExecuteWithResult(out result)).HasErrorsOrExceptions())
                        scriptVariableSet.SetValue("command_result$" + command.Key(), result);
                }

                // then we determine the result
                foreach (ConditionalEvent currentConditionalEvent in this.OutputEventSet.Items)
                    if (appScope.ScriptInterpreter.Evaluate(
                        currentConditionalEvent.ConditionScript, scriptVariableSet, log) as Boolean? == true)
                    {
                        log.AddEvent(new LogEvent(currentConditionalEvent));
                        break;
                    }
            }

            return log;
        }

        /// <summary>
        /// Executes customly this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="item">The item to use.</param>
        /// <param name="dataElement">The element to use.</param>
        /// <param name="objects">The objects to use.</param>
        /// <returns>The log of check log.</returns>
        protected virtual ILog CustomExecute(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            Object item = null,
            IDataElement dataElement = null,
            params object[] objects)
        {
            return new Log();
        }

        #endregion
    }
}
