using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Conditions;
using BindOpen.Framework.Databases.Data.Queries;

namespace BindOpen.Framework.Databases.Data.Business.Conditions
{
    /// <summary>
    /// This class represents a condition using a database query condition.
    /// </summary>
    [Serializable()]
    [XmlType("DbQueryCondition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "DbQueryCondition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DbQueryCondition : Condition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Database data query used in the condition.
        /// </summary>
        [XmlAttribute("query")]
        public IDbQuery Query { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryCondition class.
        /// </summary>
        public DbQueryCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbQueryCondition class.
        /// </summary>
        /// <param name="trueValue">The true value to consider.</param>
        /// <param name="query">The data query to consider.</param>
        public DbQueryCondition(bool trueValue, IDbQuery query) : base(trueValue)
        {
            this.Query = query;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public override object Clone()
        {
            DbQueryCondition condition = new DbQueryCondition();
            condition.Query = this.Query.Clone() as IDbQuery;

            return condition;
        }

        #endregion
    }
}