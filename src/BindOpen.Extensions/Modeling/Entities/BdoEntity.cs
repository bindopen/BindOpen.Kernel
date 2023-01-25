using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a entity.
    /// </summary>
    public abstract class BdoEntity :
        TBdoExtensionItem<IBdoEntityDefinition, IBdoEntityConfiguration, IBdoEntity>,
        IBdoEntity
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntity class.
        /// </summary>
        protected BdoEntity() : base()
        {
        }

        #endregion
    }
}
