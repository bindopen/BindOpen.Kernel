using BindOpen.Data;
using System;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents an indexed data item attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public abstract class MetaExtensionAttribute : Attribute,
        INamed,
        ITitled, IDescribed
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of this instance.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of this instance.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The creation date of this instance.
        /// </summary>
        public string CreationDate { get; set; }

        /// <summary>
        /// The last modification date of this instance.
        /// </summary>
        public string LastModificationDate { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetaExtensionAttribute class.
        /// </summary>
        protected MetaExtensionAttribute() : base()
        {
        }

        #endregion
    }
}
