using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDictionaryDataItem : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string this[string key] { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultKey"></param>
        /// <returns></returns>
        string this[string key, string defaultKey] { get; }

        /// <summary>
        /// 
        /// </summary>
        List<DataKeyValue> Values { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> AvailableKeys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string SingleValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKeyValue"></param>
        void AddValue(IDataKeyValue dataKeyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IDataKeyValue AddValue(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        IDataKeyValue AddValue(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alternateKey"></param>
        /// <returns></returns>
        string GetContent(string key = "*", string alternateKey = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> GetDistinctKeys();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        string GetTextNode(string nodeName, string indent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HasKey(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKeyValue"></param>
        void RemoveValue(IDataKeyValue dataKeyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void RemoveValue(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        void RemoveValues(List<string> keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        void SetValue(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        void SetValue(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldKey"></param>
        /// <param name="newKey"></param>
        void UpdateKey(string oldKey, string newKey);
    }
}