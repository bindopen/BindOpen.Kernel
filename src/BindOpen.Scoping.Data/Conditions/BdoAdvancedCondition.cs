using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping.Data.Conditions
{
    /// <summary>
    /// This class represents an advanced condition.
    /// </summary>
    public class BdoAdvancedCondition : BdoCondition, IBdoAdvancedCondition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        public AdvancedConditionKind Kind { get; set; } = AdvancedConditionKind.And;

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
        /// Instantiates a new instance of the AdvancedCondition class.
        /// </summary>
        public BdoAdvancedCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedCondition class.
        /// </summary>
        /// <param key="conditions">The conditions to consider.</param>
        public BdoAdvancedCondition(
            AdvancedConditionKind kind,
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
            var condition = new BdoAdvancedCondition();
            //condition.Conditions.AddRange(Conditions.Select(p => p.Clone() as BdoCondition));

            return condition;
        }

        #endregion
    }
}