using BindOpen.Extensions.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents an format.
    /// </summary>
    [XmlType("Format", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "format", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class BdoFormat : TBdoExtensionItem<BdoFormatDefinition>, IBdoFormat
    {
        /// <summary>
        /// 
        /// </summary>
        new public IBdoFormatConfiguration Configuration { get => base.Configuration as IBdoFormatConfiguration; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoFormat class.
        /// </summary>
        protected BdoFormat() : base()
        {
        }

        #endregion
    }

}
