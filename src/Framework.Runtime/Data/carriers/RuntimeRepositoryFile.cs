using cor_base_wdl.data.carriers;
using cor_base_wdl.data.carriers.repository;
using cor_base_wdl.data.information._class;
using cor_base_wdl.data.documents.format;
using cor_base_wdl.data.information.levels;
using cor_base_wdl.data.objects.generic;
using cor_base_wdl.system.logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace cor_runtime_wdl.data.carriers
{

    /// <summary>
    /// This class represents a runtime repository file.
    /// </summary>
    [Serializable()]
    [XmlType("RuntimeRepositoryFile", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot(ElementName = "runtimeRepositoryFile", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class RuntimeRepositoryFile : RepositoryFile
    {

        // **************************************
        // VARIABLES
        // **************************************

        #region Variables

        private DataClassStatement myConfiguration = null;

        #endregion


        // **************************************
        // PROPERTIES
        // **************************************

        #region Properties

        /// <summary>
        /// Instance configuration of this instance.
        /// </summary>
        [XmlElement("configuration")]
        public DataClassStatement Configuration
        {
            get { return this.myConfiguration; }
            set { this.myConfiguration = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RuntimeRepositoryFile class.
        /// </summary>
        public RuntimeRepositoryFile() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="aFilePath">The file path to consider.</param>
        public RuntimeRepositoryFile(String aFilePath) : base(aFilePath)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="aFolderPath">The folder path to consider.</param>
        /// <param name="aFileFilter">The file filter to consider.</param>
        public RuntimeRepositoryFile(String aFolderPath, String aFileFilter) : base (aFolderPath, aFileFilter)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="someFilePaths">The file paths to consider.</param>
        public RuntimeRepositoryFile(List<String> someFilePaths) : base(someFilePaths)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RuntimeRepositoryFile class.
        /// </summary>
        /// <param name="aFilePath">The file path to consider.</param>
        /// <param name="aReferenceKind">The file kind to consider.</param>
        public RuntimeRepositoryFile(String aFilePath, DocumentElementReferenceKind aReferenceKind) : base(aFilePath, aReferenceKind)
        {
        }

        #endregion


        // ------------------------------------------
        // LOAD
        // ------------------------------------------

        #region Load

        /// <summary>
        /// Instantiates a new instance of RepositoryFile class from a xml string.
        /// </summary>
        /// <param name="aXmlString">The Xml string to load.</param>
        public new static InformationCarrier LoadFromXmlString(String aXmlString)
        {
            RepositoryFile aRepositoryFile = null;
            StringReader aStringReader = null;
            try
            {
                // we parse the xml string
                XDocument aXDocument = XDocument.Parse(aXmlString);

                // then we load
                XmlSerializer aXmlSerializer = new XmlSerializer(typeof(RuntimeRepositoryFile));
                aStringReader = new StringReader(aXmlString);
                aRepositoryFile = (RepositoryFile)aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
            }
            catch (Exception ex)
            {
                String st = ex.ToString();
            }
            finally
            {
                if (aStringReader != null)
                    aStringReader.Close();
            }

            return aRepositoryFile;
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns an object attribute corresponding to this isntance.
        /// </summary>
        /// <returns>An object attribute corresponding to this isntance.</returns>
        public override DataElement GetDataElement()
        {
            DataElement aDataElement = new DataElement()
            {
                CarrierKind = DocumentKind.RepositoryFile,
                ClassStatement = this.Configuration.Clone()
            };
            RuntimeRepositoryFile aRepositoryFile = (RuntimeRepositoryFile)this.Clone();
            aRepositoryFile.Configuration = null;
            aDataElement.SetValue(aRepositoryFile);
            return aDataElement;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="aIsFileExistence">Indicates whether the file existence is checked.</param>
        /// <param name="aIsConfigurationChecked">Indicates whether the configuration is checked.</param>
        /// <returns>The check log.</returns>
        public override Log Check(
            Boolean aIsFileExistence = true,
            Boolean aIsConfigurationChecked = true)
        {
            Log aLog = base.Check(aIsFileExistence, aIsConfigurationChecked);

            if (aIsConfigurationChecked)
                if (this.Configuration == null)
                    aLog.AddError("Configuration missing.", EventCriticality.High, "", "");
                else
                {
                    if (String.IsNullOrEmpty(this.Configuration.FormatUniqueName))
                        aLog.AddError("Format missing.", EventCriticality.High, "", "");

                    if ((this.Configuration.FormatReference == null) || (this.Configuration.FormatReference.Object == null))
                        aLog.AddError("No format settings definied.", EventCriticality.High, "", "");
                    else
                        aLog.AddEvents((this.Configuration.FormatReference.Object as DataFormat).CheckValidity());
                }

            return aLog;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override InformationCarrier Clone()
        {
            return new RuntimeRepositoryFile()
            {
                AvailableConnectorDefinitionUniqueNames = new List<String>(this.AvailableConnectorDefinitionUniqueNames),
                AvailableReferenceKinds = new List<DocumentElementReferenceKind>(this.AvailableReferenceKinds),
                Configuration = (this.Configuration == null ? null : this.Configuration.Clone()),
                ConnectionParameterStatement = this.ConnectionParameterStatement,
                ConnectorDefinitionUniqueName = this.ConnectorDefinitionUniqueName,
                ConnectorDefinitionSpecificationLevels = new List<SpecificationLevel>(this.ConnectorDefinitionSpecificationLevels),
                ReferenceKind = this.ReferenceKind,
                ReferenceSpecificationLevels = new List<SpecificationLevel>(this.ReferenceSpecificationLevels),
                Filter = this.Filter,
                IsRecursive = this.IsRecursive,
                IsSubFoldersConsidered = this.IsSubFoldersConsidered,
                Path = this.Path,
                PropertyStatement = (this.PropertyStatement == null ? null : this.PropertyStatement.Clone()),
                Paths = new List<string>(this.Paths),
                RelativePath = this.RelativePath,
            };
        }
        
        #endregion

    }
}
