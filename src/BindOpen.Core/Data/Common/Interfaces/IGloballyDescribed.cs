using BindOpen.Data.Items;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This interface represents a globally described data.
    /// </summary>
    public interface IGloballyDescribed
    {
        /// <summary>
        /// The global description of this instance.
        /// </summary>
        DictionaryDataItem Description
        {
            get;
            set;
        }
    }
}
