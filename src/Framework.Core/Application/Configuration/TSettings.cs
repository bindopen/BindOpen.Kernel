using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Attributes;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    public class TSettings<T> : DataItem, ITSettings<T> where T : IConfiguration, new()
    {
        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IAppScope _appScope = null;

        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        public IAppScope AppScope
        {
            get => _appScope;
        }

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public T Configuration { get; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        public TSettings()
        {
            Configuration = new T();
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public TSettings(IAppScope appScope, T configuration)
        {
            _appScope = appScope;
            Configuration = configuration;
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
    }
}
