using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITreeNode<T>
    {
        T Parent { get; set; }

        IList<T> Children { get; set; }
    }
}