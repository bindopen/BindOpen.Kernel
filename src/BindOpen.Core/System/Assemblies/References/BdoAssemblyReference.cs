using BindOpen.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.System.Assemblies.References
{
    /// <summary>
    /// This class represents the BindOpen library reference.
    /// </summary>
    [XmlType("BdoAssemblyReference", Namespace = "https://bindopen.org/xsd")]
    public class BdoAssemblyReference : DataItem, IBdoAssemblyReference
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The library name of this instance.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = null;

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; } = null;

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        [XmlAttribute("fileName")]
        public string FileName { get; set; } = null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoAssemblyReference class.
        /// </summary>
        public BdoAssemblyReference() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoAssemblyReference class.
        /// </summary>
        /// <param name="name">The library name to consider.</param>
        /// <param name="version">The library version to consider.</param>
        public BdoAssemblyReference(
            string name,
            string version = null) : this()
        {
            Name = name;
            Version = version;
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified file name.
        /// </summary>
        /// <param name="fileName">The file name to consider.</param>
        public IBdoAssemblyReference WithFileName(string fileName)
        {
            FileName = fileName;
            return this;
        }

        #endregion
    }
}
