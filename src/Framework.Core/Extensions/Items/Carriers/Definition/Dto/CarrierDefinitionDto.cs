using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers.Definition.Dto
{
    /// <summary>
    /// This class represents a DTO carrier definition.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierDefinition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "carrier.definition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class CarrierDefinitionDto : AppExtensionItemDefinitionDto, ICarrierDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public string ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        [XmlElement("dataSourceKind")]
        public DataSourceKind DataSourceKind { get; set; } = DataSourceKind.None;

        /// <summary>
        /// The set of element specifications of this instance.
        /// </summary>
        [XmlElement("detail.specification")]
        public DataElementSpecSet DetailSpec { get; set; } = new DataElementSpecSet();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierDefinitionDto class.
        /// </summary>
        public CarrierDefinitionDto()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns a text summarizing this instance.
        /// </summary>
        /// <param name="logFormat">The log format to consider.</param>
        /// <param name="uiCulture">The UI culture to consider.</param>
        /// <returns>A text summarizing this instance.</returns>
        public override string GetText(LogFormat logFormat= LogFormat.Xml, string uiCulture = "*")
        {
            string st = "";
            switch(logFormat)
            {
                case LogFormat.Xml:
                    st += "<span style='color: blue;' >" + this.Key() + "</span> (" + this.DataSourceKind.ToString() + ")<br>";
                    st += "<br>";
                    st += "Modified: " + this.LastModificationDate + "<br>";
                    st += "<br>";
                    st += this.Description.GetContent(uiCulture);
                    st += "<br>";
                    st += "<strong>Library: " + this.LibraryName + "</strong>";
                    st += "<br>";
                    st += "<strong>Path</strong>";
                    st += "<br>";
                    //foreach (DataElement dataElement in this._PathStatement.Elements)
                    //    parameterstring += (parameterstring == String.Empty ? "" : ",") +
                    //        "<span style='color: blue;'>&lt;" + dataElement.ValueType.ToString() + "&gt;</span> " + dataElement.Name + ",";
                    break;
            }

            return st;
        }

        #endregion
    }
}
