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
    [XmlType("DbDataQueryCondition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "DbDataQueryCondition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DbDataQueryCondition : Condition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Database data query used in the condition.
        /// </summary>
        [XmlAttribute("query")]
        public IDbDataQuery Query { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbDataQueryCondition class.
        /// </summary>
        public DbDataQueryCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbDataQueryCondition class.
        /// </summary>
        /// <param name="trueValue">The true value to consider.</param>
        /// <param name="query">The data query to consider.</param>
        public DbDataQueryCondition(bool trueValue, IDbDataQuery query) : base(trueValue)
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
            DbDataQueryCondition condition = new DbDataQueryCondition();
            condition.Query = this.Query.Clone() as IDbDataQuery;

            return condition;
        }

        #endregion
    }
}