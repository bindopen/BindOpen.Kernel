using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions
{
    /// <summary>
    /// This class represents the extension configuration.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtensionConfiguration", Namespace = "https://bindopen.org/xsd")]
    public class AppExtensionConfiguration : DataItem, IAppExtensionConfiguration
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
        public List<IAppExtensionFilter> Filters { get; set; } = new List<IAppExtensionFilter>();

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
        public string DefaultFolderPath { get; set; } = null;

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
            params IAppExtensionFilter[] filters) : this(null,null,filters)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="sourceKinds">The source kinds to consider.</param>
        /// <param name="filters">The filters to consider.</param>
        public AppExtensionConfiguration(
            DataSourceKind[] sourceKinds,
            params IAppExtensionFilter[] filters) : this(sourceKinds, null, filters)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="filters">The filters to consider.</param>
        /// <param name="defaultFolderPath">The librayr folder path to consider.</param>
        /// <param name="sourceKinds">The source kinds to consider.</param>
        public AppExtensionConfiguration(
            DataSourceKind[] sourceKinds,
            string defaultFolderPath,
            params IAppExtensionFilter[] filters) : base()
        {
            Filters = filters?.ToList();
            DefaultSourceKinds = sourceKinds?.ToList();
            DefaultFolderPath = defaultFolderPath;
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
        public IAppExtensionConfiguration AddFilter(IAppExtensionFilter filter)
        {
            if (Filters != null)
            {
                if (filter != null)
                {
                    if (filter.Name != null)
                        Filters.RemoveAll(p => p.Name.KeyEquals(filter.Name));
                    if (filter.FileName != null)
                        Filters.RemoveAll(p => p.FileName.KeyEquals(filter.FileName));
                    Filters.Add(filter);
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
        public IAppExtensionConfiguration AddFilter(
            string libraryName = null
            , string libraryFileName = null
            , DataSourceKind[] sourceKinds = null
            , string libraryFolderPath = null)
        {
            return AddFilter(
                new AppExtensionFilter(libraryName, libraryFileName, sourceKinds, libraryFolderPath));
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtensionConfiguration class.
        /// </summary>
        /// <param name="configuration">The configuration to consider.</param>
        public void Merge(IAppExtensionConfiguration configuration)
        {
            if (configuration != null)
            {
                foreach (IAppExtensionFilter filter in configuration.Filters)
                {
                    AddFilter(filter);
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
        public List<IAppExtensionFilter> GetFilters()
        {
            List<IAppExtensionFilter> extensionFilters = new List<IAppExtensionFilter>();
            foreach (IAppExtensionFilter filter in Filters)
            {
                extensionFilters.Add(
                   new AppExtensionFilter(
                       filter.Name,
                       filter.FileName,
                       (filter.SourceKinds ?? DefaultSourceKinds).ToArray(),
                       filter.FolderPath ?? DefaultFolderPath));
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
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            if (Filters != null)
                Filters = Filters.GroupBy(p => new { p.Name, p.FileName }).Select(p => p.First()).ToList();

            return new Log();
        }

        #endregion
    }
}
