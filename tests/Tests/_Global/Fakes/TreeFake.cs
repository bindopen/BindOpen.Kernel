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

        public IList<TreeFake> _Children { get; set; }
        public TreeFake Parent { get => _parent; set { _parent = value; } }
        public string Name { get; set; }

        public TreeFake Child(Predicate<TreeFake> filter = null, bool isRecursive = false)
        {
            foreach (var child in _Children)
            {
                if (filter == null || filter?.Invoke(this) == true)
                    return child;

                if (isRecursive)
                {
                    var subChild = child.Child(filter, true);
                    if (subChild != null) return subChild;
                }
            }

            return null;
        }

        public IEnumerable<TreeFake> Children(Predicate<TreeFake> filter = null)
            => _Children?.Where(q => filter == null || filter(q));

        public bool HasChild(Predicate<TreeFake> filter = null)
            => _Children.Any(q => filter == null || filter(q));

        public TreeFake InsertChild(Action<TreeFake> updater)
        {
            var child = new TreeFake();
            updater?.Invoke(child);

            _Children ??= new List<TreeFake>();
            _Children.Add(child);

            return this;
        }

        public void RemoveChildren(Predicate<TreeFake> filter = null)
        {
            _Children = _Children?.Where(p => filter?.Invoke(p) != true).ToList();
        }

        public string Key() => Name;

        #endregion
    }
}