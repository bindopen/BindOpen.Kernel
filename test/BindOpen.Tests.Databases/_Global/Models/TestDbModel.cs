using BindOpen.Data.Models;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Carriers;
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
        /// <param name="builder"></param>
        public override void OnCreating(IBdoDbModelBuilder builder)
        {
            builder
                .AddTable("Employee", DbFluent.Table(nameof(DbEmployee).Substring(2), "Mdm"))
                //.AddTable<DbRegionalDirectorate>(nameof(DbRegionalDirectorate).Substring(2))
                .AddTable("RegionalDirectorate", DbFluent.Table(nameof(DbRegionalDirectorate).Substring(2), "Mdm"));

            builder
                .AddJoinCondition("Employee_RegionalDirectorate",
                    DbFluent.Eq(
                        DbFluent.Field<DbEmployee, int>(p => p.EmployeeId, Table("Employee")),
                        DbFluent.Field<DbRegionalDirectorate, int>(p => p.RegionalDirectorateId, Table("RegionalDirectorate"))));

            builder
                .AddTuple("Fields_SelectEmployee",
                    new DbField[]
                    {
                        DbFluent.FieldAsAll(Table("Employee")),
                        DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId), Table("RegionalDirectorate")),
                        DbFluent.Field(nameof(DbRegionalDirectorate.Code), Table("RegionalDirectorate"))
                    });

            builder
                .AddQuery(
                    new DbStoredQuery(
                    DbFluent.SelectQuery(Table("Employee"))
                    .From(
                        Table("Employee"),
                        DbFluent.Table(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .AddIdField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), p.UseParameter("code")))));
        }
    }
}
