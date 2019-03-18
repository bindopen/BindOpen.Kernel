using System;
using System.Collections.Generic;
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
    public class CarrierConfiguration : TAppExtensionItemConfiguration<CarrierDefinition>
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Path --------------------------

        /// <summary>
        /// Path of this instance.
        /// </summary>
        [XmlElement("path")]
        public String Path { get; set; } = null;

        /// <summary>
        /// Specification of the Path property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean PathSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.Path);
            }
        }

        /// <summary>
        /// The parent path of this instance.
        /// </summary>
        [XmlElement("parentPath")]
        public String ParentPath { get; set; } = null;

        /// <summary>
        /// Specification of the ParentPath property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ParentPathSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.ParentPath);
            }
        }

        /// <summary>
        /// The detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public DataElementSet Detail { get; set; } = null;

        /// <summary>
        /// Specification of the Detail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DetailSpecified
        {
            get
            {
                return this.Detail != null && (this.Detail.ElementsSpecified || this.Detail.DescriptionSpecified);
            }
        }

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
        public Boolean FlagSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.Flag);
            }
        }

        /// <summary>
        /// Indicates whether this instance is read only.
        /// </summary>
        [XmlElement("isReadOnly")]
        [DefaultValue(false)]
        public Boolean IsReadonly { get; set; }

        /// <summary>
        /// The date of last access of this instance.
        /// </summary>
        [XmlElement("lastAccessDate")]
        public String LastAccessDate { get; set; } = null;

        /// <summary>
        /// Specification of the LastAccessDate property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean LastAccessDateSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.LastAccessDate);
            }
        }

        /// <summary>
        /// The date of last write of this instance.
        /// </summary>
        [XmlElement("lastWriteDate")]
        public String LastWriteDate { get; set; } = null;

        /// <summary>
        /// Specification of the LastWriteDate property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean LastWriteDateSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.LastWriteDate);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        public CarrierConfiguration()
            : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="path">The path to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        public CarrierConfiguration(
            String name,
            String definitionName,
            String namePreffix = "carrier_",
            String path = null,
            DataElementSet detail = null)
            : base(name, definitionName, null, namePreffix)
        {
            this.Path = path;
            this.Detail = detail;
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
        public DataElementSet NewDetail()
        {
            return this.Detail = this.Detail ?? new DataElementSet();
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
        public override Log Update<T1>(
            T1 item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is CarrierConfiguration)
                this.Detail.Update((item as CarrierConfiguration).Detail);

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
        public override Log Check<T1>(
            Boolean isExistenceChecked = true,
            T1 item = null,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

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
        public override Log Repair<T1>(
            T1 item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is CarrierConfiguration)
                this.Detail.Repair((item as CarrierConfiguration).Detail);

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
        public override Object Clone()
        {
            CarrierConfiguration dataCarrier = base.Clone() as CarrierConfiguration;
            if (this.Detail != null)
                dataCarrier.Detail = this.Detail.Clone() as DataElementSet;
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
        public override void UpdateStorageInfo(Log log = null)
        {
            this.Detail?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, Log log = null)
        {
            this.Detail?.UpdateRuntimeInfo(appScope, log);
        }

        #endregion
    }
}
