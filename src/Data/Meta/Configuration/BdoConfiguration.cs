using BindOpen.System.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a config.
    /// </summary>
    public partial class BdoConfiguration : BdoMetaSet, IBdoConfiguration
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        public BdoConfiguration() : base()
        {
        }

        #endregion

        // -------------------------------------------------------
        // IBdoConfiguration Implementation
        // -------------------------------------------------------

        #region IBdoConfiguration

        /// <summary>
        /// The using file paths of this instance.
        /// </summary>
        public IList<string> UsedItemIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override TChild Descendant<TChild>(
            params object[] tokens)
        {
            var token = tokens?.FirstOrDefault();

            object child = default;

            if (token?.ToString().StartsWith('/') == true)
            {
                var tokenSt = token?.ToString().ToSubstring(1);
                object tokenInt = tokenSt.ToObject(DataValueTypes.Integer);

                if (tokenInt is int index)
                {
                    child = _Children[index];
                }
                else if (token is string key)
                {
                    child = Child(q => q.BdoKeyEquals(tokenSt), false);
                }

                if (tokens?.Length == 1) return child.As<TChild>();

                if (child is IBdoSet childSet)
                {
                    tokens = tokens?.Skip(1).ToArray();
                    return childSet.Descendant<TChild>(tokens);
                }
                else
                    return default;
            }

            return base.Descendant<TChild>(tokens);
        }

        #endregion

        // ------------------------------------------
        // IIndexed Implementation
        // ------------------------------------------

        #region IIndexed

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

        #endregion

        // ------------------------------------------
        // IStorable Implementation
        // ------------------------------------------

        #region IStorable

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        public DateTime? LastModificationDate { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Title { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Description { get; set; }

        #endregion

        // ------------------------------------------
        // ITParent Implementation
        // ------------------------------------------

        #region ITParent

        public IBdoConfiguration Parent { get; set; }

        protected ITBdoSet<IBdoConfiguration> _children = null;

        public ITBdoSet<IBdoConfiguration> _Children { get => _children; set { _children = value; } }

        public IEnumerable<IBdoConfiguration> Children(Predicate<IBdoConfiguration> filter = null, bool isRecursive = false)
        {
            var children = new List<IBdoConfiguration>();

            if (_children != null)
            {
                foreach (var child in _children)
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

        public IBdoConfiguration Child(Predicate<IBdoConfiguration> filter, bool isRecursive = false)
        {
            if (_Children != null)
            {
                foreach (var child in _Children)
                {
                    if (filter?.Invoke(child) != false)
                        return child;

                    if (isRecursive)
                    {
                        var subChild = child?.Child(filter, true);
                        if (subChild != null) return subChild;
                    }
                }
            }

            return null;
        }

        public bool HasChild(Predicate<IBdoConfiguration> filter = null, bool isRecursive = false)
            => _Children?.Any(q => filter?.Invoke(q) != false || (isRecursive && q.HasChild(filter, true))) == true;

        public IBdoConfiguration InsertChild(Action<IBdoConfiguration> updater)
        {
            var child = BdoData.NewConfig();
            updater?.Invoke(child);

            child.WithParent(this);

            return child;
        }

        public void RemoveChildren(Predicate<IBdoConfiguration> filter = null, bool isRecursive = false)
        {
            _children?.Remove(filter);
        }

        #endregion
    }
}
