using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Items.Formats.Definition;

namespace BindOpen.Framework.Core.Extensions.Items.Formats
{
    /// <summary>
    /// This class represents an format.
    /// </summary>
    [Serializable()]
    [XmlType("Format", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "format", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class Format : TAppExtensionItem<FormatDefinition>, IFormat
    {
        /// <summary>
        /// 
        /// </summary>
        new public IFormatConfiguration Configuration { get => base.Configuration as IFormatConfiguration; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Format class.
        /// </summary>
        protected Format() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Format class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected Format(IFormatConfiguration dto)
        {
        }

        #endregion
    }

}
