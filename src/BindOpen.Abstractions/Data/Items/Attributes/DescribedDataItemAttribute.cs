using System;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents an indexed data item attribute.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public abstract class DescribedDataItemAttribute : Attribute, INamed, IIdentified
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
        /// The ID of this instance.
        /// </summary>
        public string Id { get; set; }

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
        /// Instantiates a new instance of the DescribedDataItemAttribute class.
        /// </summary>
        protected DescribedDataItemAttribute() : base()
        {
        }

        #endregion
    }
}
