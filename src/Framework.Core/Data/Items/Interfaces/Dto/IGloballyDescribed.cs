using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This interface represents a globally described DTO.
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

        void AddDescription(string text);
        void AddDescription(string key, string text);
        string GetDescription(string variantName = "*", string defaultVariantName = "*");
        void SetDescription(string key = "*", string text = "*");
        void SetDescription(string text);
    }
}
