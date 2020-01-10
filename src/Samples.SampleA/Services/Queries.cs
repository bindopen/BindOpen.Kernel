using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Data.Queries.ApiScript;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    [DbQueryDepot()]
    public static class Queries
    {
        public static IAdvancedDbQuery GetMyTables(
            string dataModuleName = "",
            IBdoLog log = null,
            string filterQuery = null,
            string orderByQuery = null,
            int? pageSize = null,
            string pageToken = null)
        => DbQueryFactory.CreateAdvancedSelect("GetMyTables", DbTableFactory.Create().WithDataModule(dataModuleName))
            .AsDistinct()
            .WithFields(
                DbFieldFactory.CreateAsAll("table"),
                DbFieldFactory.Create("Field1", "table"),
                DbFieldFactory.Create("Field2", "table"))
            .From(
                DbQueryStatementFactory.CreateFrom(DbTableFactory.Create(nameof(DbMyTable).Substring(2), "schema1").WithAlias("table"))
                    .WithJointures(
                        DbQueryStatementFactory.CreateJointure(DbQueryJointureKind.Left,
                            DbTableFactory.Create("DbTable1".Substring(2), "schema2").WithAlias("table1"),
                            DbFieldFactory.Create("table1key", "table1"),
                            DbFieldFactory.Create(nameof(DbMyTable.ExecutionStatusReferenceId), "table")),
                        DbQueryStatementFactory.CreateJointure(DbQueryJointureKind.Left,
                            DbTableFactory.Create("DbTable1".Substring(2), "schema2").WithAlias("table2"),
                            DbFieldFactory.Create("table1key", "table2"),
                            DbFieldFactory.Create("Field1", "table"))))
            .Filter(
                filterQuery,
                log,
                new ApiScriptFilteringDefinition(
                    new ApiScriptClause("startCreationDate", DbFieldFactory.Create("CreationDate", "table")),
                    new ApiScriptClause("endCreationDate", DbFieldFactory.Create("CreationDate", "table")),
                    new ApiScriptClause("name", DbFieldFactory.Create("Name", "table"), DataOperator.Equal)
                //new ApiScriptClause("table", null, DataOperator.,
                //    new ApiScriptFilteringDefinition(
                //        new ApiScriptClause("CreationDate", DbFieldFactory.Create("CreationDate", "MyTable", nameof(DbSchemas.Iam), null), DataOperator.GreaterOrEqual)))
                ))
            .Sort(
                orderByQuery,
                log,
                new ApiScriptSortingDefinition(
                    new ApiScriptField("CreationDate", DbFieldFactory.Create("CreationDate", "table"))
                    , new ApiScriptField("Id", DbFieldFactory.Create("Name", "table"))
                    , new ApiScriptField("LastModificationDate", DbFieldFactory.Create("LastModificationDate", "table"))));

        public static IDbQuery GetMyTable(string name, string dataModuleName = "module")
            => DbQueryFactory.CreateBasicSelect("GetMyTable", DbTableFactory.Create(nameof(DbMyTable).Substring(2), null, dataModuleName), p =>
                p.AsDistinct()
                .WithFields(
                    DbFieldFactory.Create("table").AsAll(),
                    DbFieldFactory.Create("Field1", "table"),
                    DbFieldFactory.Create("Field2", "table"))
                .From(
                    DbQueryStatementFactory.CreateFrom(DbTableFactory.Create(nameof(DbMyTable).Substring(2), "schema1").WithAlias("table"))
                        .WithJointures(
                            DbQueryStatementFactory.CreateJointure(
                                DbQueryJointureKind.Left,
                                DbTableFactory.Create("DbTable1".Substring(2), "schema2").WithAlias("table1"),
                                DbFieldFactory.Create("table1key", "table1"),
                                DbFieldFactory.Create(nameof(DbMyTable.ExecutionStatusReferenceId), "table")),
                            DbQueryStatementFactory.CreateJointure(
                                DbQueryJointureKind.Left,
                                DbTableFactory.Create("DbTable1".Substring(2), "schema2").WithAlias("table2"),
                                DbFieldFactory.Create("table1key", "table2"),
                                DbFieldFactory.Create("Field1", "table"))))
                .WithIdFields(DbFieldFactory.CreateAsParameter(nameof(DbMyTable.Name), nameof(DbMyTable).Substring(2), p.UseParameter("name", DataValueType.Text, name))));


        [StoredDbQuery("delete_table")]
        public static IDbQuery DeleteMyTable()
            => DbQueryFactory.CreateBasicDelete(
                "DeleteMyTable",
                DbTableFactory.Create(nameof(DbMyTable).Substring(2), "schema1", "dataModuleName"), p =>
                    p.WithIdFields(
                        DbFieldFactory.CreateAsParameter(nameof(DbMyTable.MyTableId), p.UseParameter("id")),
                        DbFieldFactory.CreateAsParameter(nameof(DbMyTable.Name), p.UseParameter("name"))))
                    .WithParameters(
                        ElementSpecFactory.Create("dataModuleName", DataValueType.Text),
                        ElementSpecFactory.Create("id", DataValueType.Text),
                        ElementSpecFactory.Create("name", DataValueType.Text));

        public static IDbQuery UpdateMyTable(DbMyTable table, string dataModuleName = "module")
            => DbQueryFactory.CreateBasicUpdate("UpdateMyTable", DbTableFactory.Create(nameof(DbMyTable).Substring(2), "schema1"))
                .WithTableAlias("u_table1")
                .WithFields(
                    DbFieldFactory.Create(nameof(DbMyTable.Name), table?.Name),
                    DbFieldFactory.CreateAsQuery("fieldA",
                        DbQueryFactory.CreateBasicSelect(DbTableFactory.Create("table1", "schema1", dataModuleName))
                            .AsDistinct()
                            .WithFields(DbFieldFactory.Create("id"))
                            .WithIdFields(DbFieldFactory.Create("name", "myname"))))
                .From(
                    DbQueryStatementFactory.CreateFrom(DbTableFactory.Create(nameof(DbMyTable).Substring(2), "schema1").WithAlias("table1"))
                        .WithJointures(
                            DbQueryStatementFactory.CreateJointure(DbQueryJointureKind.Left,
                                DbTableFactory.Create("tableA", "schema1").WithAlias("item"),
                                DbFieldFactory.Create("idA", "tableA"),
                                DbFieldFactory.Create("idA", nameof(DbMyTable).Substring(2), "schema1"),
                                DataOperator.Lesser)))
                .WithIdFields(
                    DbFieldFactory.CreateAsOther("fieldA", "u_item", DbFieldFactory.Create("fieldA", "item")),
                    DbFieldFactory.CreateAsLiteral(nameof(DbMyTable.Name), "table1", table?.Name, DataValueType.Text),
                    DbFieldFactory.CreateAsLiteral("fieldA", "item", "categoryName", DataValueType.Text));

        public static IDbQuery InsertMyTable(DbMyTable table, string dataModuleName = "module")
            => DbQueryFactory.CreateBasicInsert("InsertMyTable",
                DbTableFactory.Create(nameof(DbMyTable).Substring(2), null, dataModuleName), false, p =>
                p.WithFields(
                    DbFieldFactory.CreateAsScript(nameof(DbMyTable.CreationDate), "$sqlGetCurrentDate()".CreateScript()),
                    DbFieldFactory.CreateAsScript(nameof(DbMyTable.LastModificationDate), "$sqlGetCurrentDate()".CreateScript()),
                    DbFieldFactory.CreateAsParameter(nameof(DbMyTable.Name), p.UseParameter("name", table?.Name)),
                    DbFieldFactory.CreateAsScript(nameof(DbMyTable.rowguid), "$sqlNewGuid()".CreateScript()),
                    DbFieldFactory.CreateAsParameter(nameof(DbMyTable.DisplayName), p.UseParameter("displayName", table?.DisplayName)),
                    DbFieldFactory.CreateAsParameter(nameof(DbMyTable.Description), p.UseParameter(null, table?.Description))));
    }
}
