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
            object child = null;

            if (token?.ToString().StartsWith('/') == true)
            {
                var tokenSt = token?.ToString().ToSubstring(1);
                object tokenInt = tokenSt.ToObject(DataValueTypes.Integer);

                if (tokenInt is int index)
                {
                    child = _Children.GetAt(index);
                }
                else if (token is string key)
                {
                    child = Child(q => q.BdoKeyEquals(tokenSt));
                }

                if (child is IBdoSet childSet)
                {
                    tokens = tokens?.Skip(1).ToArray();
                    child = childSet?.Descendant<TChild>(tokens);
                }

                return (child ?? this) as TChild;
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

        private IList<IBdoConfiguration> _children = null;

        public IList<IBdoConfiguration> _Children { get => _children; set { _children = value; } }

        public IEnumerable<IBdoConfiguration> Children(Predicate<IBdoConfiguration> filter = null, bool isRecursive = false)
            => _children?.Where(p => filter?.Invoke(p) != false) ?? Enumerable.Empty<IBdoConfiguration>();

        public IBdoConfiguration Child(Predicate<IBdoConfiguration> filter = null, bool isRecursive = false)
        {
            foreach (var child in _Children)
            {
                if (filter == null || filter?.Invoke(child) == true)
                    return child;

                if (isRecursive)
                {
                    var subChild = child.Child(filter, true);
                    if (subChild != null) return subChild;
                }
            }

            return null;
        }

        public bool HasChild(Predicate<IBdoConfiguration> filter = null, bool isRecursive = false)
            => _children?.Any(p => filter?.Invoke(p) == true) ?? false;

        public IBdoConfiguration InsertChild(Action<IBdoConfiguration> updater)
        {
            var child = BdoData.NewConfig();
            updater?.Invoke(child);

            child.WithParent(this);

            return child;
        }

        public void RemoveChildren(Predicate<IBdoConfiguration> filter = null, bool isRecursive = false)
        {
            _children = _children?.Where(p => filter?.Invoke(p) != true).ToList();
        }

        #endregion
    }
}
