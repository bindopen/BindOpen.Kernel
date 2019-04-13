using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers
{
    /// <summary>
    /// This class represents a carrier configuration.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierDto", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "carrier", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class CarrierDto
        : TAppExtensionTitledItemDto<ICarrierDefinition>, ICarrierDto
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
        /// Instantiates a new instance of the CarrierDto class.
        /// </summary>
        public CarrierDto() : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CarrierDto class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="path">The path to consider.</param>
        /// <param name="items">The items to consider.</param>
        public CarrierDto(
            string definitionUniqueId,
            string path = null,
            params IDataElement[] items)
            : base(AppExtensionItemKind.Carrier, definitionUniqueId, items)
        {
            Path = path;
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
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            ILog log = new Log();

            if (item is CarrierDto configuration)
                log.Append(base.Update(configuration));

            return log;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public override ILog Check<T1>(
            bool isExistenceChecked = true,
            T1 item = default,
            string[] specificationAreas = null)
        {
            ILog log = new Log();

            if (item is CarrierDto configuration)
            {
                log.Append(Check(isExistenceChecked, configuration, specificationAreas));
            }
            return log;
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        public override ILog Repair<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            ILog log = new Log();

            if (item is CarrierDto configuration)
                log.Append(base.Repair(configuration));

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
            CarrierDto configuration = base.Clone() as CarrierDto;
            return configuration;
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
        public override void UpdateRuntimeInfo(ILog log = null)
        {
            base.UpdateRuntimeInfo(log);
        }

        #endregion
    }
}
