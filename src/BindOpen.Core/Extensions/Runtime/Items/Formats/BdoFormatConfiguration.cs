using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using System;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents an format configuration.
    /// </summary>
    [Serializable()]
    [XmlType("BdoFormatConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "format", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoFormatConfiguration
        : TBdoExtensionTitledItemConfiguration<BdoFormatDefinition>, IBdoFormatConfiguration
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatConfiguration class.
        /// </summary>
        public BdoFormatConfiguration() : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the FormatConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public BdoFormatConfiguration(
            string definitionUniqueId,
            params IDataElement[] items)
            : base(BdoExtensionItemKind.Format, definitionUniqueId, items)
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
            BdoFormatConfiguration dataFormat = base.Clone() as BdoFormatConfiguration;

            return dataFormat;
        }

        #endregion
    }
}
