using BindOpen.Data.Items;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This interface represents a globally titled data.
    /// </summary>
    public interface IGloballyTitled
    {
        /// <summary>
        /// The global title of this instance.
        /// </summary>
        DictionaryDataItem Title
        {
            get;
            set;
        }
    }
}
