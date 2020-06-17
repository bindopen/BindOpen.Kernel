using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Extensions.Carriers
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
        public override void WithPath(string path = null, string relativePath = null)
        {
            base.WithPath(path, relativePath.EndingWith(@"\"));
        }

        #endregion
    }
}
