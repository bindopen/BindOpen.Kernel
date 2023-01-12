using BindOpen.Meta.Elements;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a carrier configuration.
    /// </summary>
    public class BdoCarrierConfiguration
        : TBdoExtensionTitledItemConfiguration<IBdoCarrierDefinition>,
        IBdoCarrierConfiguration
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoCarrierConfiguration class.
        /// </summary>
        public BdoCarrierConfiguration() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoCarrierConfiguration class.
        /// </summary>
        public BdoCarrierConfiguration(string definitionUniqueId)
            : base(BdoExtensionItemKind.Carrier, definitionUniqueId)
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
        public new IBdoCarrierConfiguration Add(params IBdoMetaElement[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoCarrierConfiguration WithItems(params IBdoMetaElement[] items)
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
        /// <returns>Returns a clone of this instance.</returns>
        public override object Clone(params string[] areas)
        {
            BdoCarrierConfiguration configuration = base.Clone(areas) as BdoCarrierConfiguration;
            return configuration;
        }

        #endregion
    }
}
