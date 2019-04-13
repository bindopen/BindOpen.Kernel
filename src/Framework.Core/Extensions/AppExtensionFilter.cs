using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;

namespace BindOpen.Framework.Core.Extensions
{
    /// <summary>
    /// This class represents the application extension filter.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtensionFilter", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public class AppExtensionFilter : DataItem, IAppExtensionFilter
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        [XmlAttribute("fileName")]
        public string FileName { get; set; } = null;

        /// <summary>
        /// The name of this instance.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = null;

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        [XmlArray("sourceKinds")]
        [XmlArrayItem("sourceKind")]
        public List<DataSourceKind> SourceKinds { get; set; } = null;

        /// <summary>
        /// The path of the folder of this instance.
        /// </summary>
        [XmlElement("folderPath")]
        public string FolderPath { get; set; } = null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppExtensionFilter class.
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
            string libraryName = null
            , string libraryFileName = null
            , DataSourceKind[] sourceKinds = null
            , string libraryFolderPath = null): this()
        {
            Name = libraryName;
            FileName = libraryFileName;
            SourceKinds = sourceKinds?.ToList() ?? new List<DataSourceKind>() { DataSourceKind.Memory, DataSourceKind.Repository };
            FolderPath = libraryFolderPath;
        }

        #endregion
    }
}
