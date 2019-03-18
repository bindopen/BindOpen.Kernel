using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Conditions
{
    /// <summary>
    /// This class represents a business condition.
    /// </summary>
    [Serializable()]
    [XmlType("BusinessConditon", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "businessConditon", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    //[XmlInclude(typeof(BusinessQueryCondition))]
    [XmlInclude(typeof(BusinessScriptCondition))]
    [XmlInclude(typeof(AdvancedBusinessCondition))]
    public abstract class BusinessCondition : DataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Value that expresses that the condition is satisfied.
        /// </summary>
        [XmlElement("trueValue")]
        public String TrueValue { get; set; } = "";

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessCondition class.
        /// </summary>
        protected BusinessCondition() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BusinessCondition class.
        /// </summary>
        /// <param name="trueValue">The true value to consider.</param>
        protected BusinessCondition(String trueValue) : base()
        {
            this.TrueValue= trueValue;
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
        /// <returns>True if the business script value is the true value.</returns>
        public virtual Boolean Evaluate(
            ScriptInterpreter scriptInterpreter,
            ScriptVariableSet scriptVariableSet)
        {
            return false;
        }

        #endregion
    }
}