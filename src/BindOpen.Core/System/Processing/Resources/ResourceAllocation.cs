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
        /// <param name="allocatedResourceId">Allocated resource ID to consider.</param>
        /// <param name="ownerId">The owner ID to consider.</param>
        public ResourceAllocation(
            string allocatedResourceId,
            string ownerId = null)
        {
            CreationDate = DateTime.Now.ToString(DataValueTypes.Date);
            AllocatedResourceId = allocatedResourceId;
            OwnerId = ownerId;
        }

        #endregion
    }
}
