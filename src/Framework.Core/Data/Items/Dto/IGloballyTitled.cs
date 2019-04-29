using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This interface represents a globally titled DTO.
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

        void AddTitle(string text);
        void AddTitle(string key, string text);
        string GetTitle(string variantName = "*", string defaultVariantName = "*");
        void SetTitle(string key = "*", string text = "*");
        void SetTitle(string text);
    }
}
