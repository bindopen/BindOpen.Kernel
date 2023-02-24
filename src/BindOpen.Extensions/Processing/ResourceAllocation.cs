using BindOpen.Data.Items;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents an resource allocation.
    /// </summary>
    public class ResourceAllocation : BdoItem, IResourceAllocation
    {
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
        /// <param key="allocatedResourceId">Allocated resource ID to consider.</param>
        /// <param key="ownerId">The owner ID to consider.</param>
        public ResourceAllocation(
            string allocatedResourceId,
            string ownerId = null)
        {
            AllocatedResourceId = allocatedResourceId;
            OwnerId = ownerId;
        }

        #endregion


        // ------------------------------------------
        // IResourceAllocation Implementation
        // ------------------------------------------

        #region IResourceAllocation

        /// <summary>
        /// The allocated resource ID of this instance.
        /// </summary>
        public string AllocatedResourceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        /// <returns></returns>
        public IResourceAllocation WithAllocatedResourceId(string id)
        {
            AllocatedResourceId = id;
            return this;
        }

        /// <summary>
        /// The owner ID of this instance.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        /// <returns></returns>
        public IResourceAllocation WithOwnerId(string id)
        {
            OwnerId = id;
            return this;
        }

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
    }
}
