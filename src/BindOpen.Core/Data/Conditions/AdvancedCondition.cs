using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents an advanced condition.
    /// </summary>
    [XmlType("AdvancedBusinessCondition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "advanced.condition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        /// <param name="areas">The areas to consider.</param>
        public override object Clone(params string[] areas)
        {
            AdvancedCondition aAdvancedBusinessCondition = new AdvancedCondition();
            aAdvancedBusinessCondition.Conditions.AddRange(this.Conditions.Select(p => p.Clone() as Condition));

            return aAdvancedBusinessCondition;
        }

        #endregion
    }
}