using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;

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


        // -------------------------------------------------------------
        // IBdoMetaData Implementation
        // -------------------------------------------------------------

        #region IBdoMetaData

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public override IList<object> GetDataList(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            return Items?
                .Select(q => q.GetData(scope, varSet, log))
                .ToList();
        }

        #endregion

        // -------------------------------------------------------
        // IBdoConfiguration Implementation
        // -------------------------------------------------------

        #region IBdoConfiguration

        /// <summary>
        /// 
        /// </summary>
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BdoExtensionKind DefinitionExtensionKind { get; set; }

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
