using BindOpen.Data;
using BindOpen.Data.Items;
using System;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents the definition of BindOpen extension item.
    /// </summary>
    public abstract class BdoExtensionDefinition : BdoItem, IBdoExtensionDefinition
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
        /// Instantiates a new instance of the BdoExtensionDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="preffix">The preffix to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        protected BdoExtensionDefinition(
            string name,
            string preffix,
            IBdoPackageDefinition extensionDefinition)
            : base()
        {
            preffix = _preffix;
            this.WithName(name);
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
        public IBdoPackageDefinition PackageDefinition { get; private set; }

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

