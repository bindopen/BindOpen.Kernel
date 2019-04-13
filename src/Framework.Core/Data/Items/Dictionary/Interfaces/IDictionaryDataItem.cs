using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Items.Dictionary
{
    public interface IDictionaryDataItem : IDataItem
    {
        string this[string key] { get; set; }
        string this[string key, string defaultKey] { get; }

        List<DataKeyValue> Values { get; set; }
        bool ValuesSpecified { get; }

        List<string> AvailableKeys { get; set; }
        bool AvailableKeysSpecified { get; }
        string SingleValue { get; set; }
        bool SingleValueSpecified { get; }

        void AddValue(IDataKeyValue dataKeyValue);
        IDataKeyValue AddValue(string text);
        IDataKeyValue AddValue(string key, string text);
        void Clear();

        string GetContent(string key = "*", string alternateKey = null);
        List<string> GetDistinctKeys();
        string GetTextNode(string nodeName, string indent);
        bool HasKey(string key);
        void RemoveValue(IDataKeyValue dataKeyValue);
        void RemoveValue(string key);
        void RemoveValues(List<string> keys);
        void SetValue(string text);
        void SetValue(string key, string text);
        void UpdateKey(string oldKey, string newKey);
    }
}