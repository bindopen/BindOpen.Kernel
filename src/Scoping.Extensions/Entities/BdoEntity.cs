using BindOpen.System.Data;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This class represents a entity.
    /// </summary>
    public abstract class BdoEntity : BdoExtension, IBdoEntity
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
            this.WithDefinition(BdoExtensionKind.Entity);
        }

        #endregion
    }
}
