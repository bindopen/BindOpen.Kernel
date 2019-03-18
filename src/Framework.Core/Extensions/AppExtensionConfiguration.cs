using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions
{
    /// <summary>
    /// This class represents the extension configuration.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtensionConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public class AppExtensionConfiguration : DataItem
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The filters of this instance.
        /// </summary>
        [XmlArray("filters")]
        [XmlArrayItem("add")]
        public List<AppExtensionFilter> Filters { get; set; } = new List<AppExtensionFilter>();

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        [XmlArray("sourceKinds")]
        [XmlArrayItem("add")]
        public List<DataSourceKind> DefaultSourceKinds { get; set; } = null;

        /// <summary>
        /// The path of the folder of this instance.
        /// </summary>
        [XmlElement("defaultFolderPath")]
        public String DefaultFolderPath { get; set; } = null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        public AppExtensionConfiguration() : base()
        {

        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="filters">The filters to consider.</param>
        public AppExtensionConfiguration(
            params AppExtensionFilter[] filters) : this(null,null,filters)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="sourceKinds">The source kinds to consider.</param>
        /// <param name="filters">The filters to consider.</param>
        public AppExtensionConfiguration(
            List<DataSourceKind> sourceKinds,
            params AppExtensionFilter[] filters) : this(sourceKinds, null, filters)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="filters">The filters to consider.</param>
        /// <param name="sourceKinds">The source kinds to consider.</param>
        /// <param name="libraryFolderPath">The librayr folder path to consider.</param>
        public AppExtensionConfiguration(            
            List<DataSourceKind> sourceKinds,
            String libraryFolderPath,
            params AppExtensionFilter[] filters) : this()
        {
            this.Filters = filters?.ToList();
            this.DefaultSourceKinds = sourceKinds;
            this.DefaultFolderPath = libraryFolderPath;
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="defaultSourceKinds">The source kinds to consider.</param>
        /// <param name="defaultLibraryFolderPath">The librayr folder path to consider.</param>
        public AppExtensionConfiguration(
            List<DataSourceKind> defaultSourceKinds = null,
            String defaultLibraryFolderPath = null) : this()
        {
            //if ((libraryNames != null) || (libraryFileNames != null))
            //{
            //    this._Filters = new List<AppExtensionFilter>();
            //    if (libraryNames != null)
            //        this._Filters.AddRange(libraryNames.Select(p => new AppExtensionFilter(p)).ToList());
            //    if (libraryFileNames != null)
            //        this._Filters.AddRange(libraryFileNames.Select(p => new AppExtensionFilter(null, p)).ToList());
            //}
            this.DefaultSourceKinds = defaultSourceKinds;
            this.DefaultFolderPath = defaultLibraryFolderPath;
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified fileter.
        /// </summary>
        /// <param name="filter">The filter to consider.</param>
        public AppExtensionConfiguration AddExtensionFilter(AppExtensionFilter filter)
        {
            if (this.Filters != null)
            {
                if (filter != null)
                {
                    if (filter.Name != null)
                        this.Filters.RemoveAll(p => p.Name.KeyEquals(filter.Name));
                    if (filter.FileName != null)
                        this.Filters.RemoveAll(p => p.FileName.KeyEquals(filter.FileName));
                    this.Filters.Add(filter);
                }
            }

            return this;
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="libraryName">The library name to consider.</param>
        /// <param name="libraryFileName">The library file name to consider.</param>
        /// <param name="sourceKinds">The source kinds to consider.</param>
        /// <param name="libraryFolderPath">The librayr folder path to consider.</param>
        public AppExtensionConfiguration AddExtension(
            String libraryName = null
            , String libraryFileName = null
            , List<DataSourceKind> sourceKinds = null
            , String libraryFolderPath = null)
        {
            return this.AddExtensionFilter(new AppExtensionFilter(libraryName, libraryFileName, sourceKinds, libraryFolderPath));
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="configuration">The configuration to consider.</param>
        public void Merge(AppExtensionConfiguration configuration)
        {
            if (configuration != null)
            {
                foreach (AppExtensionFilter filter in configuration.Filters)
                {
                    this.AddExtensionFilter(filter);
                }
            }
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the filters of this instance
        /// </summary>
        /// <returns></returns>
        public List<AppExtensionFilter> GetFilters()
        {
            List<AppExtensionFilter> extensionFilters = new List<AppExtensionFilter>();
            foreach (AppExtensionFilter filter in this.Filters)
            {
                extensionFilters.Add(
                   new AppExtensionFilter(
                       filter.Name,
                       filter.FileName,
                       filter.SourceKinds ?? this.DefaultSourceKinds,
                       filter.FolderPath ?? this.DefaultFolderPath));
            }

            return extensionFilters;
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            if (this.Filters != null)
                this.Filters = this.Filters.GroupBy(p => new { p.Name, p.FileName }).Select(p => p.First()).ToList();

            return new Log();
        }

        #endregion
    }

}
