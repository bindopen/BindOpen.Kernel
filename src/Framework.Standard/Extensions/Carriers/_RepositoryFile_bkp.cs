using dkm.core.extensions.items.carriers;
using dkm.core.system.diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace dkm.standard.extension.carriers.repository
{

    /// <summary>
    /// This class represents a repository file.
    /// </summary>
    [Serializable()]
    [XmlType("RepositoryFile", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot(ElementName = "repositoryFile", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class RepositoryFile : DataCarrier
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private Boolean myIsSubFoldersConsidered = false;
        private String myRelativePath = "";
        private String myPath = "";
        private List<String> myPaths = new List<String>();
        private String myFilter = null;
        private Boolean myIsRecursive = false;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates whether this instance considers the sub folders.
        /// </summary>
        [XmlElement("isSubFoldersConsidered")]
        public Boolean IsSubFoldersConsidered
        {
            get { return this.myIsSubFoldersConsidered; }
            set { this.myIsSubFoldersConsidered = value; }
        }        

        /// <summary>
        /// Path of this instance.
        /// </summary>
        [XmlElement("path")]
        public String Path
        {
            get 
            {
                return this.myPath;
            }
            set 
            {
                String aFilePath = value;
                if ((!String.IsNullOrEmpty(this.myRelativePath)) && (!String.IsNullOrEmpty(aFilePath)))
                {
                    String aRelativeFolder = this.myRelativePath.ToLower();
                    aFilePath = aFilePath.ToLower();

                    if (aFilePath.StartsWith(aRelativeFolder))
                        aFilePath = aFilePath.Substring(aRelativeFolder.Length);
                }
                this.myPath = aFilePath; 
            }
        }

        /// <summary>
        /// Paths of this instance.
        /// </summary>
        [XmlArray("paths")]
        [XmlArrayItem("path")]
        public List<String> Paths
        {
            get
            {
                return this.myPaths;
            }
            set 
            {
                if ((this.myPaths != null) && (this.myPaths.Count > 0) && (!String.IsNullOrEmpty(this.myRelativePath)))
                {
                    List<String> someFilePaths = value;
                    String aRelativeFolder = this.myRelativePath.ToLower();
                    foreach (String aPath in this.myPaths)
                    {
                        String aFilePath = aPath.ToLower();
                        if (aFilePath.StartsWith(aRelativeFolder))
                            aFilePath = aFilePath.Substring(aRelativeFolder.Length);
                        someFilePaths.Add(aPath);
                    }

                    this.myPaths = someFilePaths;
                }
                else
                    this.myPaths = value; 
            }
        }

        /// <summary>
        /// Relative path of this instance.
        /// </summary>
        [XmlIgnore()]
        public String RelativePath
        {
            get 
            { 
                return ((this.myRelativePath != null) && (this.myRelativePath.EndsWith("\\")) ? this.myRelativePath : this.myRelativePath + "\\"); 
            }
            set  
            {
                this.myRelativePath = value;
                this.Path = this.Path;
                this.Paths = this.Paths;
            }
        }

        /// <summary>
        /// Filter of this instance.
        /// </summary>
        [XmlElement("filter")]
        public String Filter
        {
            get { return this.myFilter; }
            set { this.myFilter = value; }
        }

        /// <summary>
        /// Indicates whether the search is folder recursive.
        /// </summary>
        [XmlElement("isRecursive")]
        public Boolean IsRecursive
        {
            get { return this.myIsRecursive; }
            set { this.myIsRecursive = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        public RepositoryFile() : base(DocumentKind.RepositoryFile)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="aFilePath">The file path to consider.</param>
        public RepositoryFile(String aFilePath)
        {
            this.myPath = aFilePath;
            this.ReferenceKind = DocumentElementReferenceKind.Single;
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="aFolderPath">The folder path to consider.</param>
        /// <param name="aFileFilter">The file filter to consider.</param>
        public RepositoryFile(String aFolderPath, String aFileFilter)
        {
            this.myPath = aFolderPath;
            this.myFilter = aFileFilter;
            this.ReferenceKind = DocumentElementReferenceKind.MultipleInContainer;
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="someFilePaths">The file paths to consider.</param>
        public RepositoryFile(List<String> someFilePaths)
        {
            this.myPaths = someFilePaths;
            this.ReferenceKind = DocumentElementReferenceKind.Multiple;
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="aFilePath">The file path to consider.</param>
        /// <param name="aReferenceKind">The file kind to consider.</param>
        public RepositoryFile(String aFilePath, DocumentElementReferenceKind aReferenceKind)
        {
            this.ReferenceKind = aReferenceKind;
            switch (this.ReferenceKind)
            {
                case DocumentElementReferenceKind.Single:
                    this.myPath = aFilePath;
                    break;
                case DocumentElementReferenceKind.Multiple:
                    this.myPaths = new List<string>() { aFilePath };
                    break;
                case DocumentElementReferenceKind.MultipleInContainer:
                    if (aFilePath != null)
                        if (aFilePath.Trim().EndsWith("\\"))
                        {
                            this.myPath = aFilePath;
                            this.myFilter = "*.*";
                        }
                        else
                        {
                            this.myPath = Path.GetDirectoryName(aFilePath);
                            this.myFilter = Path.GetFileName(aFilePath);
                        }
                    break;
            }
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
            this.myPath = "";
            this.myPaths = new List<string>();
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
        public static DataElement CreateDataElement()
        {
            DataElement aDataElement = new DataElement()
            {
                CarrierKind = DocumentKind.RepositoryFile
            };
            aDataElement.SetValue(new RepositoryFile());
            return aDataElement;
        }

        /// <summary>
        /// Returns an object attribute corresponding to this isntance.
        /// </summary>
        /// <returns>An object attribute corresponding to this isntance.</returns>
        public override DataElement GetDataElement()
        {
            DataElement aDataElement = new DataElement()
            {
                CarrierKind = DocumentKind.RepositoryFile,
            };
            RepositoryFile aRepositoryFile = (RepositoryFile) this.Clone();
            aDataElement.SetValue(aRepositoryFile);
            return aDataElement;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override InformationCarrier Clone()
        {
            return new RepositoryFile()
                {
                    AvailableConnectorDefinitionUniqueNames = new List<String>(this.AvailableConnectorDefinitionUniqueNames),
                    AvailableReferenceKinds = new List<DocumentElementReferenceKind>(this.AvailableReferenceKinds),
                    ConnectionParameterStatement = this.ConnectionParameterStatement,
                    ConnectorDefinitionUniqueName = this.ConnectorDefinitionUniqueName,
                    ConnectorDefinitionSpecificationLevels = new List<SpecificationLevel>(this.ConnectorDefinitionSpecificationLevels),
                    ReferenceKind = this.ReferenceKind,
                    ReferenceSpecificationLevels = new List<SpecificationLevel>(this.ReferenceSpecificationLevels),
                    Filter = this.Filter,
                    IsRecursive = this.myIsRecursive,
                    IsSubFoldersConsidered = this.myIsSubFoldersConsidered,
                    Path = this.Path,
                    PropertyStatement = (this.PropertyStatement==null ? null : this.PropertyStatement.Clone()),
                    Paths= new List<string>(this.myPaths),                     
                    RelativePath = this.myRelativePath,
                };
        }

        /// <summary>
        /// Get the absolute path of the specified file path.
        /// </summary>
        /// <param name="aPath">The path to consider.</param>
        /// <param name="aReferencePath">The reference path to consider.</param>
        /// <returns>The path represented by this instance.</returns>
        public static String GetAbsolutePath(String aPath, String aReferencePath)
        {
            if ((!String.IsNullOrEmpty(aReferencePath)) && (aPath!=null) && (!aPath.Contains(":")))
                aPath = (aReferencePath.EndsWith("\\") ? aReferencePath : aReferencePath+ "\\") + aPath;

            return aPath;
        }

        /// <summary>
        /// Get the absolute path of the specified file path.
        /// </summary>
        /// <param name="somePaths">The paths to consider.</param>
        /// <param name="aReferencePath">The reference path to consider.</param>
        /// <returns>The path represented by this instance.</returns>
        public static List<String> GetAbsolutePath(List<String> somePaths, String aReferencePath)
        {
            List<String> someNewPaths = new List<String>();
            for (int i = 0; i < somePaths.Count; i++)
                someNewPaths.Add(RepositoryFile.GetAbsolutePath(somePaths[i], aReferencePath));

            return someNewPaths;
        }

        /// <summary>
        /// Get the path of this instance.
        /// </summary>
        /// <remarks>If this instance is a multiple file in folder then the path will end with \. If this instance is a multiple file then the first path is returned.</remarks>
        /// <returns>The path represented by this instance.</returns>
        public String GetPath()
        {
            String aPath = this.myPath;

            if (this.ReferenceKind == DocumentElementReferenceKind.MultipleInContainer)
                aPath = (
                    ((this.myPath != null) && (this.myPath.EndsWith("\\"))) ?
                    this.myPath : this.myPath + "\\");
            else if (this.ReferenceKind == DocumentElementReferenceKind.Multiple)
                aPath = (this.myPaths.Count==0 ? "" : this.myPaths[0]);

            aPath = RepositoryFile.GetAbsolutePath(aPath,this.RelativePath);

            return aPath;
        }

        /// <summary>
        /// Get the paths of this instance.
        /// </summary>
        /// <remarks>If this instance is a multiple file in folder then the path will end with \. If this instance is a multiple file then the first path is returned.</remarks>
        /// <returns>The path represented by this instance.</returns>
        public List<String> GetPaths()
        {
            List<String> somePaths = new List<String>();

            if (this.ReferenceKind == DocumentElementReferenceKind.Single)
                somePaths.Add(this.myPath);
            else if (this.ReferenceKind == DocumentElementReferenceKind.Multiple)
                somePaths = new List<string>(this.myPaths);
            else if (this.ReferenceKind == DocumentElementReferenceKind.MultipleInContainer)
                somePaths = System.IO.Directory.GetFiles(
                    this.myPath, 
                    (String.IsNullOrEmpty(this.myFilter) ? "*.*" : this.myFilter),
                    (this.IsRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)).ToList();

            somePaths = RepositoryFile.GetAbsolutePath(somePaths, this.RelativePath);

            return somePaths;
        }

        /// <summary>
        /// Gets a default title for this instance.
        /// </summary>
        public override String GetDefaultTitle()
        {
            
            String aPath = this.myPath;

            if (this.ReferenceKind == DocumentElementReferenceKind.MultipleInContainer)
            {
                aPath = (((this.myPath != null) && (this.myPath.EndsWith("\\"))) ?
                    this.myPath : this.myPath + "\\");
                aPath = Path.GetDirectoryName(aPath);
                aPath += "...";
            }
            else if (this.ReferenceKind == DocumentElementReferenceKind.Multiple)
            {
                if (this.myPaths.Count > 0)
                    aPath = Path.GetFileName(this.myPaths[0]);
                if (this.myPaths.Count>1)
                    aPath += "," + Path.GetFileName(this.myPaths[1]);
                if (this.myPaths.Count>2)
                    aPath += ",...";
            }
            else
                aPath = Path.GetFileName(aPath);

            return aPath;
        }

        #endregion


        // ------------------------------------------
        // LOAD
        // ------------------------------------------

        #region Load

        /// <summary>
        /// Loads a new instance of the RepositoryFile class from the specified parameter string.
        /// </summary>
        /// <param name="aStringValue">Parameter string to load.</param>
        public static RepositoryFile LoadFromParameterString(String aStringValue)
        {
            RepositoryFile aRepositoryFile = new RepositoryFile();

            //aRepositoryFile.DataModule = StringParser.GetParameterValue(aStringValue, "dataModule");
            aRepositoryFile.Path = ScriptParsingHelper.GetParameterValue(aStringValue, "path");

            return aRepositoryFile;
        }

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
                XmlSerializer aXmlSerializer = new XmlSerializer(typeof(RepositoryFile));
                aStringReader = new StringReader(aXmlString);
                aRepositoryFile = (RepositoryFile)aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
            }
            catch(Exception ex)
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

        /// <summary>
        /// Instantiates a new instance of RepositoryFile class from a text string.
        /// </summary>
        /// <param name="aTextString">The Text string to load.</param>
        /// /// <param name="aCharSeparator">The char separator to consider that carriage return by default.</param>
        public static RepositoryFile LoadFromTextString(String aTextString, Char aCharSeparator = '\n')
        {
            RepositoryFile aRepositoryFile = new RepositoryFile();

            if (aTextString != null)
            {
                aTextString = aTextString.Replace("\r", "").Trim();
                try
                {
                    String[] someFilePaths = aTextString.Split(aCharSeparator);
                    if (someFilePaths.Length == 1)
                        aRepositoryFile.myPath = someFilePaths[0];
                    else
                        foreach (String aCurrentPath in someFilePaths)
                            aRepositoryFile.Paths.Add(aCurrentPath);
                }
                catch
                {
                }
            }

            return aRepositoryFile;
        }

        /// <summary>
        /// Instantiates a new instance of RepositoryFile class from a text string.
        /// </summary>
        /// <param name="aTextString">The Text string to load.</param>
        public static RepositoryFile LoadFromTextString(String aTextString)
        {

            return RepositoryFile.LoadFromTextString(aTextString,'\n');
        }

        #endregion


        // ------------------------------------------
        // CHECK / REPAIR
        // ------------------------------------------

        #region Check Repair

        /// <summary>
        /// Determine whether this instance represents a or several files that exist.
        /// </summary>
        /// <returns>True if this instance represents a or several files that exist.</returns>
        public override Boolean IsExisting()
        {
            if ((!File.Exists(this.GetPath())) & (this.ReferenceKind == DocumentElementReferenceKind.Single))
                return false;
            else if ((!Directory.Exists(this.GetPath())) & (this.ReferenceKind == DocumentElementReferenceKind.MultipleInContainer))
                return false;
            else if (this.ReferenceKind == DocumentElementReferenceKind.Multiple)
            {
                Boolean aIsExists =true;
                foreach (String aPath in this.GetPaths())
                    aIsExists &= File.Exists(aPath);
                return aIsExists;
            }
            return true;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="aIsExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <returns>The check log.</returns>
        public override Log Check(Boolean aIsExistenceChecked = true)
        {
            Log aLog = base.Check(aIsExistenceChecked);

            if (this.ReferenceKind== DocumentElementReferenceKind.Multiple)
            {
                if (this.myPaths==null)
                    aLog.AddError("File path missing.", LogEventCriticality.High, "", "");
                else if (this.myPaths.Count==0)
                    aLog.AddWarning("File missing.", LogEventCriticality.High, "", "");
                if (aIsFileExistence && !this.IsExisting())
                    aLog.AddError("File path not found.", LogEventCriticality.High, "", "");
            }
            else if (this.ReferenceKind != DocumentElementReferenceKind.Referenced)
                if (String.IsNullOrEmpty(this.myPath))
                    aLog.AddError("File path missing.", LogEventCriticality.High, "", "");
                else if (aIsFileExistence && (this.ReferenceKind == DocumentElementReferenceKind.Single) && !this.IsExisting())
                    aLog.AddError("File path not found.", LogEventCriticality.High, "", "");
                else if (aIsFileExistence && (this.ReferenceKind == DocumentElementReferenceKind.MultipleInContainer) && !this.IsExisting())
                    aLog.AddError("Folder path not found.", LogEventCriticality.High, "", "");

            return aLog;
        }

        #endregion

    }
}
