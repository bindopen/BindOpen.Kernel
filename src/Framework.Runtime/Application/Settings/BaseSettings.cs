using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    public abstract class BaseSettings : DataItem, IBaseSettings
    {
        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IAppScope _appScope = null;

        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        protected IBaseConfiguration _configuration = null;

        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        public IAppScope AppScope => _appScope;

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IBaseConfiguration Configuration => _configuration;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BaseSettings class.
        /// </summary>
        public BaseSettings()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BaseSettings class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public BaseSettings(IAppScope appScope, IBaseConfiguration configuration)
        {
            _appScope = appScope;
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
        /// <typeparam name="Q"></typeparam>
        /// <param name="name">The name to consider.</param>
        public Q Get<Q>(string name = null) where Q : class
        {
            return Configuration?.GetElementObject<Q>(name, _appScope);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="propertyName">The calling property name to consider.</param>
        public Q GetProperty<Q>([CallerMemberName] string propertyName = null)
        {
            if (Configuration == null) return default;

            if (propertyName != null)
            {
                IDataElement element = Configuration.GetItem(propertyName);
                if (element != null)
                {
                    return (Q)Configuration.GetElementObject(propertyName, _appScope);
                }
                else
                {
                    PropertyInfo propertyInfo = GetType().GetPropertyInfo(
                        propertyName,
                        new Type[] { typeof(DetailPropertyAttribute) },
                        out DataElementAttribute attribute);

                    if (attribute is DetailPropertyAttribute)
                    {
                        object value = Configuration.GetElementObject(attribute.Name, _appScope);
                        if (value is Q q)
                            return q;
                    }
                }
            }

            return default;
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="defaultValue">The default value to consider.</param>
        /// <param name="propertyName">The calling property name to consider.</param>
        public Q GetProperty<Q>(Q defaultValue, [CallerMemberName] string propertyName = null) where Q : struct, IConvertible
        {
            if (Configuration == null) return default;

            if (propertyName != null)
            {
                IDataElement element = Configuration.GetItem(propertyName);
                if (element != null)
                {
                    return (Q)Configuration.GetElementObject(propertyName, _appScope);
                }
                else
                {
                    PropertyInfo propertyInfo = GetType().GetPropertyInfo(
                        propertyName,
                        new Type[] { typeof(DetailPropertyAttribute) },
                        out DataElementAttribute attribute);

                    if (attribute is DetailPropertyAttribute)
                        return (Configuration.GetElementObject(attribute.Name, _appScope) as string)?.ToEnum<Q>(defaultValue) ?? default;
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
        /// <param name="value">The value to set.</param>
        /// <param name="name">The name to consider.</param>
        public void Set(object value, string name = null)
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

        #endregion

        /// <summary>
        /// Loads the application settings of this instance.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <returns>Returns the loading log.</returns>
        public virtual ILog Load(
            string filePath,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            XmlSchemaSet xmlSchemaSet = null)
        => new Log();
    }
}
