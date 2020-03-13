using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data table.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a new instance of the DbQueryCondition class.
        /// </summary>
        /// <param name="field1">The field 1 to consider.</param>
        /// <param name="op">The operation to consider.</param>
        /// <param name="field2">The field 2 to consider.</param>
        /// <param name="parameters">The parameters to consider.</param>
        public static DbQueryCondition Condition(
            DbField field1,
            DataOperator op = DataOperator.Equal,
            DbField field2 = null,
            params DataElement[] parameters)
        {
            return new DbQueryCondition()
            {
                Field1 = field1,
                Operator = op,
                Field2 = field2,
                ParameterSet = new DataElementSet(parameters)
            };
        }

        /// <summary>
        /// Creates a new instance of the DbQueryCondition class.
        /// </summary>
        /// <param name="field1">The field 1 to consider.</param>
        /// <param name="op">The operation to consider.</param>
        /// <param name="field2">The field 2 to consider.</param>
        /// <param name="parameters">The parameters to consider.</param>
        public static DbQueryCondition Condition(
            DbField field1,
            DbField field2)
        {
            return new DbQueryCondition()
            {
                Field1 = field1,
                Operator = DataOperator.Equal,
                Field2 = field2
            };
        }
    }
}
