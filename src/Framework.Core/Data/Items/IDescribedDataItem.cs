using BindOpen.Framework.Core.Data.Dto;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public interface IDescribedDataItem : ITitledDataItem, IGloballyDescribed
    {
        void AddDescription(string text);
        void AddDescription(string key, string text);
        string GetDescription(string variantName = "*", string defaultVariantName = "*");
        void SetDescription(string key = "*", string text = "*");
        void SetDescription(string text);
        void Update(DescribedDataItem updateBaseObject);
        bool DescriptionSpecified { get; }
    }
}
