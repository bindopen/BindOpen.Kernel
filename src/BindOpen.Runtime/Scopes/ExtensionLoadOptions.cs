using BindOpen.Meta.Items;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This class represents the extension loading options.
    /// </summary>
    [XmlType("ExtensionLoadOptions", Namespace = "https://docs.bindopen.org/xsd")]
    public class ExtensionLoadOptions : BdoItem, IExtensionLoadOptions
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The path of the library folder of this instance.
        /// </summary>
        public string LibraryFolderPath { get; private set; }

        /// <summary>
        /// The URI of the remote server of this instance.
        /// </summary>
        public string RemoteServerUri { get; private set; }

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        public List<DatasourceKind> SourceKinds { get; private set; }

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
            LibraryFolderPath = folderPath;
            return this;
        }

        /// <summary>
        /// Sets the URI of the remote server of this instance.
        /// </summary>
        /// <param name="uri">The URI of the remote server.</param>
        /// <returns>Returns this instance.</returns>
        public IExtensionLoadOptions WithRemoteServerUri(string uri)
        {
            RemoteServerUri = uri;
            return this;
        }

        /// <summary>
        /// Sets the source kinds of this instance.
        /// </summary>
        /// <param name="sourceKinds">The source kinds to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IExtensionLoadOptions WithSourceKinds(params DatasourceKind[] sourceKinds)
        {
            SourceKinds = sourceKinds?.ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public void Update<T>(T item = default)
        {
            if (item is ExtensionLoadOptions options)
            {
                WithLibraryFolderPath(options.LibraryFolderPath);
                WithRemoteServerUri(options.RemoteServerUri);
                WithSourceKinds(options.SourceKinds?.ToArray());
            }
        }

        #endregion
    }
}
