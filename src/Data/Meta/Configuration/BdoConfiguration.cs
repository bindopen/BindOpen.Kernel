using System;
using System.Collections.Generic;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a config.
    /// </summary>
    public partial class BdoConfiguration : BdoMetaSet, IBdoConfiguration
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        public BdoConfiguration() : base()
        {
            this.WithId();
        }

        #endregion

        // -------------------------------------------------------
        // IBdoConfiguration Implementation
        // -------------------------------------------------------

        #region IBdoConfiguration

        /// <summary>
        /// The using file paths of this instance.
        /// </summary>
        public IList<string> UsedItemIds { get; set; }

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
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Title { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Description { get; set; }

        #endregion
    }
}
