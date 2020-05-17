using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents an entity configuration.
    /// </summary>
    [XmlType("BdoEntityConfiguration", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "entity", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoEntityConfiguration
        : TBdoExtensionTitledItemConfiguration<BdoEntityDefinition>, IBdoEntityConfiguration
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The schema of this instance. 
        /// </summary>
        [XmlIgnore()]
        public DataSchema Schema { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityConfiguration class.
        /// </summary>
        public BdoEntityConfiguration() : base(BdoExtensionItemKind.Entity, null)
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
        public new IBdoEntityConfiguration Add(params IDataElement[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoEntityConfiguration WithItems(params IDataElement[] items)
        {
            base.WithItems(items);
            return this;
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone(params string[] areas)
        {
            BdoEntityConfiguration configuration = base.Clone(areas) as BdoEntityConfiguration;
            if (Schema != null)
                configuration.Schema = Schema.Clone() as DataSchema;

            return configuration;
        }

        #endregion
    }
}