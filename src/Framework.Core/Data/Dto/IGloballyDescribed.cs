using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Data.Dto
{
    /// <summary>
    /// This interface represents a globally described DTO.
    /// </summary>
    public interface IGloballyDescribed
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The global description of this instance.
        /// </summary>
        IDictionaryDataItem Description
        {
            get;
            set;
        }

        #endregion
    }
}
