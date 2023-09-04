using BindOpen.System.Data;
using System;
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

        public TreeFake InsertChild(Action<TreeFake> updater)
        {
            var child = new TreeFake();
            updater?.Invoke(child);

            _Children ??= BdoData.NewItemSet<TreeFake>();
            _Children.Add(child);

            return child;
        }

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

        #endregion
    }
}