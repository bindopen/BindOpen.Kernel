using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents an format configuration.
    /// </summary>
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
        /// Instantiates a new instance of the BdoFormatConfiguration class.
        /// </summary>
        public BdoFormatConfiguration() : base(BdoExtensionItemKind.Format, null)
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoFormatConfiguration Add(params IDataElement[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoFormatConfiguration WithItems(params IDataElement[] items)
        {
            base.WithItems(items);
            return this;
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
        public override object Clone(params string[] areas)
        {
            BdoFormatConfiguration dataFormat = base.Clone(areas) as BdoFormatConfiguration;

            return dataFormat;
        }

        #endregion
    }
}
