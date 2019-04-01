using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Configuration
{
    public interface ITAppExtensionTitledItemConfiguration<T> : ITAppExtensionItemConfiguration<T>
        where T : IAppExtensionItemDefinition
    {
        IDictionaryDataItem Description { get; set; }
        IDictionaryDataItem Title { get; set; }

        void AddDescriptionText(string text);
        void AddDescriptionText(string key, string text);
        void AddTitleText(string text);
        void AddTitleText(string key, string text);
        string GetDescriptionText(string variantName = "*", string defaultVariantName = "*");
        string GetTitleText(string variantName = "*", string defaultVariantName = "*");
        void SetDescriptionText(string key = "*", string text = "*");
        void SetDescriptionText(string text);
        void SetTitleText(string key = "*", string text = "*");
        void SetTitleText(string text);
    }
}