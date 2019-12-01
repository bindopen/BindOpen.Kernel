using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Runtime.Stores
{
    /// <summary>
    /// This class represents the extension loading configuration.
    /// </summary>
    [Serializable()]
    [XmlType("ExtensionLoadOptions", Namespace = "https://bindopen.org/xsd")]
    public class ExtensionLoadOptions : DataItem, IExtensionLoadOptions
    {
        string _libraryFolderPath;
        string _remoteServerUri;
        List<DatasourceKind> _sourceKinds;

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The path of the library folder of this instance.
        /// </summary>
        [XmlElement("libraryFolderPath")]
        public string LibraryFolderPath => _libraryFolderPath;

        /// <summary>
        /// The URI of the remote server of this instance.
        /// </summary>
        [XmlElement("remoteServerUri")]
        public string RemoteServerUri => _remoteServerUri;

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        [XmlArray("sourceKinds")]
        [XmlArrayItem("add")]
        public List<DatasourceKind> SourceKinds => _sourceKinds;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ExtensionLoadOptions class.
        /// </summary>
        public ExtensionLoadOptions() : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the path of the library folder of this instance.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IExtensionLoadOptions WithLibraryFolderPath(string folderPath)
        {
            _libraryFolderPath = folderPath;
            return this;
        }

        /// <summary>
        /// Sets the URI of the remote server of this instance.
        /// </summary>
        /// <param name="uri">The URI of the remote server.</param>
        /// <returns>Returns this instance.</returns>
        public IExtensionLoadOptions WithRemoteServerUri(string uri)
        {
            _remoteServerUri = uri;
            return this;
        }

        /// <summary>
        /// Sets the source kinds of this instance.
        /// </summary>
        /// <param name="sourceKinds">The source kinds to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IExtensionLoadOptions WithSourceKinds(params DatasourceKind[] sourceKinds)
        {
            _sourceKinds = sourceKinds?.ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="specificationAreas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        public override IBdoLog Update<T>(T item = default, string[] specificationAreas = null, UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is ExtensionLoadOptions options)
            {
                WithLibraryFolderPath(options.LibraryFolderPath);
                WithRemoteServerUri(options.RemoteServerUri);
                WithSourceKinds(options.SourceKinds?.ToArray());
            }

            return log;
        }

        #endregion
    }
}
