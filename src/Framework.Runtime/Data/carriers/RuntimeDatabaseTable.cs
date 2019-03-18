using cor_base_wdl.data.carriers;
using cor_base_wdl.data.carriers.database;
using cor_base_wdl.data.information._class;
using cor_base_wdl.data.documents.format;
using cor_base_wdl.data.information.levels;
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
    /// This class represents the runtime database table.
    /// </summary>
    [Serializable()]
    [XmlType("RuntimeDatabaseTable", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot(ElementName = "runtimeDatabaseTable", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class RuntimeDatabaseTable : DatabaseTable
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
        

        // **************************************
        // CONSTRUCTORS
        // **************************************

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RuntimeDatabaseTable class.
        /// </summary>
        public RuntimeDatabaseTable() : base()
        {
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override InformationCarrier Clone()
        {
            return new RuntimeDatabaseTable()
            {
                DataModule = this.DataModule,
                Name = this.Name,
                Names = new List<string>(this.Names),
                AvailableConnectorDefinitionUniqueNames = new List<String>(this.AvailableConnectorDefinitionUniqueNames),
                AvailableReferenceKinds = new List<DocumentElementReferenceKind>(this.AvailableReferenceKinds),
                Configuration = (this.Configuration == null ? null : this.Configuration.Clone()),
                ConnectionParameterStatement = this.ConnectionParameterStatement,
                ConnectorDefinitionUniqueName = this.ConnectorDefinitionUniqueName,
                ConnectorDefinitionSpecificationLevels = new List<SpecificationLevel>(this.ConnectorDefinitionSpecificationLevels),
                ReferenceKind = this.ReferenceKind,
                ReferenceSpecificationLevels = new List<SpecificationLevel>(this.ReferenceSpecificationLevels),
                PropertyStatement = (this.PropertyStatement == null ? null : this.PropertyStatement.Clone())
            };
        }

        #endregion


        // ------------------------------------
        // LOADING
        // ------------------------------------

        #region Loading

        /// <summary>
        /// Instantiates a new instance of DatabaseTable class from a xml string.
        /// </summary>
        /// <param name="aXmlString">The Xml string to load.</param>
        public new static InformationCarrier LoadFromXmlString(String aXmlString)
        {
            DatabaseTable aDatabaseTable = null;
            StringReader aStringReader = null;
            try
            {
                // we parse the xml string
                XDocument aXDocument = XDocument.Parse(aXmlString);

                // then we load
                XmlSerializer aXmlSerializer = new XmlSerializer(typeof(RuntimeDatabaseTable));
                aStringReader = new StringReader(aXmlString);
                aDatabaseTable = (DatabaseTable)aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
            }
            catch
            {
            }
            finally
            {
                if (aStringReader != null)
                    aStringReader.Close();
            }

            return aDatabaseTable;
        }

        #endregion


        // ------------------------------------------
        // CHECK / REPAIR
        // ------------------------------------------

        #region Check Repair

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="aIsTableExistence">Indicates whether the table existence is checked.</param>
        /// <param name="aIsConfigurationChecked">Indicates whether the configuration is checked.</param>
        /// <returns>The check log.</returns>
        public override Log Check(
            Boolean aIsTableExistence = true,
            Boolean aIsConfigurationChecked = true)
        {
            Log aLog = base.Check(aIsTableExistence, aIsConfigurationChecked);

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

        #endregion

    }
}
