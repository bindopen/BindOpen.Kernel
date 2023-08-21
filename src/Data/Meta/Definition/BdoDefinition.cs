using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a config.
    /// </summary>
    public partial class BdoDefinition : BdoSpecSet, IBdoDefinition
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Varibales

        string _preffix = "def_";

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDefinition class.
        /// </summary>
        public BdoDefinition() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoDefinition class.
        /// </summary>
        public BdoDefinition(
            string name,
            string preffix) : base()
        {
            _preffix = preffix;
            this.WithName(name);
        }

        #endregion

        // -------------------------------------------------------
        // IBdoDefinition Implementation
        // -------------------------------------------------------

        #region IBdoDefinition

        /// <summary>
        /// The using file paths of this instance.
        /// </summary>
        public IList<string> UsedItemIds { get; set; }

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
        // IIndexed Implementation
        // ------------------------------------------

        #region IIndexed

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

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

        public IBdoDefinition Parent { get; set; }

        protected ITBdoSet<IBdoDefinition> _children = null;

        public ITBdoSet<IBdoDefinition> _Children { get => _children; set { _children = value; } }

        public IEnumerable<IBdoDefinition> Children(Predicate<IBdoDefinition> filter = null, bool isRecursive = false)
        {
            var children = new List<IBdoDefinition>();

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

        public IBdoDefinition Child(Predicate<IBdoDefinition> filter = null, bool isRecursive = false)
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

        public bool HasChild(Predicate<IBdoDefinition> filter = null, bool isRecursive = false)
            => _Children?.Any(q => filter?.Invoke(q) != false || (isRecursive && q.HasChild(filter, isRecursive))) == true;

        public IBdoDefinition InsertChild(Action<IBdoDefinition> updater)
        {
            var child = BdoData.NewDefinition();
            updater?.Invoke(child);

            child.WithParent(this);

            return child;
        }

        public void RemoveChildren(Predicate<IBdoDefinition> filter = null, bool isRecursive = false)
        {
            _children?.Remove(filter);
        }

        #endregion
    }
}
