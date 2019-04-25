using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definitions.Formats;

namespace BindOpen.Framework.Core.Extensions.Items.Formats
{
    /// <summary>
    /// This class represents an format configuration.
    /// </summary>
    [Serializable()]
    [XmlType("FormatConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "format", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class FormatConfiguration
        : TAppExtensionTitledItemConfiguration<FormatDefinition>, IFormatConfiguration
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatConfiguration class.
        /// </summary>
        public FormatConfiguration() : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the FormatConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public FormatConfiguration(
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
            FormatConfiguration dataFormat = base.Clone() as FormatConfiguration;

            return dataFormat;
        }

        #endregion
    }
}
