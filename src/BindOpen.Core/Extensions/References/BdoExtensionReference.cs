using BindOpen.System.Assemblies.References;
using System.Xml.Serialization;

namespace BindOpen.Extensions.References
{
    /// <summary>
    /// This class represents the BindOpen extension reference.
    /// </summary>
    [XmlType("BdoExtensionReference", Namespace = "https://docs.bindopen.org/xsd")]
    public class BdoExtensionReference : BdoAssemblyReference, IBdoExtensionReference
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionReference class.
        /// </summary>
        public BdoExtensionReference() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionReference class.
        /// </summary>
        /// <param name="name">The library name to consider.</param>
        /// <param name="version">The library version to consider.</param>
        public BdoExtensionReference(
            string name,
            string version = null) : base(name, version)
        {
        }

        #endregion
    }
}
