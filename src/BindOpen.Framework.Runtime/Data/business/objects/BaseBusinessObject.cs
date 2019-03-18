using bdo.core.data.items.dictionary;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace bdo.runtime.data.business.objects
{

    /// <summary>
    /// This class represents base library elements.
    /// </summary>
    [Serializable()]
    [XmlType("BaseBusinessObject", Namespace = "http://www.w3.org/2001/bdo.xsd")]
    [XmlRoot(ElementName = "baseBusinessObject", Namespace = "http://www.w3.org/2001/bdo.xsd", IsNullable = false)]
    public class BaseBusinessObject
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private String _Name = null;
        private DictionaryDataItem _Title = null;
        private DictionaryDataItem _Description = null;

        #endregion

        
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [XmlAttribute("name")]
        public String Name
        {
            get { return this._Name; }
            set { this._Name = value; }
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

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [XmlElement("description")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual DictionaryDataItem Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BaseBusinessObject class.
        /// </summary>
        public BaseBusinessObject()
        {
        }

        #endregion

    }
}