using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Business.Conditions
{

    /// <summary>
    /// This class represents an basic condition.
    /// </summary>
    [Serializable()]
    [XmlType("BasicBusinessCondition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "basic.condition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class BasicCondition : Condition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The arugment 1 of this instance.
        /// </summary>
        [XmlElement("arg1")]
        public object Argument1 { get; set; }

        /// <summary>
        /// The operator of this instance.
        /// </summary>
        [XmlElement("operator")]
        public ConditionOperator Operator { get; set; }

        /// <summary>
        /// The arugment 2 of this instance.
        /// </summary>
        [XmlElement("arg2")]
        public object Argument2 { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BasicBusinessCondition class.
        /// </summary>
        public BasicCondition()
        {
        }


        /// <summary>
        /// Instantiates a new instance of the BasicBusinessCondition class.
        /// </summary>
        /// <param name="trueValue">The value that expresses that the condition is satisfied.</param>
        public BasicCondition(
            string trueValue) : base(trueValue)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BasicBusinessCondition class.
        /// </summary>
        /// <param name="arg1">The argument 1 to consider.</param>
        /// <param name="ope">The operator to consider.</param>
        /// <param name="arg2">The argument 2 to consider.</param>
        public BasicCondition(string arg1, ConditionOperator ope, string arg2 = null)
        {
            this.Argument1= arg1;
            this.Argument2 = arg2;
            this.Operator = ope;
        }

        /// <summary>
        /// Instantiates a new instance of the BasicBusinessCondition class.
        /// </summary>
        /// <param name="trueValue">The value that expresses that the condition is satisfied.</param>
        /// <param name="arg1">The argument 1 to consider.</param>
        /// <param name="ope">The operator to consider.</param>
        /// <param name="arg2">The argument 2 to consider.</param>
        public BasicCondition(
            string trueValue,
            string arg1,
            ConditionOperator ope,
            string arg2 = null) : base(trueValue)
        {
            this.Argument1 = arg1;
            this.Argument2 = arg2;
            this.Operator = ope;
        }

        #endregion
    }
}