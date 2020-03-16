using BindOpen.Data.Common;
using BindOpen.Data.Elements;
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
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode1(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode1", p =>
                DbFluent.SelectQuery(Table("Employee"))
                    .From(
                        Table("Employee"),
                        DbFluent.Table(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee2"))
                    .WithLimit(100)
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))))
                .WithParameters(
                    ElementFactory.Create("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode2(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode2", p =>
                DbFluent.SelectQuery(Table("Employee"))
                    .From(
                        DbFluent.Table(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee2"))
                    .WithLimit(100)
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))))
                .WithParameters(
                    ElementFactory.Create("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode3(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode3", p =>
                DbFluent.SelectQuery(null)
                    .From(
                        Table("Employee"),
                        DbFluent.Table(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee2"))
                    .WithLimit(100)
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))))
                .WithParameters(
                    ElementFactory.Create("code", code));
        }
    }
}
