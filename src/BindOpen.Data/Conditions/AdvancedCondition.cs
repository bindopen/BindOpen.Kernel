using System.Collections.Generic;
using System.Linq;
using BindOpen.Abstractions.Data._Core.Enums;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents an advanced condition.
    /// </summary>
    public class AdvancedCondition : Condition, IAdvancedCondition
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
        public List<ICondition> Conditions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AdvancedCondition class.
        /// </summary>
        public AdvancedCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedCondition class.
        /// </summary>
        /// <param key="conditions">The conditions to consider.</param>
        public AdvancedCondition(params ICondition[] conditions)
        {
            Conditions = conditions?.ToList();
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedCondition class.
        /// </summary>
        /// <param key="trueValue">The true value to consider.</param>
        /// <param key="conditions">The conditions to consider.</param>
        public AdvancedCondition(bool trueValue, params ICondition[] conditions)
        {
            TrueValue = trueValue;
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
        public override object Clone(params string[] areas)
        {
            var condition = new AdvancedCondition();
            condition.Conditions.AddRange(Conditions.Select(p => p.Clone() as Condition));

            return condition;
        }

        #endregion
    }
}