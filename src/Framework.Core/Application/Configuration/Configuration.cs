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
    public class Configuration : DataElementSet
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// Current file path of this instance.
        /// </summary>
        [XmlIgnore()]
        public String CurrentFilePath
        {
            get;
            set;
        }

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [XmlElement("creationDate")]
        public String CreationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [XmlElement("lastModificationDate")]
        public String LastModificationDate
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
        public Configuration(IAppScope appScope, params DataElement[] items) : this(null, appScope, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="items">The items to consider.</param>
        public Configuration(string filePath, IAppScope appScope, params DataElement[] items) : base(items)
        {
            this.CurrentFilePath = filePath;
            this.AppScope = appScope;
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified elements into the specified group.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <param name="items">The items to add.</param>
        /// <returns>Returns this instance.</returns>
        public Configuration AddGroup(string groupId, params DataElement[] items)
        {
            foreach(DataElement element in items)
            {
                if (element.Specification == null)
                    element.NewSpecification();
                element.Specification.GroupId = groupId;
                Add(element);
            }

            return this;
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
                    this.AddElement(attribute.Name, value, propertyInfo.PropertyType.GetValueType());
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
        public T Get<T>([CallerMemberName] String propertyName = null)
        {
            if (propertyName!=null)
            {
                DataElement element = this.GetItem(propertyName);
                if (element!=null)
                {
                    return (T)this.GetElementItemObject(propertyName, this.AppScope);
                }
                else
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
                        Object value = this.GetElementItemObject(attribute.Name, this.AppScope);
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
        public T Get<T>(T defaultValue, [CallerMemberName] String propertyName = null) where T : struct, IConvertible
        {
            if (propertyName != null)
            {
                DataElement element = this.GetItem(propertyName);
                if (element != null)
                {
                    return (T)this.GetElementItemObject(propertyName, this.AppScope);
                }
                else
                {
                    DataElementAttribute attribute = null;
                    PropertyInfo propertyInfo = this.GetPropertyInfo(
                        this.GetType(),
                        propertyName,
                        new Type[] { typeof(DetailPropertyAttribute) },
                        ref attribute,
                        this.AppScope);

                    if (attribute is DetailPropertyAttribute)
                        return (this.GetElementItemObject(attribute.Name, this.AppScope) as String)?.ToEnum<T>(defaultValue) ?? default(T); ;
                }
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
            //this.UpdateFromObject<DetailPropertyAttribute>(this);

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

            //this.UpdateFromElementSet<DetailPropertyAttribute>(this);
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
        public override Boolean SaveXml(String filePath, Log log = null)
        {
            if (base.SaveXml(filePath, log))
            {
                this.CurrentFilePath = filePath;
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
            String filePath,
            Log log,
            IAppScope appScope = null,
            XmlSchemaSet xmlSchemaSet = null,
            Boolean mustFileExist = true) where T : Configuration, new()
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
