using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Items;
using BindOpen.Extensions;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This class represents the extension loading options.
    /// </summary>
    [XmlType("ExtensionLoadOptions", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    public class ExtensionLoadOptions : BdoItem, IExtensionLoadOptions
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The source kinds of this instance.
        /// </summary>
        public List<(DatasourceKind Kind, string Uri)> Sources { get; set; }

        /// <summary>
        /// The extension kinds of this instance.
        /// </summary>
        public List<BdoExtensionKind> ExtensionKinds { get; set; }

        /// <summary>
        /// The assmbly references of this instance.
        /// </summary>
        public List<IBdoAssemblyReference> References { get; set; }

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
