using BindOpen.Data.Meta;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a entity configuration.
    /// </summary>
    public class BdoEntityConfiguration
        : TBdoExtensionTitledItemConfiguration<IBdoEntityDefinition>,
        IBdoEntityConfiguration
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityConfiguration class.
        /// </summary>
        public BdoEntityConfiguration() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoEntityConfiguration class.
        /// </summary>
        public BdoEntityConfiguration(string definitionUniqueId)
            : base(BdoExtensionItemKind.Entity, definitionUniqueId)
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
        public new IBdoEntityConfiguration Add(params IBdoMetaData[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new IBdoEntityConfiguration WithItems(params IBdoMetaData[] items)
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
            BdoEntityConfiguration configuration = base.Clone(areas) as BdoEntityConfiguration;
            return configuration;
        }

        #endregion
    }
}
