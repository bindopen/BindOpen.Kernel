using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data.Conditions
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
        public CompositeConditionKind Kind { get; set; } = CompositeConditionKind.And;

        /// <summary>
        /// Conditions of this instance.
        /// </summary>
        public IList<IBdoCondition> Conditions { get; set; }

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
            CompositeConditionKind kind,
            params IBdoCondition[] conditions)
        {
            Conditions = conditions?.ToList();
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
    }
}