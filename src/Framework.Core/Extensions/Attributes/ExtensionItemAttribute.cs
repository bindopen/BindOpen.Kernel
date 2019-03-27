using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents an indexed data item attribute.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class ExtensionItemAttribute : Attribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        public string Id { get; set; } = null;

        /// <summary>
        /// The title of this instance.
        /// </summary>
        public string Title { get; set; } = null;

        /// <summary>
        /// The description of this instance.
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// The creation date of this instance.
        /// </summary>
        public string CreationDate { get; set; } = null;

        /// <summary>
        /// The last modification date of this instance.
        /// </summary>
        public string LastModificationDate { get; set; } = null;

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int Index { get; set; } = -1;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ExtensionItemAttribute class.
        /// </summary>
        public ExtensionItemAttribute() : base()
        {
        }

        #endregion
    }
}
