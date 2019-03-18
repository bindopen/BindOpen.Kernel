using dkm.core.extensions.items.carriers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace dkm.standard.extension.carriers.emails
{
    /// <summary>
    /// This class represents the email.
    /// </summary>
    [Serializable()]
    [XmlType("Email", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot(ElementName = "email", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class Email : DataCarrier
    {

        // **************************************
        // VARIABLES
        // **************************************

        #region Variables

        private List<String> myEmailUniqueIds = new List<String>();
        private List<String> myAttachementFileNames = new List<String>();

        #endregion


        // **************************************
        // PROPERTIES
        // **************************************

        #region Properties

        /// <summary>
        /// Email unique ID of this instance.
        /// </summary>
        [XmlIgnore()]
        public String EmailUniqueId
        {
            get { return ((this.myEmailUniqueIds == null) || (this.myEmailUniqueIds.Count == 0) ? "" : this.myEmailUniqueIds[0]); }
            set { this.myEmailUniqueIds = new List<String>() { value }; }
        }

        /// <summary>
        /// Email unique IDs of this instance.
        /// </summary>
        [XmlArray("emailUniqueIds")]
        [XmlArrayItem("emailUniqueId")]
        public List<String> EmailUniqueIds
        {
            get { return this.myEmailUniqueIds; }
            set { this.myEmailUniqueIds = value; }
        }

        /// <summary>
        /// Attachement file names of this instance.
        /// </summary>
        [XmlArray("attachementFileNames")]
        [XmlArrayItem("attachementFileName")]
        public List<String> AttachementFileNames
        {
            get { return this.myAttachementFileNames; }
            set { this.myAttachementFileNames = value; }
        }

        #endregion


        // **************************************
        // CONSTRUCTORS
        // **************************************

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Email class.
        /// </summary>
        public Email()
            : base(DocumentKind.Email)
        {
        }

        #endregion


        // ------------------------------------
        // ACCESSORS
        // ------------------------------------

        #region Accessors

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="aIsConfigurationChecked">Indicates whether the configuration is checked.</param>
        /// <returns>The check log.</returns>
        public virtual Log Check(
            Boolean aIsConfigurationChecked = true)
        {
            Log aLog = new Log();

            return aLog;
        }

        /// <summary>
        /// Gets a default title for this instance.
        /// </summary>
        public override String GetDefaultTitle()
        {
            String aPath = "Email";

            if ((this.ReferenceKind == DocumentElementReferenceKind.Single)&&(!String.IsNullOrEmpty(this.EmailUniqueId)))
                aPath += "_" + this.EmailUniqueId;
            else if (this.ReferenceKind == DocumentElementReferenceKind.Multiple)
            {
                if ((this.myEmailUniqueIds.Count > 0)&&(!String.IsNullOrEmpty(this.myEmailUniqueIds[0])))
                    aPath += "_" + Path.GetFileName(this.myEmailUniqueIds[0]);
                if ((this.myEmailUniqueIds.Count > 1)&&(!String.IsNullOrEmpty(this.myEmailUniqueIds[1])))
                    aPath += "," + Path.GetFileName(this.myEmailUniqueIds[1]);
                if ((this.myEmailUniqueIds.Count > 2) && (!String.IsNullOrEmpty(this.myEmailUniqueIds[2])))
                    aPath += ",...";
            }
            else
                aPath = Path.GetFileName(aPath);

            return aPath;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override InformationCarrier Clone()
        {
            return new Email()
            {
                AttachementFileNames = new List<String>(this.AttachementFileNames),
                AvailableConnectorDefinitionUniqueNames = new List<String>(this.AvailableConnectorDefinitionUniqueNames),
                AvailableReferenceKinds = new List<DocumentElementReferenceKind>(this.AvailableReferenceKinds),
                EmailUniqueId = this.EmailUniqueId,
                EmailUniqueIds = new List<string>(this.EmailUniqueIds),
                ConnectionParameterStatement = this.ConnectionParameterStatement,
                ConnectorDefinitionUniqueName = this.ConnectorDefinitionUniqueName,
                ConnectorDefinitionSpecificationLevels = new List<SpecificationLevel>(this.ConnectorDefinitionSpecificationLevels),
                ReferenceKind = this.ReferenceKind,
                ReferenceSpecificationLevels = new List<SpecificationLevel>(this.ReferenceSpecificationLevels),
                PropertyStatement = (this.PropertyStatement == null ? null : this.PropertyStatement.Clone())
            };
        }

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Initializes the path of this instance.
        /// </summary>
        public override void InitializePath()
        {
            this.myEmailUniqueIds = new List<string>();
            this.myAttachementFileNames = new List<string>();
        }

        #endregion


        // ------------------------------------
        // LOADING
        // ------------------------------------

        #region Loading

        /// <summary>
        /// Instantiates a new instance of Email class from a xml string.
        /// </summary>
        /// <param name="aXmlString">The Xml string to load.</param>
        public new static InformationCarrier LoadFromXmlString(String aXmlString)
        {
            Email aEmail = null;
            StringReader aStringReader = null;
            try
            {
                // we parse the xml string
                XDocument aXDocument = XDocument.Parse(aXmlString);

                // then we load
                XmlSerializer aXmlSerializer = new XmlSerializer(typeof(Email));
                aStringReader = new StringReader(aXmlString);
                aEmail = (Email)aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
            }
            catch
            {
            }
            finally
            {
                if (aStringReader != null)
                    aStringReader.Close();
            }

            return aEmail;
        }

        #endregion


        // ------------------------------------------
        // CHECK / REPAIR
        // ------------------------------------------

        #region Check Repair

        /// <summary>
        /// Repairs this instance.
        /// </summary>
        /// <returns>Returns a repair log.</returns>
        public override Log Repair()
        {
            Log aLog = base.Repair();

            return aLog;
        }

        #endregion
    }
}
