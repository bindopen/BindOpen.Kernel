using BindOpen.Data;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents an resource allocation.
    /// </summary>
    public interface IResourceAllocation : ITIdentifiedPoco<IResourceAllocation>
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
        /// <param name="id"></param>
        IResourceAllocation WithAllocatedResourceId(string id);

        /// <summary>
        /// The owner ID of this instance.
        /// </summary>
        string OwnerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        IResourceAllocation WithOwnerId(string id);

        #endregion
    }
}
