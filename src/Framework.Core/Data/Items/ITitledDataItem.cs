using BindOpen.Framework.Core.Data.Dto;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents an titled data item.
    /// </summary>
    public interface ITitledDataItem : INamedDataItem, IGloballyTitled
    {
        void AddTitleText(string text);
        void AddTitleText(string key, string text);
        string GetTitleText(string variantName = "*", string defaultVariantName = "*");
        void SetTitleText(string key = "*", string text = "*");
        void SetTitleText(string text);
        void Update(TitledDataItem updateBaseObject);
    }
}
