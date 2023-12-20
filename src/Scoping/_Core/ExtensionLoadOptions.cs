using BindOpen.Data;
using BindOpen.Data.Assemblies;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents the extension loading options.
    /// </summary>
    [XmlType("ExtensionLoadOptions", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public class ExtensionLoadOptions : BdoObject, IExtensionLoadOptions
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        public IList<(DatasourceKind Kind, string Uri)> Sources { get; set; }

        /// <summary>
        /// The extension kinds of this instance.
        /// </summary>
        public IList<BdoExtensionKinds> ExtensionKinds { get; set; }

        /// <summary>
        /// The assmbly references of this instance.
        /// </summary>
        public IList<IBdoAssemblyReference> References { get; set; }

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
    }
}
