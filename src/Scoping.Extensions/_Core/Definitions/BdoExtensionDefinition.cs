using BindOpen.System.Data.Meta;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This class represents the definition of BindOpen extension item.
    /// </summary>
    public abstract class BdoExtensionDefinition : BdoDefinition,
        IBdoExtensionDefinition
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="preffix">The preffix to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        protected BdoExtensionDefinition(
            string name,
            string preffix,
            IBdoPackageDefinition extensionDefinition)
            : base(name, preffix)
        {
            PackageDefinition = extensionDefinition;
        }

        #endregion

        // --------------------------------------------------
        // IBdoExtensionDefinition Implementation
        // --------------------------------------------------

        #region IBdoExtensionDefinition

        /// <summary>
        /// The library of this instance.
        /// </summary>        
        public IBdoPackageDefinition PackageDefinition { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueName { get => PackageDefinition?.Name + "$" + Name; }

        /// <summary>
        /// The identifier of the group of this instance.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Uri of the image representing this instance.
        /// </summary>
        public string ImageUri { get; set; }

        /// <summary>
        /// Indicates whether this instance is editable.
        /// </summary>
        public bool IsEditable { get; set; } = true;

        /// <summary>
        /// Indicates whether this instance is indexed.
        /// </summary>
        public bool IsIndexed { get; set; } = false;

        /// <summary>
        /// Business library ID of this instance.
        /// </summary>
        public string LibraryId { get; set; }

        #endregion
    }
}

