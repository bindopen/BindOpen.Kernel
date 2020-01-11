using BindOpen.Framework.Data.Helpers.Strings;
using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Runtime.Extensions.Carriers
{
    /// <summary>
    /// This class represents a repository item.
    /// </summary>
    public abstract class RepositoryItem : BdoCarrier
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryItem class.
        /// </summary>
        protected RepositoryItem() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryItem class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected RepositoryItem(IBdoCarrierConfiguration dto) : base(dto)
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the path of this instance.
        /// </summary>
        /// <param name="path">The new path to consider. Null to update the existing one.</param>
        /// <param name="relativePath">The new relative path to consider. Null to keep the existing one.</param>
        /// <returns>Returns True if this instance exists. False otherwise.</returns>
        public override void SetPath(string path = null, string relativePath = null)
        {
            base.SetPath(path, relativePath.GetEndedString(@"\"));
        }

        #endregion
    }
}
