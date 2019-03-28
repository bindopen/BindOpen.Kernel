using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Document
{

    /// <summary>
    /// This class represents a document element specification.
    /// </summary>
    [Serializable()]
    [XmlType("DocumentElementSpec", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DocumentElementSpec : DataElementSpec
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new document element specification.
        /// </summary>
        public DocumentElementSpec(): base()
        {
        }

        /// <summary>
        /// Initializes a new document element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public DocumentElementSpec(
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
        }

        #endregion


        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param name="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public override Boolean IsCompatibleWith(DataItem item)
        {
            Boolean isCompatible = base.IsCompatibleWith(item);

            if (isCompatible)
            {

            }

            return isCompatible;
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public override Log CheckItem(
            Object item,
            DataElement dataElement = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            return new Log();
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public override Log CheckElement(
            DataElement dataElement,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            return new Log();
        }

        #endregion


        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair


        #endregion


        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override Object Clone()
        {
            DocumentElementSpec aDocumentElementSpec = base.Clone() as DocumentElementSpec;
            //entityElementSpec.EntityUniqueNameFilter = this.EntityUniqueNameFilter.Clone() as DataValueFilter;
            //entityElementSpec.FormatUniqueNameFilter = this.FormatUniqueNameFilter.Clone() as DataValueFilter;
            return aDocumentElementSpec;
        }

        #endregion

    }

}
