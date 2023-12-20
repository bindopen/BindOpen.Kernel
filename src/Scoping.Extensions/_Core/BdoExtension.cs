using BindOpen.Data;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents a BindOpen extension item config.
    /// </summary>
    public abstract class BdoExtension : BdoObject, IBdoExtension
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtension class.
        /// </summary>
        protected BdoExtension() : base()
        {
            this.WithId();
        }

        #endregion

        // -----------------------------------------------
        // IBdoExtension
        // -----------------------------------------------

        #region IBdoExtension

        /// <summary>
        /// The config of this instance.
        /// </summary>
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BdoExtensionKinds ExtensionKind { get; set; }

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion
    }
}
