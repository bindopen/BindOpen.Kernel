using BindOpen.Data;
using System;
using System.Linq;

namespace BindOpen.Kernel.Tests
{
    /// <summary>
    /// This class represents a fake class.
    /// </summary>
    public class TreeFake : INamed, ITTreeNode<TreeFake>
    {
        private TreeFake _parent;

        public TreeFake()
        {
        }

        public ITBdoSet<TreeFake> _Children { get; set; }
        public TreeFake Parent { get => _parent; set { _parent = value; } }
        public string Name { get; set; }

        public void RemoveChildren(Predicate<TreeFake> filter = null, bool isRecursive = false)
        {
            _Children?.Remove(filter);

            if (isRecursive && _Children?.Any() == true)
            {
                foreach (var child in _Children)
                {
                    child.RemoveChildren(filter, true);
                }
            }
        }

        public string Key() => Name;

        public Q InsertChild<Q>(Action<Q> updater) where Q : TreeFake, new()
        {
            var child = new Q();
            updater?.Invoke(child);

            _Children ??= BdoData.NewItemSet<TreeFake>();
            _Children.Insert(child);

            return child;
        }
    }
}