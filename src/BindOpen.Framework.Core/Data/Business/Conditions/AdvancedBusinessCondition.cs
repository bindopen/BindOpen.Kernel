using BindOpen.Framework.Core.System.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Conditions
{

    /// <summary>
    /// This class represents an advanced business condition.
    /// </summary>
    [Serializable()]
    [XmlType("AdvancedBusinessCondition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "advancedBusinessCondition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class AdvancedBusinessCondition : BusinessCondition
    {

        // --------------------------------------------------
        // ENUMERATIONS
        // --------------------------------------------------

        #region Enumerations

        /// <summary>
        /// This enumeration lists the possible view advanced business condition kinds.
        /// </summary>
        public enum AdvancedBusinessConditonKind
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
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<BusinessCondition> _BusinessConditions = new List<BusinessCondition>();
        private AdvancedBusinessConditonKind _Kind = AdvancedBusinessConditonKind.And;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Business conditions of this instance.
        /// </summary>
        [XmlArray("businessConditions")]
        [XmlArrayItem("businessCondition")]
        public List<BusinessCondition> BusinessConditions
        {
            get { return this._BusinessConditions; }
            set { this._BusinessConditions = value; }
        }

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        [XmlElement("kind")]
        public AdvancedBusinessConditonKind Kind
        {
            get { return this._Kind; }
            set { this._Kind = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AdvancedBusinessConditon class.
        /// </summary>
        public AdvancedBusinessCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedBusinessConditon class.
        /// </summary>
        /// <param name="businessConditions">The business conditions to consider.</param>
        public AdvancedBusinessCondition(List<BusinessCondition> businessConditions)
        {
            this._BusinessConditions = businessConditions;
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public override Object Clone()
        {
            AdvancedBusinessCondition aAdvancedBusinessCondition = new AdvancedBusinessCondition();
            aAdvancedBusinessCondition.BusinessConditions.AddRange(this._BusinessConditions.Select(p => p.Clone() as BusinessCondition));

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
        public override Boolean Evaluate(
            ScriptInterpreter scriptInterpreter,
            ScriptVariableSet scriptVariableSet)
        {
            Boolean isAllConditionSatisfied = true;
            foreach (BusinessCondition aBusinessCondition in this._BusinessConditions)
                switch (this._Kind)
                {
                    case AdvancedBusinessConditonKind.And:
                        isAllConditionSatisfied &= aBusinessCondition.Evaluate(scriptInterpreter, scriptVariableSet);
                        break;
                    case AdvancedBusinessConditonKind.Or:
                        isAllConditionSatisfied |= aBusinessCondition.Evaluate(scriptInterpreter, scriptVariableSet);
                        break;
                }
            return isAllConditionSatisfied;
        }

        #endregion


    }
}