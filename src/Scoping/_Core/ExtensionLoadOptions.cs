using BindOpen.Data;
using BindOpen.Data.Assemblies;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents the options for loading extensions within the BindOpen kernel.
/// </summary>
/// <remarks>
/// It specifies which sources, assembly references, and kinds of extensions 
/// should be considered during the discovery process.
/// </remarks>
[XmlType("ExtensionLoadOptions", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public class ExtensionLoadOptions : BdoObject, IExtensionLoadOptions
{
    // --------------------------------------------------
    // PROPERTIES
    // --------------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the list of data source locations to scan.
    /// </summary>
    /// <value>A list of tuples containing the datasource kind and its URI.</value>
    [XmlIgnore]
    public IList<(DatasourceKind Kind, string Uri)> Sources { get; set; } = new List<(DatasourceKind Kind, string Uri)>();

    /// <summary>
    /// Gets or sets the specific extension kinds to filter during loading.
    /// </summary>
    /// <value>A list of <see cref="BdoExtensionKinds"/> (e.g., Connector, Task, Function).</value>
    [XmlElement("extensionKind")]
    public IList<BdoExtensionKinds> ExtensionKinds { get; set; } = new List<BdoExtensionKinds>();

    /// <summary>
    /// Gets or sets the assembly references to be loaded and scanned.
    /// </summary>
    /// <value>A list of <see cref="IBdoAssemblyReference"/>.</value>
    [XmlElement("reference")]
    public IList<IBdoAssemblyReference> References { get; set; } = new List<IBdoAssemblyReference>();

    #endregion

    // --------------------------------------------------
    // CONSTRUCTORS
    // --------------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtensionLoadOptions"/> class.
    /// </summary>
    public ExtensionLoadOptions() : base()
    {
    }

    #endregion
}