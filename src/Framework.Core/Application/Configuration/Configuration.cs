using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    [XmlType("Configuration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("config", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class Configuration : DataElementSet, IConfiguration
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; } = null;

        /// <summary>
        /// Specification of the ID of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool IdSpecified => !string.IsNullOrEmpty(this.Id);

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [XmlAttribute("name")]
        [DefaultValue("")]
        public string Name { get; set; } = "";

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
        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        public Configuration() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public Configuration(params IDataElement[] items)
            : base(items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="items">The items to consider.</param>
        public Configuration(string filePath, params IDataElement[] items)
            : base(items)
        {
            CurrentFilePath = filePath;
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
        public IConfiguration AddGroup(string groupId, params IDataElement[] items)
        {
            if (items!=null)
            {
                foreach (DataElement element in items.Cast<DataElement>())
                {
                    if (element.Specification == null)
                        element.NewSpecification();
                    element.Specification.GroupId = groupId;

                    Add(element);
                }
            }

            return this;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the identifier key.
        /// </summary>
        public virtual string Key()
        {
            return this.Id;
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
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="log">The output log.</param>
        /// <returns>True if this instance has been</returns>
        public bool SaveXml(string filePath, ILog log = null)
        {
            UpdateStorageInfo(log);

            if (XmlHelper.SaveXml(this, filePath, log))
            {
                CurrentFilePath = filePath;
                return true;
            }

            return false;
        }

        #endregion
    }
}
