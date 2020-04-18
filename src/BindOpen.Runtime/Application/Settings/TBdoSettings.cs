using BindOpen.Application.Configuration;
using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

namespace BindOpen.Application.Settings
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    public class TBdoSettings<Q> : DataItem, ITBdoSettings<Q>
        where Q : class, IBdoBaseConfiguration, new()
    {
        // -------------------------------------------------------
        // VARIABLES
        // -------------------------------------------------------

        #region Variables

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IBdoScope _scope = null;

        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        protected Q _configuration = null;

        #endregion

        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        public IBdoScope BdoScope => _scope;

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public Q Configuration => _configuration;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoSettings class.
        /// </summary>
        public TBdoSettings()
        {
            _configuration = new Q();
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoSettings class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public TBdoSettings(IBdoScope scope, Q configuration)
        {
            _scope = scope;
            _configuration = configuration;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public string Key()
        {
            return Configuration?.Name;
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name to consider.</param>
        public T Get<T>(string name)
        {
            return Configuration.GetElementObject<T>(name, _scope);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public object Get(string name)
        {
            return Configuration.GetElementObject(name, _scope);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">The calling property name to consider.</param>
        public T GetProperty<T>([CallerMemberName] string propertyName = null)
        {
            if (Configuration == null) return default;

            if (propertyName != null)
            {
                IDataElement element = Configuration.GetItem(propertyName);
                if (element != null)
                {
                    return (T)Configuration.GetElementObject(propertyName, _scope);
                }
                else
                {
                    PropertyInfo propertyInfo = GetType().GetPropertyInfo(
                        propertyName,
                        new Type[] { typeof(DetailPropertyAttribute) },
                        out DataElementAttribute attribute);

                    if (attribute is DetailPropertyAttribute)
                    {
                        object value = Configuration.GetElementObject(attribute.Name, _scope);
                        if (value is T t)
                            return t;
                    }
                }
            }

            return default;
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue">The default value to consider.</param>
        /// <param name="propertyName">The calling property name to consider.</param>
        public T GetProperty<T>(T defaultValue, [CallerMemberName] string propertyName = null) where T : struct, IConvertible
        {
            if (Configuration == null) return default;

            if (propertyName != null)
            {
                IDataElement element = Configuration.GetItem(propertyName);
                if (element != null)
                {
                    return (T)Configuration.GetElementObject(propertyName, _scope);
                }
                else
                {
                    PropertyInfo propertyInfo = GetType().GetPropertyInfo(
                        propertyName,
                        new Type[] { typeof(DetailPropertyAttribute) },
                        out DataElementAttribute attribute);

                    if (attribute is DetailPropertyAttribute)
                        return (Configuration.GetElementObject(attribute.Name, _scope) as string)?.ToEnum<T>(defaultValue) ?? default;
                }
            }

            return default;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified value.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to set.</param>
        public void Set(string name, object value)
        {
            Configuration?.AddElementItem(name, value);
        }

        /// <summary>
        /// Sets the specified value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyName">The calling property name to consider.</param>
        public void SetProperty(object value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
            {
                PropertyInfo propertyInfo = GetType().GetPropertyInfo(
                    propertyName,
                    new Type[] { typeof(DetailPropertyAttribute) },
                    out DataElementAttribute attribute);

                if (attribute is DetailPropertyAttribute)
                {
                    Configuration.AddElement(ElementFactory.CreateScalar(attribute.Name, propertyInfo.PropertyType.GetValueType(), value));
                }
            }
        }

        /// <summary>
        /// Loads the application settings of this instance.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="specificationLevels">The specification levels to consider.</param>
        /// <param name="specificationSet">The specification set to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <returns>Returns the loading log.</returns>
        public virtual IBdoLog UpdateFromFile(
            string filePath,
            SpecificationLevels[] specificationLevels = null,
            IDataElementSpecSet specificationSet = null,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            XmlSchemaSet xmlSchemaSet = null)
        {
            var log = new BdoLog();

            Q configuration = ConfigurationFactory.Load<Q>(filePath, scope, scriptVariableSet, log, xmlSchemaSet, false, true);

            if (!log.HasErrorsOrExceptions() && configuration != null)
            {
                _scope = scope;
                Configuration?.Update(configuration);
                Configuration?.Update(
                    ElementSpecFactory.CreateSet(
                        specificationSet?.Items?
                            .Where(p => p.SpecificationLevels?.ToArray().Has(specificationLevels) == true).ToArray()),
                    null, new[] { UpdateModes.Incremental_UpdateCommonItems });

                UpdateRuntimeInfo(_scope, null, log);
            }

            return log;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            Configuration?.UpdateFromObject<DetailPropertyAttribute>(this);
            Configuration?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            if (Configuration != null)
            {
                Configuration.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                this.UpdateFromElementSet<DetailPropertyAttribute>(Configuration, scope, scriptVariableSet);
            }
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _scope?.Dispose();

            _isDisposed = true;

            if (isDisposing)
            {
                GC.SuppressFinalize(this);
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
