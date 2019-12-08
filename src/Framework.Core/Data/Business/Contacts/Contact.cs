using BindOpen.Framework.Core.Data.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Business.Contacts
{

    /// <summary>
    /// This class represents a contact.
    /// </summary>
    [Serializable()]
    [XmlType("Contact", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "contact", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class Contact : NamedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Email of this instance.
        /// </summary>
        [XmlElement("email")]
        public string Email { get; set; } = "";

        /// <summary>
        /// Sms phone of this instance.
        /// </summary>
        [XmlElement("smsPhone")]
        public string SmsPhone { get; set; } = "";

        /// <summary>
        /// Vocal phone of this instance.
        /// </summary>
        [XmlElement("vocalPhone")]
        public string VocalPhone { get; set; } = "";

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Contact class.
        /// </summary>
        public Contact()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Contact class.
        /// </summary>
        public Contact(string name) : base(name, "contact_")
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
        public override object Clone()
        {
            return base.Clone() as Contact;
        }

        #endregion
    }
}
