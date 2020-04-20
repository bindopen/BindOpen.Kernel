using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a carrier configuration.
    /// </summary>
    [XmlType("BdoCarrierConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "carrier", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoCarrierConfiguration
        : TBdoExtensionTitledItemConfiguration<IBdoCarrierDefinition>, IBdoCarrierConfiguration
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        public BdoCarrierConfiguration() : base(BdoExtensionItemKind.Carrier, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public BdoCarrierConfiguration(
            params IDataElement[] items)
            : base(BdoExtensionItemKind.Carrier, null, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CarrierConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public BdoCarrierConfiguration(
            string definitionUniqueId,
            params IDataElement[] items)
            : base(BdoExtensionItemKind.Carrier, definitionUniqueId, items)
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
        public override IBdoLog Update<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is BdoCarrierConfiguration configuration)
                log.AddEvents(base.Update(configuration));

            return log;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public override IBdoLog Check<T1>(
            bool isExistenceChecked = true,
            T1 item = default,
            string[] specificationAreas = null)
        {
            var log = new BdoLog();

            if (item is BdoCarrierConfiguration configuration)
            {
                log.AddEvents(Check(isExistenceChecked, configuration, specificationAreas));
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
        public override IBdoLog Repair<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is BdoCarrierConfiguration configuration)
                log.AddEvents(base.Repair(configuration));

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
            BdoCarrierConfiguration configuration = base.Clone() as BdoCarrierConfiguration;
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
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion
    }
}
