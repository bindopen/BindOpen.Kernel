using BindOpen.Framework.Extensions.Attributes;
using BindOpen.Framework.System.Diagnostics;

namespace BindOpen.Framework.Runtime.Extensions.Carriers
{
    /// <summary>
    /// This class represents a repository folder.
    /// </summary>
    [BdoCarrier(Name = "runtime$folder")]
    public class RepositoryFolder : RepositoryItem
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryFolder class.
        /// </summary>
        public RepositoryFolder() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFolder class.
        /// </summary>
        /// <param name="path">The path of the instance.</param>
        public RepositoryFolder(string path) : base()
        {
            this.Path = path;
        }

        #endregion

        // ------------------------------------------
        // CHECK, UPDATE, REPAIR
        // ------------------------------------------

        #region Check Repair

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public override IBdoLog Check<T>(
            bool isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null)
        {
            IBdoLog log = base.Check<T>(isExistenceChecked);

            if (string.IsNullOrEmpty(this.Path))
                log.AddError("Folder path missing");

            return log;
        }

        #endregion
    }
}

