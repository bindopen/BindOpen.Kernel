using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Models;
using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.Extensions.Carriers;
using BindOpen.Framework.System.Diagnostics;

namespace BindOpen.Framework.Samples.SampleA.Services.Databases
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public partial class MyDbModel : BdoDbModel, IMyDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void OnCreating(IBdoDbModelBuilder builder)
        {
            builder
                .AddTable(DbFluent.Table(nameof(DbEmployee).Substring(2), "Mdm"), nameof(DbEmployee).Substring(2))
                .AddTable(DbFluent.Table(nameof(DbRegionalDirectorate).Substring(2), "Mdm"), nameof(DbRegionalDirectorate).Substring(2));

            builder
                .AddJoinCondition(DbFluent.JoinCondition(
                    DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId), Table("Employee")),
                    DbFluent.Field(nameof(DbEmployee.EmployeeId), Table("Employee")),
                    DataOperator.Equal), "Employee_RegionalDirectorate");

            builder
                .AddTuple(new DbField[]
                    {
                        DbFluent.FieldAsAll(Table("Employee")),
                        DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId), Table("RegionalDirectorate")),
                        DbFluent.Field(nameof(DbRegionalDirectorate.Code), Table("RegionalDirectorate"))
                    }, "Fields_SelectEmployee");

            builder
                .AddQuery(
                    new DbStoredQuery(
                    DbFluent.SelectBasic(Table("Employee"))
                    .From(DbFluent.From(Table("Employee"))
                        .Join(DbFluent.Join(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate"))))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .AddIdField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), p.UseParameter("code")))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="q"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageToken"></param>
        /// <returns></returns>
        internal IDbQuery ListEmployees(string q, string orderBy, int? pageSize = null, int? pageToken = null,
            IBdoLog log = null, string filterQuery = null, string orderByQuery = null)
            => DbFluent.SelectAdvanced("GetMyTables", DbFluent.Table())
                .AsDistinct()
                .WithFields(
                    DbFluent.FieldAsAll("table"),
                    DbFluent.Field("Field1", "table"),
                    DbFluent.Field("Field2", "table"))
                .From(
                    DbFluent.From(DbFluent.Table(nameof(DbMyTable).Substring(2), "schema1").WithAlias("table"))
                        .Join(
                            DbFluent.Join(DbQueryJoinKind.Left, DbFluent.Table("DbTable1".Substring(2), "schema2").WithAlias("table1"))
                                .WithCondition(
                                    DbFluent.And(
                                        DbFluent.JoinCondition(
                                            DbFluent.Field("table1key", "table1"),
                                            DbFluent.Field(nameof(DbMyTable.ExecutionStatusReferenceId), "table")),
                                        DbFluent.JoinCondition(
                                            DbFluent.Field("table2key", "table2"),
                                            DbFluent.Field(nameof(DbMyTable.ExecutionStatusReferenceId), "table")))))
                        .Join(
                            DbFluent.Join(DbQueryJoinKind.Left, DbFluent.Table("DbTable1".Substring(2), "schema2").WithAlias("table2"))
                                .WithCondition(
                                    DbFluent.JoinCondition(
                                        DbFluent.Field("table1key", "table2"),
                                        DbFluent.Field("Field1", "table"))))
                )
                .Filter(
                    filterQuery,
                    log,
                    new ApiScriptFilteringDefinition(
                        new ApiScriptClause("startCreationDate", DbFluent.Field("CreationDate", "table")),
                        new ApiScriptClause("endCreationDate", DbFluent.Field("CreationDate", "table")),
                        new ApiScriptClause("name", DbFluent.Field("Name", "table"), DataOperator.Equal)
                    //new ApiScriptClause("table", null, DataOperator.,
                    //    new ApiScriptFilteringDefinition(
                    //        new ApiScriptClause("CreationDate", DbFieldFactory.Create("CreationDate", "MyTable", nameof(DbSchemas.Iam), null), DataOperator.GreaterOrEqual)))
                    ))
                .Sort(
                    orderByQuery,
                    log,
                    new ApiScriptSortingDefinition(
                        new ApiScriptField("CreationDate", DbFluent.Field("CreationDate", "table"))
                        , new ApiScriptField("Id", DbFluent.Field("Name", "table"))
                        , new ApiScriptField("LastModificationDate", DbFluent.Field("LastModificationDate", "table"))));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery GetEmployeeWithCode(string code)
        {
            return this.UseQuery("GetEmployeeWithCode",
                p =>
                {
                    return DbFluent.SelectBasic(Table("Employee"))
                        .From(
                            DbFluent.From(Table("Employee"))
                            .Join(
                                DbFluent.Join(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                                    .WithCondition(JoinCondition("Employee_RegionalDirectorate"))))
                        .WithFields(Tuple("Fields_SelectEmployee"))
                        .AddIdField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), p.UseParameter("code", code)));
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery InsertEmployee(EmployeeDto employee)
        {
            return DbFluent.InsertBasic(Table("Employee"), true)
                .From(DbFluent.From(Table("Employee"))
                    .Join(
                        DbFluent.Join(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate"))))
                //.WithFields(p => Fields_InsertEmployee(p, employee))
                ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isPartialUpdate"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpdateEmployee(string code, bool isPartialUpdate, EmployeeDto employee)
        {
            return this.UseQuery("UpdateEmployee",
                p =>
                {
                    var query = DbFluent.UpdateBasic(Table("Employee"))
                        .From(DbFluent.From()
                            .Join(
                                DbFluent.Join(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                                    .WithCondition(JoinCondition("Employee_RegionalDirectorate"))));

                    if (!isPartialUpdate || employee?.Code?.Length > 0)
                    {
                        query.AddField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), p.UseParameter("code", nameof(DbEmployee.Code))));
                    }
                    if (!isPartialUpdate || employee?.ContactEmail?.Length > 0)
                    {
                        query.AddField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.ContactEmail), p.UseParameter("contactEmail", nameof(DbEmployee.ContactEmail))));
                    }
                    if (!isPartialUpdate || employee?.FisrtName?.Length > 0)
                    {
                        query.AddField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), p.UseParameter("fisrtName", nameof(DbEmployee.FisrtName))));
                    }
                    if (!isPartialUpdate || employee?.LastName?.Length > 0)
                    {
                        query.AddField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.LastName), p.UseParameter("lastName", nameof(DbEmployee.LastName))));
                    }
                    //if (!isPartialUpdate || employee?.RegionalDirectorateId?.Length > 0)
                    //{
                    //    query.AddField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.RegionalDirectorateId), p.UseParameter("regionalDirectorateId", nameof(DbEmployee.RegionalDirectorateId))));
                    //}
                    if (!isPartialUpdate || employee?.StaffNumber?.Length > 0)
                    {
                        query.AddField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.StaffNumber), p.UseParameter("staffNumber", nameof(DbEmployee.StaffNumber))));
                    }

                    return query;
                });
        }

        /// <summary>
        /// Delete the specified  employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee(string code)
        {
            var query = DbFluent.InsertBasic(Table("Employee"), true)
                .AddIdField(DbFluent.Field(code));

            return query;
        }
    }
}
