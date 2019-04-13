using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition.Formats;

namespace BindOpen.Framework.Core.Extensions.Items.Formats
{
    /// <summary>
    /// This class represents an format.
    /// </summary>
    [Serializable()]
    [XmlType("FormatDto", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "format", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class FormatDto
        : TAppExtensionTitledItemDto<FormatDefinition>, IFormatDto
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatDto class.
        /// </summary>
        public FormatDto() : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the FormatDto class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public FormatDto(
            string definitionUniqueId,
            params IDataElement[] items)
            : base(AppExtensionItemKind.Format, definitionUniqueId, items)
        {
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone()
        {
            FormatDto dataFormat = base.Clone() as FormatDto;

            return dataFormat;
        }

        #endregion
    }
}
