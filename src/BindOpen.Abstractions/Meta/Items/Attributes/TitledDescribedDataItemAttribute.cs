using System;

namespace BindOpen.Meta.Items
{
    /// <summary>
    /// This class represents an indexed data item attribute.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public abstract class TitledDescribedDataItemAttribute : DescribedDataItemAttribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The title of this instance.
        /// </summary>
        public string Title { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TitledDescribedDataItemAttribute class.
        /// </summary>
        protected TitledDescribedDataItemAttribute() : base()
        {
        }

        #endregion
    }
}
