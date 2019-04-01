using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Data.Dto
{
    /// <summary>
    /// This interface represents a globally titled DTO.
    /// </summary>
    public interface IGloballyTitled
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The global title of this instance.
        /// </summary>
        IDictionaryDataItem Title
        {
            get;
            set;
        }

        #endregion
    }
}
