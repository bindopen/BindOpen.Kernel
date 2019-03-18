using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;

namespace BindOpen.Framework.Core.Extensions.Definition.Carriers
{
    /// <summary>
    /// This class represents a script word definition.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "carrier.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class CarrierDefinition : AppExtensionItemDefinition
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DataSourceKind _DataSourceKind = DataSourceKind.None;
        private DataElementSpecificationSet _PathSpecification = new DataElementSpecificationSet();

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public String ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        [XmlElement("dataSourceKind")]
        public DataSourceKind DataSourceKind
        {
            get { return this._DataSourceKind; }
            set { this._DataSourceKind = value; }
        }

        /// <summary>
        /// The set of element specifications of this instance.
        /// </summary>
        [XmlElement("path.specification")]
        public DataElementSpecificationSet PathSpecification
        {
            get { return this._PathSpecification; }
            set { this._PathSpecification = value; }
        }
        
        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierDefinition class.
        /// </summary>
        public CarrierDefinition()
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
        public override String GetText(LogFormat logFormat= LogFormat.Xml, String uiCulture = "*")
        {
            String st = "";
            switch(logFormat)
            {
                case LogFormat.Xml:
                    st += "<span style='color: blue;' >" + this.Key() + "</span> (" + this._DataSourceKind.ToString() + ")<br>";
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
                    //    parameterString += (parameterString == String.Empty ? "" : ",") +
                    //        "<span style='color: blue;'>&lt;" + dataElement.ValueType.ToString() + "&gt;</span> " + dataElement.Name + ",";
                    break;
            }

            return st;
        }

        #endregion
    
    }
}
