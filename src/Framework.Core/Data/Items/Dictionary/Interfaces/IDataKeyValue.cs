using System;

namespace BindOpen.Framework.Core.Data.Items.Dictionary
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataKeyValue : ICloneable, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        string GetTextNode(string nodeName, string indent);
    }
}