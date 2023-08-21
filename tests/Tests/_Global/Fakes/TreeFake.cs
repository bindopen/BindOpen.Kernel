using BindOpen.System.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Tests
{
    /// <summary>
    /// This class represents a fake class.
    /// </summary>
    public class TreeFake : INamed, ITTreeNode<TreeFake>
    {
        private TreeFake _parent;

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        public ITBdoSet<TreeFake> _Children { get; set; }
        public TreeFake Parent { get => _parent; set { _parent = value; } }
        public string Name { get; set; }

        public IEnumerable<TreeFake> Children(Predicate<TreeFake> filter = null, bool isRecursive = false)
        {
            var children = new List<TreeFake>();

            if (_Children != null)
            {
                foreach (var child in _Children)
                {
                    if (filter?.Invoke(child) != false)
                        children.Add(child);

                    if (isRecursive)
                    {
                        children.AddRange(child.Children(filter, isRecursive));
                    }
                }
            }

            return children;
        }

        public TreeFake Child(Predicate<TreeFake> filter, bool isRecursive = false)
        {
            if (_Children != null)
            {
                foreach (var child in _Children)
                {
                    if (filter?.Invoke(child) != false)
                        return child;

                    if (isRecursive)
                    {
                        var subChild = child.Child(filter, true);
                        if (subChild != null) return subChild;
                    }
                }
            }

            return null;
        }

        public bool HasChild(Predicate<TreeFake> filter = null, bool isRecursive = false)
            => _Children?.Any(q => filter?.Invoke(q) != false || (isRecursive && q.HasChild(filter, isRecursive))) == true;

        public TreeFake InsertChild(Action<TreeFake> updater)
        {
            var child = new TreeFake();
            updater?.Invoke(child);

            _Children ??= BdoData.NewSet<TreeFake>();
            _Children.Add(child);

            return child;
        }

        public void RemoveChildren(Predicate<TreeFake> filter = null, bool isRecursive = false)
        {
            _Children?.Remove(filter);
        }

        public string Key() => Name;

        #endregion
    }
}