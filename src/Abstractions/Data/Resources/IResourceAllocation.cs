namespace BindOpen.System.Data.Resources
{
    /// <summary>
    /// This class represents an resource allocation.
    /// </summary>
    public interface IResourceAllocation : IIdentified
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The allocated resource ID of this instance.
        /// </summary>
        string AllocatedResourceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        IResourceAllocation WithAllocatedResourceId(string id);

        /// <summary>
        /// The owner ID of this instance.
        /// </summary>
        string OwnerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        IResourceAllocation WithOwnerId(string id);

        #endregion
    }
}
