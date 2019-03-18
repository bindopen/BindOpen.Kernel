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
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Definition.Entities;

namespace BindOpen.Framework.Core.Extensions.Runtime.Entities
{

    /// <summary>
    /// This class represents an data entity item.
    /// </summary>
    [Serializable()]
    public abstract class Entity : EntityConfiguration, ITAppExtensionRuntimeItem<EntityDefinition>
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

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Entity class.
        /// </summary>
        public Entity() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Entity class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public Entity(
            String name,
            String definitionName,
            EntityConfiguration configuration = null,
            String namePreffix = "entity_",
            AppScope appScope = null)
            : base(name, definitionName, namePreffix)
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
        public void SetConfiguration(TAppExtensionItemConfiguration<EntityDefinition> configuration)
        {
            if (configuration == null
                || (AppScope != null && configuration.KeyEquals(this.DefinitionUniqueId)))
            {
                configuration = this.AppScope.AppExtension.CreateConfiguration<EntityDefinition>(this.DefinitionUniqueId) as EntityConfiguration;
            }

            if (configuration != null)
                this.Update(configuration);
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
    }
}