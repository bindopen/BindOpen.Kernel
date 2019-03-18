using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Configuration;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Runtime.Connectors
{

    /// <summary>
    /// This class represents a connector.
    /// </summary>
    [XmlType("Connector", Namespace = "http://www.w3.org/2001/bdo.xsd")]
    [XmlRoot(ElementName = "connector", Namespace = "http://www.w3.org/2001/bdo.xsd", IsNullable = false)]
    public abstract class Connector : ConnectorConfiguration, IConnection, ITAppExtensionRuntimeItem<ConnectorDefinition>
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
        /// Instantiates a new instance of the Connector class.
        /// </summary>
        protected Connector() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Connector class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        protected Connector(
            String name,
            String definitionName,
            ConnectorConfiguration configuration = null,
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
        public void SetConfiguration(TAppExtensionItemConfiguration<ConnectorDefinition> configuration)
        {
            if (configuration == null
                || (this.AppScope != null && configuration.KeyEquals(this.DefinitionUniqueId)))
            {
                configuration = this.AppScope.AppExtension.CreateConfiguration<ConnectorDefinition>(this.DefinitionUniqueId) as ConnectorConfiguration;
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
                    if (this.Detail == null)
                        this.Detail = new DataElementSet();

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
                new Type[] { typeof(DetailPropertyAttribute) },
                ref attribute,
                this.AppScope);

                if (attribute is DetailPropertyAttribute)
                {
                    Object value = this.Detail?.GetElementItemObject(attribute.Name, this.AppScope);
                    if (value is T t)
                        return t;
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
                    return (this.Detail.GetElementItemObject(attribute.Name, this.AppScope) as String)?.ToEnum<T>(defaultValue) ?? default(T); ;
            }

            return default(T);
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        // Open / Close -----------------------------

        /// <summary>
        /// Opens a connection.
        /// </summary>
        public virtual Log Open()
        {
            return new Log();
        }

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        public virtual Log Close()
        {
            return new Log();
        }

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        public virtual Boolean IsConnected()
        {
            return false;
        }

        #endregion

    }
}
