using System;
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
    public class DocumentElementSpec : DataElementSpec, IDocumentElementSpec
    {
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
            SpecificationLevel[] specificationLevels = null)
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
        public override bool IsCompatibleWith(IDataItem item)
        {
            bool isCompatible = base.IsCompatibleWith(item);

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
        public override ILog CheckItem(
            object item,
            IDataElement dataElement = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
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
        public override ILog CheckElement(
            IIDataElement dataElement,
            string[] specificationAreas = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
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
        public override object Clone()
        {
            DocumentElementSpec aDocumentElementSpec = base.Clone() as DocumentElementSpec;
            return aDocumentElementSpec;
        }

        #endregion
    }
}
