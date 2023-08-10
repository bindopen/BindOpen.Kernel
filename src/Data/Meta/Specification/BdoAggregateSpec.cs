using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public partial class BdoAggregateSpec : BdoSpec, IBdoAggregateSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoAggregateSpec class.
        /// </summary>
        public BdoAggregateSpec() : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            var dataElementSpec = base.Clone<BdoAggregateSpec>();

            return dataElementSpec;
        }

        #endregion

        // ------------------------------------------
        // ITParent Implementation
        // ------------------------------------------

        #region ITParent

        private IList<IBdoSpec> _children = null;

        public IList<IBdoSpec> _Children { get => _children; set { _children = value; } }

        public IEnumerable<IBdoSpec> Children(Predicate<IBdoSpec> filter = null)
            => _children?.Where(p => filter?.Invoke(p) == true) ?? Enumerable.Empty<IBdoSpec>();

        public IBdoSpec Child(Predicate<IBdoSpec> filter = null, bool isRecursive = false)
        {
            foreach (var child in _Children)
            {
                if (filter == null || filter?.Invoke(this) == true)
                    return child;

                if (isRecursive && child is IBdoAggregateSpec compositeChild)
                {
                    var subChild = compositeChild.Child(filter, true);
                    if (subChild != null) return subChild;
                }
            }

            return null;
        }

        public bool HasChild(Predicate<IBdoSpec> filter = null)
            => _children?.Any(p => filter?.Invoke(p) == true) ?? false;

        public IBdoSpec InsertChild(Action<IBdoSpec> updater)
        {
            var child = BdoData.NewSpec();
            updater?.Invoke(child);

            child.WithParent(this);

            return child;
        }

        public void RemoveChildren(Predicate<IBdoSpec> filter = null)
        {
            _children = _children?.Where(p => filter?.Invoke(p) != true).ToList();
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            Condition?.Dispose();
            this.

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
