using System.Collections.Generic;

namespace BindOpen.Framework.Core.Data.Items.Dictionary
{
    public interface IDictionaryDataItem : IDataItem
    {
        string this[string key] { get; set; }
        string this[string key, string defaultKey] { get; }

        List<string> AvailableKeys { get; set; }
        bool AvailableKeysSpecified { get; }
        string SingleValue { get; set; }
        bool SingleValueSpecified { get; }
        List<DataKeyValue> Values { get; set; }
        bool ValuesSpecified { get; }

        void AddValue(DataKeyValue dataKeyValue);
        DataKeyValue AddValue(string text);
        DataKeyValue AddValue(string key, string text);
        void Clear();
        object Clone();
        string GetContent(string key = "*", string alternateKey = null);
        List<string> GetDistinctKeys();
        string GetTextNode(string nodeName, string indent);
        bool HasKey(string key);
        void RemoveValue(DataKeyValue dataKeyValue);
        void RemoveValue(string key);
        void RemoveValues(List<string> keys);
        void SetValue(string text);
        void SetValue(string key, string text);
        void UpdateKey(string oldKey, string newKey);
    }
}