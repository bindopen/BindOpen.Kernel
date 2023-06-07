using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Assemblies;
using BindOpen.Scoping.Extensions;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Scopes
{
    /// <summary>
    /// This class represents the extension loading options.
    /// </summary>
    [XmlType("ExtensionLoadOptions", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
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
        public IList<BdoExtensionKind> ExtensionKinds { get; set; }

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
