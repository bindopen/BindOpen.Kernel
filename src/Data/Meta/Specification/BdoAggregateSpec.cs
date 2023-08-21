using BindOpen.System.Data.Helpers;
using System;
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
            var spec = base.Clone().As<BdoAggregateSpec>();

            spec._children = BdoData.NewSet(_children?.Select(q => q.Clone<IBdoSpec>()).ToArray());

            return spec;
        }

        #endregion

        // ------------------------------------------
        // ITParent Implementation
        // ------------------------------------------

        #region ITParent

        protected ITBdoSet<IBdoSpec> _children = null;

        public ITBdoSet<IBdoSpec> _Children { get => _children; set { _children = value; } }

        public IBdoSpec InsertChild(Action<IBdoSpec> updater)
        {
            var child = BdoData.NewSpec();
            updater?.Invoke(child);

            child.WithParent(this);

            return child;
        }

        public void RemoveChildren(Predicate<IBdoSpec> filter = null, bool isRecursive = false)
        {
            _children?.Remove(filter);

            if (isRecursive && _children?.Any() == true)
            {
                foreach (var child in _children)
                {
                    if (child is IBdoAggregateSpec aggregateChild)
                    {
                        aggregateChild.RemoveChildren(filter, true);
                    }
                }
            }
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
