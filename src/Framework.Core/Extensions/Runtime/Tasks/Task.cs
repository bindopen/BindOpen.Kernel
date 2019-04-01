using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Configuration;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Tasks
{
    /// <summary>
    /// This class represents an task.
    /// </summary>
    [Serializable()]
    [XmlType("Task", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "task", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    [XmlInclude(typeof(Command))]
    public abstract class Task : TaskConfiguration, ITAppExtensionRuntimeItem<TaskDefinition>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        [XmlIgnore()]
        public IAppScope AppScope { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Task class.
        /// </summary>
        public Task() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Task class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public Task(
            String name,
            String definitionName,
            TaskConfiguration configuration = null,
            String namePreffix = "task_",
            IAppScope appScope = null)
            : base(name, definitionName, null, namePreffix)
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
        public virtual void SetConfiguration(TAppExtensionItemConfiguration<TaskDefinition> configuration)
        {
            if (this.AppScope != null && configuration?.KeyEquals(this.DefinitionUniqueId) == true)
            {
                configuration = this.AppScope.AppExtension.CreateConfiguration<TaskDefinition>(this.DefinitionUniqueId) as TaskConfiguration;
            }

            if (configuration != null)
            {
                this.Update(configuration);
            }
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
                new Type[] { typeof(TaskInputAttribute), typeof(TaskOutputAttribute), typeof(DetailPropertyAttribute) },
                ref attribute,
                this.AppScope);

                if (attribute is TaskInputAttribute)
                {
                    if (this.InputDetail == null) this.InputDetail = new DataElementSet();

                    this.InputDetail.AddElement(attribute.Name, value, propertyInfo.PropertyType.GetValueType());
                }
                else if (attribute is TaskOutputAttribute)
                {
                    if (this.OutputDetail == null) this.OutputDetail = new DataElementSet();

                    this.OutputDetail.AddElement(attribute.Name, value, propertyInfo.PropertyType.GetValueType());
                }
                else if (attribute is DetailPropertyAttribute)
                {
                    if (this.Detail == null) this.Detail = new DataElementSet();

                    this.Detail.AddElement(attribute.Name, value, propertyInfo.PropertyType.GetValueType());
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
                    new Type[] { typeof(TaskInputAttribute), typeof(TaskOutputAttribute), typeof(DetailPropertyAttribute) },
                    ref attribute,
                    this.AppScope);

                Object value = null;
                if (attribute is TaskInputAttribute)
                    value = this.InputDetail?.GetElementItemObject(attribute.Name, this.AppScope);
                else if (attribute is TaskOutputAttribute)
                    value = this.OutputDetail?.GetElementItemObject(attribute.Name, this.AppScope);
                else if (attribute is DetailPropertyAttribute)
                    value = this.Detail?.GetElementItemObject(attribute.Name, this.AppScope);
                if (value is T)
                    return (T)value;
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
                    new Type[] { typeof(TaskInputAttribute), typeof(TaskOutputAttribute), typeof(DetailPropertyAttribute) },
                    ref attribute,
                    this.AppScope);

                if (attribute is TaskInputAttribute)
                    return (this.InputDetail.GetElementItemObject(attribute.Name, this.AppScope) as string).ToEnum<T>(defaultValue);
                else if (attribute is TaskOutputAttribute)
                    return (this.OutputDetail.GetElementItemObject(attribute.Name, this.AppScope) as string).ToEnum<T>(defaultValue);
                else if (attribute is DetailPropertyAttribute)
                    return (this.Detail.GetElementItemObject(attribute.Name, this.AppScope) as string).ToEnum<T>(defaultValue);
            }

            return default(T);
        }

        #endregion

        //------------------------------------------
        // EXECUTION
        //-----------------------------------------

        #region Execution

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use for execution.</param>
        /// <param name="runtimeMode">The runtime mode to consider.</param>
        /// <returns>Returns the output value of the execution.</returns>
        public abstract void Execute(
            Log log,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal);

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use for execution.</param>
        /// <param name="runtimeMode">The runtime mode to consider.</param>
        /// <returns>Returns the output value of the execution.</returns>
        public ILog Execute(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal)
        {
            ILog log = new Log();
            this.Execute(log, appScope, scriptVariableSet, runtimeMode);

            return log;
        }

        #endregion
    }

}