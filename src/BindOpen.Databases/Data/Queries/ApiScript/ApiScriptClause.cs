using BindOpen.Data.Common;
using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a Api script clause.
    /// </summary>
    public class ApiScriptClause : ApiScriptField
    {
        /// <summary>
        /// The operators of this instance.
        /// </summary>
        public List<DataOperator> Operators
        {
            get;
            set;
        } = new List<DataOperator>();

        /// <summary>
        /// The value definition of this instance.
        /// </summary>
        public ApiScriptFilteringDefinition ValueDefinition
        {
            get;
            set;
        } = null;

        /// <summary>
        /// Creates a new instance of the ApiScriptClause class.
        /// </summary>
        public ApiScriptClause()
        {
        }

        /// <summary>
        /// Creates a new instance of the ApiScriptClause class.
        /// </summary>
        /// <param name="fieldAlias">The field alias to consider.</param>
        /// <param name="field">The field to consider.</param>
        /// <param name="operators">The operators to consider.</param>
        public ApiScriptClause(
            string fieldAlias,
            DbField field,
            params DataOperator[] operators)
        {
            this.FieldAlias = fieldAlias;
            this.Field = field;
            this.Operators = operators?.ToList();
        }

        /// <summary>
        /// Creates a new instance of the DbQueryScriptItem class.
        /// </summary>
        /// <param name="fieldAlias">The field alias to consider.</param>
        /// <param name="field">The field to consider.</param>
        /// <param name="aOperator">The operator to consider.</param>
        /// <param name="valueFilteringDefinition">The value filter definition to consider.</param>
        public ApiScriptClause(
            string fieldAlias,
            DbField field,
            DataOperator aOperator,
            ApiScriptFilteringDefinition valueFilteringDefinition)
        {
            this.FieldAlias = fieldAlias;
            this.Field = field;
            this.Operators = new List<DataOperator>() { aOperator };
            this.ValueDefinition = valueFilteringDefinition;
        }
    }
}
