using BindOpen.MetaData;
using BindOpen.MetaData.Configuration;
using BindOpen.MetaData.Elements;
using BindOpen.MetaData.Items;
using BindOpen.Runtime.Scopes;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BindOpen.Runtime.Settings
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    public class BdoSettings : BdoItem, IBdoSettings
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoSettings class.
        /// </summary>
        public BdoSettings()
        {
            Configuration = BdoMeta.NewConfig();
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoSettings class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public BdoSettings(
            IBdoScope scope,
            IBdoConfiguration configuration)
        {
            Scope = scope;
            Configuration = configuration;
        }

        #endregion

        // -------------------------------------------------------
        // IBdoSettings Implementation
        // -------------------------------------------------------

        #region IBdoSettings

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; set; }

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IBdoConfiguration Configuration { get; set; }

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
            return Configuration.GetItem<T>(name, Scope);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public object Get(string name)
        {
            return Configuration.GetItem(name, Scope);
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
                IBdoMetaElement element = Configuration.Get(propertyName);
                if (element != null)
                {
                    return (T)Configuration.GetItem(propertyName, Scope);
                }
                else
                {
                    _ = GetType().GetPropertyInfo(
                        propertyName,
                        new Type[] { typeof(BdoMetaAttribute) },
                        out BdoMetaAttribute attribute);

                    if (attribute is BdoMetaAttribute)
                    {
                        object value = Configuration.GetItem(attribute.Name, Scope);
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
                IBdoMetaElement element = Configuration.Get(propertyName);
                if (element != null)
                {
                    return (T)Configuration.GetItem(propertyName, Scope);
                }
                else
                {
                    _ = GetType().GetPropertyInfo(
                        propertyName,
                        new Type[] { typeof(BdoMetaAttribute) },
                        out BdoMetaAttribute attribute);

                    if (attribute is BdoMetaAttribute)
                        return (Configuration.GetItem(attribute.Name, Scope) as string)?.ToEnum<T>(defaultValue) ?? default;
                }
            }

            return default;
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
                    new Type[] { typeof(BdoMetaAttribute) },
                    out BdoMetaAttribute attribute);

                if (attribute is BdoMetaAttribute)
                {
                    Configuration.Add(
                        BdoMeta.NewScalar(
                            attribute.Name,
                            propertyInfo.PropertyType.GetValueType(),
                            value));
                }
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

            Scope?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        public IBdoScoped WithScope(IBdoScope scope)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
