using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Business.Conditions
{
    /// <summary>
    /// This class represents an advanced condition.
    /// </summary>
    [Serializable()]
    [XmlType("AdvancedBusinessCondition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "advanced.condition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class AdvancedCondition : Condition, IAdvancedCondition
    {
        // --------------------------------------------------
        // ENUMERATIONS
        // --------------------------------------------------

        #region Enumerations

        /// <summary>
        /// This enumeration lists the possible view advanced condition kinds.
        /// </summary>
        public enum AdvancedConditionKind
        {
            /// <summary>
            /// And.
            /// </summary>
            And,

            /// <summary>
            /// Or.
            /// </summary>
            Or
        }

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        [XmlElement("kind")]
        public AdvancedConditionKind Kind { get; set; } = AdvancedConditionKind.And;

        /// <summary>
        /// Conditions of this instance.
        /// </summary>
        [XmlArray("conditions")]
        [XmlArrayItem("condition")]
        public List<ICondition> Conditions { get; set; } = new List<ICondition>();

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
        /// <param name="conditions">The conditions to consider.</param>
        public AdvancedCondition(params ICondition[] conditions)
        {
            this.Conditions = conditions?.ToList();
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedCondition class.
        /// </summary>
        /// <param name="trueValue">The true value to consider.</param>
        /// <param name="conditions">The conditions to consider.</param>
        public AdvancedCondition(bool trueValue, params ICondition[] conditions)
        {
            this.TrueValue = trueValue;
            this.Conditions = conditions?.ToList();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public override object Clone()
        {
            AdvancedCondition aAdvancedBusinessCondition = new AdvancedCondition();
            aAdvancedBusinessCondition.Conditions.AddRange(this.Conditions.Select(p => p.Clone() as Condition));

            return aAdvancedBusinessCondition;
        }

        #endregion

        // ------------------------------------------
        // PROCESS
        // ------------------------------------------

        #region Process

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="scriptVariableSet">The script variable set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        public override bool Evaluate(
            IScriptInterpreter scriptInterpreter,
            IScriptVariableSet scriptVariableSet)
        {
            bool isAllConditionSatisfied = true;
            foreach (Condition condition in this.Conditions)
            {
                switch (this.Kind)
                {
                    case AdvancedConditionKind.And:
                        isAllConditionSatisfied &= condition.Evaluate(scriptInterpreter, scriptVariableSet);
                        break;
                    case AdvancedConditionKind.Or:
                        isAllConditionSatisfied |= condition.Evaluate(scriptInterpreter, scriptVariableSet);
                        break;
                    default:
                        break;
                }
            }

            return isAllConditionSatisfied == TrueValue;
        }

        #endregion
    }
}