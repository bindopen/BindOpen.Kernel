using System;

namespace BindOpen.Framework.Core.Data.Items.Dictionary
{
    public interface IDataKeyValue : ICloneable, IDisposable
    {
        string Content { get; set; }

        string Key { get; set; }

        string GetTextNode(string nodeName, string indent);
    }
}