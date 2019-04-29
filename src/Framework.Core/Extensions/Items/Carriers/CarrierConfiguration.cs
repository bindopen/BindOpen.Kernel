using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Items.Carriers.Definition;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers
{
    /// <summary>
    /// This class represents a carrier configuration.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "carrier", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class CarrierConfiguration
        : TAppExtensionTitledItemConfiguration<ICarrierDefinition>, ICarrierConfiguration
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        public CarrierConfiguration() : base(AppExtensionItemKind.Carrier, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public CarrierConfiguration(
            params IDataElement[] items)
            : base(AppExtensionItemKind.Carrier, null, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public CarrierConfiguration(
            string definitionUniqueId,
            params IDataElement[] items)
            : base(AppExtensionItemKind.Carrier, definitionUniqueId, items)
        {
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

            if (item is CarrierConfiguration configuration)
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

            if (item is CarrierConfiguration configuration)
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

            if (item is CarrierConfiguration configuration)
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
            CarrierConfiguration configuration = base.Clone() as CarrierConfiguration;
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
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
        }

        #endregion
    }
}
