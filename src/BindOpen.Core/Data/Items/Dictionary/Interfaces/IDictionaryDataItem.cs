using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDictionaryDataItem : IIdentifiedDataItem
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
        /// <param name="dataKeyValue"></param>
        IDictionaryDataItem Add(params IDataKeyValue[] dataKeyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IDictionaryDataItem Add(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        IDictionaryDataItem Add(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        IDictionaryDataItem Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        IDictionaryDataItem Remove(params string[] keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKeyValue"></param>
        IDictionaryDataItem Set(params IDataKeyValue[] dataKeyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        IDictionaryDataItem Set(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        IDictionaryDataItem Set(string key, string text);

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
    }
}