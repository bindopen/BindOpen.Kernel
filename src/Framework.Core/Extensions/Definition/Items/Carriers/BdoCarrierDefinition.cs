using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Definition;
using System;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// This class represents a carrier definition.
    /// </summary>
    public class BdoCarrierDefinition : DataItem, IBdoCarrierDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The library of this instance.
        /// </summary>
        public IBdoExtensionDefinition ExtensionDefinition { get; }

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IBdoCarrierDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueId { get => ExtensionDefinition?.Dto.Name + "$" + Dto?.Name; set { } }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoCarrierDefinition class.
        /// </summary>
        public BdoCarrierDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoCarrierDefinition class.
        /// </summary>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        public BdoCarrierDefinition(IBdoExtensionDefinition extensionDefinition, IBdoCarrierDefinitionDto dto)
        {
            ExtensionDefinition = extensionDefinition;
            Dto = dto;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public string Key()
        {
            return UniqueId;
        }

        ///// <summary>
        ///// Returns a text summarizing this instance.
        ///// </summary>
        ///// <param name="logFormat">The log format to consider.</param>
        ///// <param name="uiCulture">The UI culture to consider.</param>
        ///// <returns>A text summarizing this instance.</returns>
        //public override string GetText(BdoLoggerFormat logFormat = BdoLoggerFormat.Xml, string uiCulture = "*")
        //{
        //    string st = "";
        //    if (Dto!=null)
        //    {
        //        switch (logFormat)
        //        {
        //            case BdoLoggerFormat.Xml:
        //                st += "<span style='color: blue;' >" + this.Key() + "</span> (" + Dto?.DatasourceKind.ToString() + ")<br>";
        //                st += "<br>";
        //                st += "Modified: " + Dto?.LastModificationDate + "<br>";
        //                st += "<br>";
        //                st += Dto?.Description.GetContent(uiCulture);
        //                st += "<br>";
        //                st += "<strong>Library: " + Dto?.ExtensionDefinition?.Name + "</strong>";
        //                st += "<br>";
        //                st += "<strong>Path</strong>";
        //                st += "<br>";
        //                //foreach (DataElement dataElement in this._PathStatement.Elements)
        //                //    parameterstring += (parameterstring == String.Empty ? "" : ",") +
        //                //        "<span style='color: blue;'>&lt;" + dataElement.ValueType.ToString() + "&gt;</span> " + dataElement.Name + ",";
        //                break;
        //        }
        //    }

        //    return st;
        //}

        #endregion
    }
}
