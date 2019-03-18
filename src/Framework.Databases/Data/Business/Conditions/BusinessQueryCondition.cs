using BindOpen.Framework.Core.Data.Conditions;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Databases.Data.Business.Conditions
{

    /// <summary>
    /// This class represents a business condition using a business query.
    /// </summary>
    [Serializable()]
    [XmlType("BusinessQueryCondition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "BusinessQueryCondition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class BusinessQueryCondition : BusinessCondition
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DbDataQuery _Query = null;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Database data query used in the condition.
        /// </summary>
        [XmlAttribute("query")]
        public DbDataQuery Query
        {
            get { return this._Query; }
            set { this._Query = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessQueryCondition class.
        /// </summary>
        public BusinessQueryCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BusinessQueryCondition class.
        /// </summary>
        /// <param name="aTrueValue">The true value to consider.</param>
        /// <param name="query">The data query to consider.</param>
        public BusinessQueryCondition(String aTrueValue, DbDataQuery query) : base(aTrueValue)
        {
            this._Query = query;
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
            BusinessQueryCondition aBusinessQueryCondition = new BusinessQueryCondition();
            aBusinessQueryCondition.Query = this.Query.Clone() as DbDataQuery;

            return aBusinessQueryCondition;
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
        public override Boolean Evaluate(
            ScriptInterpreter scriptInterpreter,
            ScriptVariableSet scriptVariableSet)
        {
            return false;
        }

        #endregion

    }
}