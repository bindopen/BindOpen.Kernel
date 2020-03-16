using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Models;
using BindOpen.Data.Queries;
using BindOpen.Tests.Databases.Entities;

namespace BindOpen.Tests.Databases.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public partial class TestDbModel : BdoDbModel
    {
        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee1(string code)
        {
            var query = DbFluent.DeleteQuery(Table("Employee"))
                .AddIdField(DbFluent.FieldAsLiteral<DbEmployee, string>(p => p.Code, code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee2(string code)
        {
            var query = DbFluent.DeleteQuery(Table("Employee"))
                .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)))
                .WithParameters(
                    ElementFactory.Create("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee3(string code)
        {
            var query = DbFluent.DeleteQuery(Table("Employee"))
                .Where(q => DataExpressionFactory.CreateAuto("{{" + string.Format("$sqlEq($sqlField('Code'), {0})", q.UseParameter("code", DataValueType.Text).AsScript()) + "}}"))
                .WithParameters(
                    ElementFactory.Create("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee4(string code)
        {
            var query = DbFluent.DeleteQuery(Table("Employee"))
                .Where(q => DbFluent.Eq(DbFluent.Field("code"), q.UseParameter("code", DataValueType.Text).AsScript()))
                .WithParameters(
                    ElementFactory.Create("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee5(string code)
        {
            var query = DbFluent.DeleteQuery(Table("Employee"))
                .Where(q => DbFluent.Eq(DbFluent.Field("code"), code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee6(string code)
        {
            var query = DbFluent.DeleteQuery(Table("Employee"))
                .From(
                    DbFluent.Table(DbQueryJoinKind.Left, Table("RegionalDirectorate").WithAlias("directorate"))
                        .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                .Where(q => DbFluent.Eq(DbFluent.Field("code"), code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee7(string code)
        {
            var query = DbFluent.DeleteQuery(Table("Employee"))
                .From(
                    DbFluent.Table(DbQueryJoinKind.Left, Table("RegionalDirectorate").WithAlias("directorate"))
                        .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                .Where(q => DataExpressionFactory.CreateAuto("{{" + string.Format("$sqlEq($sqlField('Code'), {0})", q.UseParameter("code", DataValueType.Text).AsScript()) + "}}"))
                .WithParameters(
                    ElementFactory.Create("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee8(string code)
        {
            var query = DbFluent.DeleteQuery(Table("Employee"))
                .From(
                    DbFluent.Table(DbQueryJoinKind.Left, Table("RegionalDirectorate").WithAlias("directorate"))
                        .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                .Where(q => DataExpressionFactory.CreateLiteral(@"""Code""='codeC'"))
                .WithParameters(
                    ElementFactory.Create("code", code));

            return query;
        }
    }
}
