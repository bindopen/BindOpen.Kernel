using BindOpen.Data;
using BindOpen.Data.Items;
using System;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents the definition of BindOpen extension item.
    /// </summary>
    public abstract class BdoExtensionItemDefinition : BdoItem, IBdoExtensionItemDefinition
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Varibales

        string _preffix = "item_";

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionItemDefinition class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="preffix">The preffix to consider.</param>
        /// <param name="extensionDefinition">The extensition definition to consider.</param>
        protected BdoExtensionItemDefinition(
            string name,
            string preffix,
            IBdoExtensionDefinition extensionDefinition)
            : base()
        {
            preffix = _preffix;
            this.WithName(name);
            ExtensionDefinition = extensionDefinition;
        }

        #endregion

        // --------------------------------------------------
        // IBdoExtensionItemDefinition Implementation
        // --------------------------------------------------

        #region IBdoExtensionItemDefinition

        /// <summary>
        /// The library of this instance.
        /// </summary>        
        public IBdoExtensionDefinition ExtensionDefinition { get; private set; }

        /// <summary>
        /// The unique identifier of this instance.
        /// </summary>
        public string UniqueId { get; set; }

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

        // ------------------------------------------
        // IStorable Implementation
        // ------------------------------------------

        #region IStorable

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        public DateTime? LastModificationDate { get; set; }

        #endregion

        // ------------------------------------------
        // IIndexed Implementation
        // ------------------------------------------

        #region IIndexed

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public virtual string Key() => Name;

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

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Description { get; set; }

        #endregion
    }
}

