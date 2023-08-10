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
            this.WithId();
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

        public IEnumerable<IBdoConfiguration> Children(Predicate<IBdoConfiguration> filter = null)
            => _children?.Where(p => filter?.Invoke(p) == true) ?? Enumerable.Empty<IBdoConfiguration>();

        public IBdoConfiguration Child(Predicate<IBdoConfiguration> filter = null, bool isRecursive = false)
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

        public bool HasChild(Predicate<IBdoConfiguration> filter = null)
            => _children?.Any(p => filter?.Invoke(p) == true) ?? false;

        public IBdoConfiguration InsertChild(Action<IBdoConfiguration> updater)
        {
            var child = BdoData.NewConfig();
            updater?.Invoke(child);

            child.WithParent(this);

            return child;
        }

        public void RemoveChildren(Predicate<IBdoConfiguration> filter = null)
        {
            _children = _children?.Where(p => filter?.Invoke(p) != true).ToList();
        }

        #endregion
    }
}
