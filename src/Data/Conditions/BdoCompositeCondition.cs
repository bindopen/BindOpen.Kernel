using System;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents an advanced condition.
    /// </summary>
    public class BdoCompositeCondition : BdoCondition, IBdoCompositeCondition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        public BdoCompositeConditionKind CompositionKind { get; set; } = BdoCompositeConditionKind.And;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CompositeCondition class.
        /// </summary>
        public BdoCompositeCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CompositeCondition class.
        /// </summary>
        /// <param key="conditions">The conditions to consider.</param>
        public BdoCompositeCondition(
            BdoCompositeConditionKind kind,
            params IBdoCondition[] conditions)
        {
            _children = new TBdoSet<IBdoCondition>(conditions);
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        public override object Clone()
        {
            var condition = new BdoCompositeCondition();
            //condition.Conditions.AddRange(Conditions.Select(p => p.Clone() as BdoCondition));

            return condition;
        }

        #endregion

        // ------------------------------------------
        // ITParent Implementation
        // ------------------------------------------

        #region ITParent

        protected ITBdoSet<IBdoCondition> _children = null;

        public ITBdoSet<IBdoCondition> _Children { get => _children; set { _children = value; } }

        public Q InsertChild<Q>(Action<Q> updater) where Q : IBdoCondition, new()
        {
            var child = BdoData.New(updater);
            child.WithParent<IBdoCondition, IBdoCompositeCondition>(this);

            return child;
        }

        public void RemoveChildren(Predicate<IBdoCondition> filter = null, bool isRecursive = false)
        {
            _children?.Remove(filter);
        }

        #endregion
    }
}