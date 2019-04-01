using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    [XmlType("Configuration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("configuration", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class Configuration : DataElementSet, IConfiguration
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// Current file path of this instance.
        /// </summary>
        [XmlIgnore()]
        public string CurrentFilePath
        {
            get;
            set;
        }

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [XmlElement("creationDate")]
        public string CreationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [XmlElement("lastModificationDate")]
        public string LastModificationDate
        {
            get;
            set;
        }

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        [XmlIgnore()]
        public IAppScope AppScope { get; set; } = null;

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        public Configuration()
            : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="items">The items to consider.</param>
        public Configuration(IAppScope appScope, params IDataElement[] items)
            : this(null, appScope, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="items">The items to consider.</param>
        public Configuration(string filePath, IAppScope appScope, params IDataElement[] items)
            : base(items)
        {
            CurrentFilePath = filePath;
            AppScope = appScope;
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyName">The calling property name to consider.</param>
        public void Set(object value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
            {
                DataElementAttribute attribute = null;
                PropertyInfo propertyInfo = GetPropertyInfo(
                    GetType(),
                    propertyName,
                    new Type[] { typeof(DetailPropertyAttribute) },
                    ref attribute,
                    AppScope);

                if (attribute is DetailPropertyAttribute)
                {
                    AddElement(attribute.Name, value, propertyInfo.PropertyType.GetValueType());
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
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">The calling property name to consider.</param>
        public T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName!=null)
            {
                IDataElement element = GetItem(propertyName);
                if (element!=null)
                {
                    return (T)GetElementItemObject(propertyName, AppScope);
                }
                else
                {
                    DataElementAttribute attribute = null;
                    PropertyInfo propertyInfo = GetPropertyInfo(
                        GetType(),
                        propertyName,
                        new Type[] { typeof(DetailPropertyAttribute) },
                        ref attribute,
                        AppScope);

                    if (attribute is DetailPropertyAttribute)
                    {
                        Object value = GetElementItemObject(attribute.Name, AppScope);
                        if (value is T t)
                            return t;
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">The calling property name to consider.</param>
        /// <param name="defaultValue">The default value to consider.</param>
        public T Get<T>(T defaultValue, [CallerMemberName] string propertyName = null) where T : struct, IConvertible
        {
            if (propertyName != null)
            {
                IDataElement element = GetItem(propertyName);
                if (element != null)
                {
                    return (T)GetElementItemObject(propertyName, AppScope);
                }
                else
                {
                    DataElementAttribute attribute = null;
                    PropertyInfo propertyInfo = GetPropertyInfo(
                        GetType(),
                        propertyName,
                        new Type[] { typeof(DetailPropertyAttribute) },
                        ref attribute,
                        AppScope);

                    if (attribute is DetailPropertyAttribute)
                        return (GetElementItemObject(attribute.Name, AppScope) as string)?.ToEnum<T>(defaultValue) ?? default(T); ;
                }
            }

            return default;
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
        public override void UpdateStorageInfo(ILog log = null)
        {
            base.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);
        }

        #endregion

        // -------------------------------------------------------------
        // LOADING / SAVING
        // -------------------------------------------------------------

        #region Loading_Saving

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="log">The output log.</param>
        /// <returns>True if this instance has been</returns>
        public override bool SaveXml(string filePath, ILog log = null)
        {
            if (base.SaveXml(filePath, log))
            {
                CurrentFilePath = filePath;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Instantiates a new instance of Configuration class from a xml file.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The Xml operation project defined in the Xml file.</returns>
        public new static T Load<T>(
            string filePath,
            Log log,
            IAppScope appScope = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true) where T : Configuration, new()
        {
            T configuration = DataItem.Load<T>(filePath, log, appScope, xmlSchemaSet, mustFileExist) as T;
            if (configuration != null)
            {
                configuration.CurrentFilePath = filePath;
                configuration.AppScope = appScope;
            }

            return configuration;
        }

        #endregion
    }
}
