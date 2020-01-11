using BindOpen.Framework.Extensions.Definition;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Extensions.Runtime
{
    /// <summary>
    /// This class represents an format.
    /// </summary>
    [Serializable()]
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
        /// Instantiates a new instance of the Format class.
        /// </summary>
        protected BdoFormat() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Format class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected BdoFormat(IBdoFormatConfiguration dto)
        {
        }

        #endregion
    }

}
