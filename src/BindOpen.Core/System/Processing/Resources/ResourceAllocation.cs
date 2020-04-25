using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using System;

namespace BindOpen.System.Processing
{
    /// <summary>
    /// This class represents an resource allocation.
    /// </summary>
    public class ResourceAllocation : StoredDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The allocated resource ID of this instance.
        /// </summary>
        public string AllocatedResourceId
        {
            get;
            set;
        }

        /// <summary>
        /// The owner ID of this instance.
        /// </summary>
        public string OwnerId
        {
            get;
            set;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of ResourceAllocation class.
        /// </summary>
        public ResourceAllocation()
        {
        }

        /// <summary>
        /// Instantiates a new instance of ResourceAllocation class.
        /// </summary>
        /// <param name="aAllocatedResourceId">Allocated resource ID to consider.</param>
        /// <param name="aOwnerId">The owner ID to consider.</param>
        public ResourceAllocation(
            String aAllocatedResourceId,
            String aOwnerId = null)
        {
            this.CreationDate = DateTime.Now.ToString(DataValueType.Date);
            this.AllocatedResourceId = aAllocatedResourceId;
            this.OwnerId = aOwnerId;
        }

        #endregion
    }
}
