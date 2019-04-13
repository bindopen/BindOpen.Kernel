using BindOpen.Framework.Core.Extensions.Definition.Carriers;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers
{
    /// <summary>
    /// This class represents a carrier.
    /// </summary>
    public abstract class Carrier : TAppExtensionItem<ICarrierDefinition>, ICarrier
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        new public ICarrierDto Dto { get; }

        private string _relativePath = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The relative path of this instance.
        /// </summary>
        public string RelativePath
        {
            get
            {
                return this._relativePath;
            }
            set
            {
                this.SetPath(null, value);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Carrier class.
        /// </summary>
        protected Carrier() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Carrier class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected Carrier(ICarrierDto dto)
        {
        }

        #endregion

        //// ------------------------------------------
        //// MUTATORS
        //// ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the path of this instance.
        /// </summary>
        /// <param name="path">The new path to consider. Null to update the existing one.</param>
        /// <param name="relativePath">The new relative path to consider. Null to keep the existing one.</param>
        /// <returns>Returns True if this instance exists. False otherwise.</returns>
        public virtual void SetPath(string path = null, string relativePath = null)
        {
            if (Dto == null) return;

            string absolutePath = (path ?? Dto?.Path);

            if (!string.IsNullOrEmpty(relativePath))
                this._relativePath = relativePath;

            if ((!string.IsNullOrEmpty(this._relativePath)) && (!string.IsNullOrEmpty(absolutePath)))
            {
                string aRelativeFolder = this._relativePath.ToLower();
                absolutePath = absolutePath.ToLower();

                if (absolutePath.StartsWith(aRelativeFolder))
                    absolutePath = absolutePath.Substring(aRelativeFolder.Length);
            }

            Dto.Path = absolutePath;
        }

        #endregion
    }
}
