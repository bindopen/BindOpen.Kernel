using BindOpen.Framework.Core.Data.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Application.Contacts
{

    /// <summary>
    /// This class represents a contact.
    /// </summary>
    [Serializable()]
    [XmlType("Contact", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "contact", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class Contact : StoredDataItem
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private String _Email = "";
        private String _SmsPhone = "";
        private String _VocalPhone = "";

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Email of this instance.
        /// </summary>
        [XmlElement("email")]
        public String Email
        {
            get { return this._Email; }
            set { this._Email = value; }
        }

        /// <summary>
        /// Sms phone of this instance.
        /// </summary>
        [XmlElement("smsPhone")]
        public String SmsPhone
        {
            get { return this._SmsPhone; }
            set { this._SmsPhone = value; }
        }

        /// <summary>
        /// Vocal phone of this instance.
        /// </summary>
        [XmlElement("vocalPhone")]
        public String VocalPhone
        {
            get { return this._VocalPhone; }
            set { this._VocalPhone = value; }
        }

        #endregion
        

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the UserContact class.
        /// </summary>
        public Contact()
        {
        }

        #endregion


        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Cloning ------------------------------------------

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned object.</returns>
        public override Object Clone()
        {
            return base.Clone() as Contact;
        }

        #endregion

    }
}
