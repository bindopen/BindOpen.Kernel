using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Libraries;

namespace BindOpen.Framework.Core.Extensions
{

    /// <summary>
    /// This class represents the extension filter.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtensionFilter", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public class AppExtensionFilter : DataItem
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private String _FileName = null;
        private String _Name = null;

        private List<DataSourceKind> _SourceKinds = null;
        private String _FolderPath = null;

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        [XmlAttribute("fileName")]
        public String FileName
        {
            get { return this._FileName; }
            set { this._FileName = value; }
        }

        /// <summary>
        /// The name of this instance.
        /// </summary>
        [XmlAttribute("name")]
        public String Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        [XmlArray("sourceKinds")]
        [XmlArrayItem("sourceKind")]
        public List<DataSourceKind> SourceKinds
        {
            get { return this._SourceKinds; }
            set { this._SourceKinds = value; }
        }

        /// <summary>
        /// The path of the folder of this instance.
        /// </summary>
        [XmlElement("folderPath")]
        public String FolderPath
        {
            get { return this._FolderPath; }
            set { this._FolderPath = value; }
        }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppExtensionFilterItem class.
        /// </summary>
        public AppExtensionFilter() : base()
        {

        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="libraryName">The library name to consider.</param>
        /// <param name="libraryFileName">The library file name to consider.</param>
        /// <param name="sourceKinds">The source kinds to consider.</param>
        /// <param name="libraryFolderPath">The librayr folder path to consider.</param>
        public AppExtensionFilter(
            String libraryName = null
            , String libraryFileName = null
            , List<DataSourceKind> sourceKinds = null
            , String libraryFolderPath = null): this()
        {
            this._Name = libraryName;
            this._FileName = libraryFileName;
            this._SourceKinds = sourceKinds ?? new List<DataSourceKind>() { DataSourceKind.Memory, DataSourceKind.Repository };
            this._FolderPath = libraryFolderPath;
        }

        ///// <summary>
        ///// Instantiates a new instance of the AppExtensionFilterItem class.
        ///// </summary>
        ///// <param name="libraryDefinitions">The library definitions to consider.</param>
        ///// <param name="sourceKinds">The source kinds to consider.</param>
        ///// <param name="libraryFolderPath">The librayr folder path to consider.</param>
        //public AppExtensionFilterItem(
        //    List<LibraryDefinition> libraryDefinitions = null
        //    , List<DataSourceKind> sourceKinds = null
        //    , String libraryFolderPath = null)
        //{
        //    this._Definitions = libraryDefinitions;
        //    this._SourceKinds = sourceKinds;
        //    this._LibraryFolderPath = libraryFolderPath;
        //}

        #endregion


        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the library definition from this instance.
        /// </summary>
        /// <returns>Gets the library definition from this instance.</returns>
        public LibraryDefinition ToDefinition()
        {
            return new LibraryDefinition()
            {
                Name = this._Name,
                AssemblyName = this._Name,
                FileName = this._FileName,
            };
        }

        #endregion

    }

}
