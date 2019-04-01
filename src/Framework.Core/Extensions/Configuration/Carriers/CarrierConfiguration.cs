using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Configuration.Carriers
{
    /// <summary>
    /// This class represents a carrier configuration.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "carrier", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class CarrierConfiguration : TAppExtensionItemConfiguration<ICarrierDefinition>, ICarrierConfiguration
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Path --------------------------

        /// <summary>
        /// Path of this instance.
        /// </summary>
        [XmlElement("path")]
        public string Path { get; set; } = null;

        /// <summary>
        /// Specification of the Path property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool PathSpecified => !string.IsNullOrEmpty(Path);

        /// <summary>
        /// The parent path of this instance.
        /// </summary>
        [XmlElement("parentPath")]
        public string ParentPath { get; set; } = null;

        /// <summary>
        /// Specification of the ParentPath property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ParentPathSpecified => !string.IsNullOrEmpty(ParentPath);

        /// <summary>
        /// The detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public IDataElementSet Detail { get; set; } = null;

        /// <summary>
        /// Specification of the Detail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DetailSpecified => Detail != null && (Detail.ElementsSpecified || Detail.DescriptionSpecified);

        // File properties --------------------------

        /// <summary>
        /// The information flag of this instance.
        /// </summary>
        [XmlElement("flag")]
        public string Flag { get; set; } = null;

        /// <summary>
        /// Specification of the Flag property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool FlagSpecified => !string.IsNullOrEmpty(Flag);

        /// <summary>
        /// Indicates whether this instance is read only.
        /// </summary>
        [XmlElement("isReadOnly")]
        [DefaultValue(false)]
        public bool IsReadonly { get; set; }

        /// <summary>
        /// The date of last access of this instance.
        /// </summary>
        [XmlElement("lastAccessDate")]
        public string LastAccessDate { get; set; } = null;

        /// <summary>
        /// Specification of the LastAccessDate property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool LastAccessDateSpecified => !string.IsNullOrEmpty(LastAccessDate);

        /// <summary>
        /// The date of last write of this instance.
        /// </summary>
        [XmlElement("lastWriteDate")]
        public string LastWriteDate { get; set; } = null;

        /// <summary>
        /// Specification of the LastWriteDate property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool LastWriteDateSpecified => !string.IsNullOrEmpty(LastWriteDate);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        public CarrierConfiguration()
            : base(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="path">The path to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        protected CarrierConfiguration(
            string name,
            ICarrierDefinition definition = default,
            string namePreffix = "carrier_",
            string path = null,
            IDataElementSet detail = null)
            : this(name, definition?.Key(), namePreffix, path, detail)
        {
            _definition = definition;
        }

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="path">The path to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        protected CarrierConfiguration(
            string name,
            string definitionUniqueId,
            string namePreffix = "carrier_",
            string path = null,
            IDataElementSet detail = null)
            : base(name, definitionUniqueId, namePreffix)
        {
            DefinitionUniqueId = definitionUniqueId;
            Path = path;
            Detail = detail;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the detail of this instance and instantiates it if it is null.
        /// </summary>
        /// <returns>Returns the detail of this instance.</returns>
        public IDataElementSet NewDetail()
        {
            return Detail = Detail ?? new DataElementSet();
        }

        #endregion

        // --------------------------------------------------
        // CHECK, UPDATE, REPAIR
        // --------------------------------------------------

        #region Check_Update_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (item is CarrierConfiguration)
                Detail.Update((item as CarrierConfiguration).Detail);

            return log;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the check log.</returns>
        public override ILog Check<T1>(
            bool isExistenceChecked = true,
            T1 item = default,
            string[] specificationAreas = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            return log;
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        public override ILog Repair<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (item is CarrierConfiguration)
                Detail.Repair((item as CarrierConfiguration).Detail);

            return log;
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a clone of this instance.</returns>
        public override object Clone()
        {
            CarrierConfiguration dataCarrier = base.Clone() as CarrierConfiguration;
            if (Detail != null)
                dataCarrier.Detail = Detail.Clone() as DataElementSet;
            return dataCarrier;
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
            Detail?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, ILog log = null)
        {
            Detail?.UpdateRuntimeInfo(appScope, log);
        }

        #endregion
    }
}
