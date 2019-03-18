using cor_base_wdl.data.information._class;
using cor_base_wdl.data.serialization;
using cor_base_wdl.system.logging;
using cor_runtime_wdl.business.objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace cor_runtime_wdl.data._class
{

    /// <summary>
    /// This class represents a configuration definition.
    /// </summary>
    [Serializable()]
    [XmlType("DataClassStatementSet", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot(ElementName = "classStatementSet", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class DataClassStatementSet
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<DataClassStatement> myClassStatements = new List<DataClassStatement>();

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Class statements of this instance.
        /// </summary>
        [XmlArray("class.statements")]
        [XmlArrayItem("add.statement")]
        public List<DataClassStatement> ClassStatements 
        {
            get { return this.myClassStatements; }
            set { this.myClassStatements = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConfigurationDefinition class.
        /// </summary>
        public DataClassStatementSet()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ConfigurationSettings class specifying its information configuration statements.
        /// </summary>
        /// <param name="someInformationConfigurationStatements">The configuration information statements of this instance.</param>
        public DataClassStatementSet(List<DataClassStatement> someInformationConfigurationStatements)
        {
            this.myClassStatements = someInformationConfigurationStatements;
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the distinct group ids of the specified target kind.
        /// </summary>
        /// <param name="aTargetKind">Target kind to consider.</param>
        /// <returns>The distinct names of the configuration files' activities.</returns>
        /// <remarks>Consider all the </remarks>
        public List<String> GetDictinctTargetGroupIds(String aTargetKind)
        {
            List<String> someTargetGroupIds = new List<String>();

            foreach (DataClassStatement aConfigurationFile in this.myClassStatements)
                if (
                    ((aConfigurationFile.TargetGroupId != null) &&
                    (!someTargetGroupIds.Contains(aConfigurationFile.TargetGroupId.ToUpper()))) &
                    ((aTargetKind == null) || (aConfigurationFile.TargetKind == aTargetKind))
                    )
                    someTargetGroupIds.Add(aConfigurationFile.TargetGroupId.ToUpper());

            return someTargetGroupIds;
        }

        /// <summary>
        /// Returns the distinct target group business object codes.
        /// </summary>
        /// <param name="aTargetKind">Target category to consider.</param>
        /// <returns>The distinct names of the configuration files' activities.</returns>
        public List<BusinessObjectCode> GetTargetGroupBusinessObjectCodes(String aTargetKind)
        {
            List<BusinessObjectCode> someActivityBusinessObjectCodes = new List<BusinessObjectCode>();
            List<String> someTargetGroupIds = new List<String>();

            foreach (DataClassStatement aConfigurationFileStatement in this.myClassStatements)
                if (
                    ((aConfigurationFileStatement.TargetGroupId != null) &&
                    (!someTargetGroupIds.Contains(aConfigurationFileStatement.TargetGroupId.ToUpper()))) &
                    ((aTargetKind == null) || (aConfigurationFileStatement.TargetKind == aTargetKind))  
                    )
                {
                    someTargetGroupIds.Add(aConfigurationFileStatement.TargetGroupId.ToUpper());

                    BusinessObjectCode aBusinessObjectCode = new BusinessObjectCode();
                    aBusinessObjectCode.Code=aConfigurationFileStatement.TargetGroupId.ToUpper();
                    aBusinessObjectCode.Title=aConfigurationFileStatement.TargetGroupTitle;
                    someActivityBusinessObjectCodes.Add(aBusinessObjectCode);
                }

            return someActivityBusinessObjectCodes;
        }

        /// <summary>
        /// Gets the class statements with the specified target name or kind. Use null value to consider all.
        /// </summary>
        /// <param name="aTargetId">Target ID to consider.</param>
        /// <param name="aTargetKind">Target kind to consider.</param>
        /// <returns>Returns the configuration statements.</returns>
        public List<DataClassStatement> GetStatements(
            String aTargetId,
            String aTargetKind)
        {
            List<DataClassStatement> someInformationConfigurationStatements = new List<DataClassStatement>();

            foreach (DataClassStatement aConfigurationFileStatement in this.myClassStatements)
                if (
                    ((aTargetId == null) || (aConfigurationFileStatement.TargetId == aTargetId)) &
                    ((aTargetKind == null) || (aConfigurationFileStatement.TargetKind == aTargetKind))
                    )
                    someInformationConfigurationStatements.Add(aConfigurationFileStatement);

            return someInformationConfigurationStatements;
        }

        /// <summary>
        /// Gets the class statements with the specified target name, kind or group ID. Use null value to consider all except for group ID that means no group.
        /// </summary>
        /// <param name="aTargetId">Target ID to consider.</param>
        /// <param name="aTargetKind">Target kind to consider.</param>
        /// <param name="aTargetGroupId">ID of the target group to consider.</param>
        /// <returns>Returns the configuration statements.</returns>
        public List<DataClassStatement> GetStatements(
            String aTargetId,
            String aTargetKind,
            String aTargetGroupId)
        {
            List<DataClassStatement> someInformationConfigurationStatements = new List<DataClassStatement>();

            foreach (DataClassStatement aConfigurationFileStatement in this.myClassStatements)
                if (
                    ((aTargetId == null) || (aConfigurationFileStatement.TargetId == aTargetId)) &
                    ((aTargetKind == null) || (aConfigurationFileStatement.TargetKind == aTargetKind)) &
                    (aConfigurationFileStatement.TargetGroupId == aTargetGroupId)
                    )
                    someInformationConfigurationStatements.Add(aConfigurationFileStatement);

            return someInformationConfigurationStatements;
        }

        #endregion


        // **************************************
        // LOAD, SAVE
        // **************************************

        #region Load_Save

        /// <summary>
        /// Instantiates a new instance of ConfigurationSettings class from a xml string.
        /// </summary>
        /// <param name="aXmlString">The Xml string to load.</param>
        /// <param name="aIsCheckXml">Indicates whether the file must be checked.</param>
        /// <param name="aLoadLog">The output log of the load task.</param>
        /// <returns>The log defined in the Xml file.</returns>
        public static DataClassStatementSet LoadFromXmlString(
            String aXmlString,
            Boolean aIsCheckXml,
            Log aLoadLog)
        {
            DataClassStatementSet aConfigurationSettings = new DataClassStatementSet();

            Log aChildLoadLog = new Log();

            StringReader aStringReader = null;
            try
            {
                // we check it
                if (aIsCheckXml)
                {
                    // we parse the xml string
                    XDocument aXDocument = XDocument.Parse(aXmlString);

                    XmlSchemaSet aXmlSchemaSet = null;
                    XsdLoader.LoadXmlSchemaSet(
                        aXmlSchemaSet,
                        Assembly.Load("cor_base_wdl"),
                        new List<String>()
                        {
                            "cor_base_wdl.data.serialization.xsd.ConfigurationSettings.xsd",
                            "cor_base_wdl.data.serialization.xsd.DataElement.xsd",
                            "cor_base_wdl.data.serialization.xsd.DataValueType.xsd",
                            "cor_base_wdl.data.serialization.xsd.DictionaryDataItem.xsd",
                            "cor_base_wdl.data.serialization.xsd.DictionaryDataItemValue.xsd",
                            "cor_base_wdl.data.serialization.xsd.DictionaryDataItemValueType.xsd"
                        });
                    aXDocument.Validate(aXmlSchemaSet, (o, e) =>
                    {
                        aChildLoadLog.AddError(
                            "Xml String not valid.",
                            EventCriticality.High,
                            "",
                            e.Message
                            );
                    });
                }


                if (!aChildLoadLog.HasWarningOrErrorOrException())
                {
                    // then we load
                    XmlSerializer aXmlSerializer = new XmlSerializer(typeof(DataClassStatementSet));
                    aStringReader = new StringReader(aXmlString);
                    aConfigurationSettings = (DataClassStatementSet)aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
                }
            }
            catch (Exception ex)
            {
                aChildLoadLog.AddException(
                    ex,
                    EventCriticality.High,
                    ""
                    );
            }
            finally
            {
                if (aStringReader != null)
                    aStringReader.Close();
            }

            if (aLoadLog != null)
                aLoadLog.AddEvents(aChildLoadLog);

            return aConfigurationSettings;
        }

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="aFilePath">Path of the file to save.</param>
        /// <returns>True if the saving operation has been done. False otherwise.</returns>
        public Boolean SaveXml(String aFilePath)
        {
            Boolean aIsWasSaved = false;
            StreamWriter aStreamWriter = null;
            try
            {
                if (aFilePath != "")
                {
                    // we create the folder if it does not exist
                    if (!Directory.Exists(Path.GetDirectoryName(aFilePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(aFilePath));

                    // we save the xml file
                    XmlSerializer aXmlSerializer = new XmlSerializer(this.GetType());
                    aStreamWriter = new StreamWriter(aFilePath, false, System.Text.Encoding.UTF8);
                    aXmlSerializer.Serialize(aStreamWriter, this);
                    aIsWasSaved = true;
                }
            }
            catch
            {
                aIsWasSaved = false;
            }
            finally
            {
                if (aStreamWriter != null)
                    aStreamWriter.Close();
            }

            return aIsWasSaved;
        }

        /// <summary>
        /// Gets the xml string of this instance.
        /// </summary>
        /// <returns>The Xml string of this instance.</returns>
        public String ToXml()
        {
            String st = "";
            StringWriter aStreamWriter = null;
            try
            {
                XmlSerializer aXmlSerializer = new XmlSerializer(this.GetType());
                aStreamWriter = new StringWriter();
                aXmlSerializer.Serialize(aStreamWriter, this);
                st = aStreamWriter.ToString();
            }
            catch
            {
            }
            finally
            {
                if (aStreamWriter != null)
                    aStreamWriter.Close();
            }

            return st;
        }

        #endregion

    }
}