using bdo.core.data.items.dictionary;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace bdo.runtime.data.business.objects
{

    /// <summary>
    /// This class represents a business object code.
    /// </summary>
    [Serializable()]
    [XmlType("BusinessObjectCode", Namespace = "http://www.w3.org/2001/bdo.xsd")]
    [XmlRoot(ElementName = "businessObjectCode", Namespace = "http://www.w3.org/2001/bdo.xsd", IsNullable = false)]
    public class BusinessObjectCode
    {        

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Code of this instance.
        /// </summary>
        [XmlAttribute("code")]
        public String Code
        {
            get { return this._Code; }
            set { this._Code = value; }
        }

        /// <summary>
        /// Text of this instance.
        /// </summary>
        [Localizable(true)]
        public String Text
        {
            get { return this._Text; }
            set { this._Text = value; }
        }

        /// <summary>
        /// Title of this instance.
        /// </summary>
        [XmlElement("title")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual DictionaryDataItem Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessObjectCode class.
        /// </summary>
        public BusinessObjectCode()
        {
        }

        #endregion

    }
}