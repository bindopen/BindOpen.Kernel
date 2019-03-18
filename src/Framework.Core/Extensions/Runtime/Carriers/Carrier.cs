using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Configuration;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Runtime.Carriers
{
    /// <summary>
    /// This class represents a carrier.
    /// </summary>
    [Serializable()]
    public abstract class Carrier : CarrierConfiguration, ITAppExtensionRuntimeItem<CarrierDefinition>
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        private String _relativePath = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        [XmlIgnore()]
        public IAppScope AppScope { get; set; } = null;

        /// <summary>
        /// Relative path of this instance.
        /// </summary>
        public String RelativePath
        {
            get
            {
                return this._relativePath;
            }
            set
            {
                this.SetPath(null, value);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Carrier class.
        /// </summary>
        public Carrier() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Carrier class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="relativePath">The relative path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public Carrier(
            String name,
            String definitionName,
            CarrierConfiguration configuration = null,
            String namePreffix = "carrier_",
            String relativePath = null,
            AppScope appScope = null)
            : base(name, definitionName, null, namePreffix)
        {
            this.AppScope = appScope;
            this.SetConfiguration(configuration);
            if (relativePath != null)
                this.SetPath(null, relativePath);
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
        public void SetConfiguration(TAppExtensionItemConfiguration<CarrierDefinition> configuration)
        {
            if (configuration?.KeyEquals(this.DefinitionUniqueId) != false)
            {
                configuration = this.AppScope?.AppExtension?.CreateConfiguration<CarrierDefinition>(this.DefinitionUniqueId) as CarrierConfiguration;
            }

            if (configuration != null)
                this.Update(configuration);
        }

        /// <summary>
        /// Sets the path of this instance.
        /// </summary>
        /// <param name="path">The new path to consider. Null to update the existing one.</param>
        /// <param name="relativePath">The new relative path to consider. Null to keep the existing one.</param>
        /// <returns>Returns True if this instance exists. False otherwise.</returns>
        public virtual void SetPath(String path = null, String relativePath = null)
        {
            String absolutePath = (path ?? this.Path);

            if (!String.IsNullOrEmpty(relativePath))
                this._relativePath = relativePath;

            if ((!String.IsNullOrEmpty(this._relativePath)) && (!String.IsNullOrEmpty(absolutePath)))
            {
                String aRelativeFolder = this._relativePath.ToLower();
                absolutePath = absolutePath.ToLower();

                if (absolutePath.StartsWith(aRelativeFolder))
                    absolutePath = absolutePath.Substring(aRelativeFolder.Length);
            }

            this.Path = absolutePath;
        }

        /// <summary>
        /// Sets the specified value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyName">The calling property name to consider.</param>
        public void Set(Object value, [CallerMemberName] String propertyName = null)
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

                    (this.Detail ?? (this.Detail = new DataElementSet())).AddElement(attribute.Name, value, propertyInfo.PropertyType.GetValueType());
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
                    Object value = this.Detail?.GetElementItemObject(attribute.Name, this.AppScope);
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
                    return (this.Detail.GetElementItemObject(attribute.Name, this.AppScope) as String).ToEnum<T>(defaultValue); ;
            }

            return default(T);
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
        public override void UpdateStorageInfo(Log log = null)
        {
            //this.Detail = DataElementSet.Create<DetailPropertyAttribute>(this);

            base.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, Log log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);

            //this.UpdateFromElementSet<PathPropertyAttribute>(this.Detail);
        }

        #endregion
    }
}
