using BindOpen.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.Data.Business
{

    /// <summary>
    /// This class represents a contact.
    /// </summary>
    [XmlType("Contact", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot(ElementName = "contact", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
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
        public override object Clone(params string[] areas)
        {
            return base.Clone(areas) as Contact;
        }

        #endregion
    }
}
