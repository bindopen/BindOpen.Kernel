using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Expression;
using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.System.Diagnostics;

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
        => DbFactory.CreateAdvancedSelect("GetMyTables", DbFactory.CreateTable().WithDataModule(dataModuleName))
            .AsDistinct()
            .WithFields(
                DbFactory.CreateFieldAsAll("table"),
                DbFactory.CreateField("Field1", "table"),
                DbFactory.CreateField("Field2", "table"))
            .From(
                DbFactory.CreateFromStatement(DbFactory.CreateTable(nameof(DbMyTable).Substring(2), "schema1").WithAlias("table"))
                    .WithJointures(
                        DbFactory.CreateJoinStatement(DbQueryJointureKind.Left,
                            DbFactory.CreateTable("DbTable1".Substring(2), "schema2").WithAlias("table1"),
                            DbFactory.CreateField("table1key", "table1"),
                            DbFactory.CreateField(nameof(DbMyTable.ExecutionStatusReferenceId), "table")),
                        DbFactory.CreateJoinStatement(DbQueryJointureKind.Left,
                            DbFactory.CreateTable("DbTable1".Substring(2), "schema2").WithAlias("table2"),
                            DbFactory.CreateField("table1key", "table2"),
                            DbFactory.CreateField("Field1", "table"))))
            .Filter(
                filterQuery,
                log,
                new ApiScriptFilteringDefinition(
                    new ApiScriptClause("startCreationDate", DbFactory.CreateField("CreationDate", "table")),
                    new ApiScriptClause("endCreationDate", DbFactory.CreateField("CreationDate", "table")),
                    new ApiScriptClause("name", DbFactory.CreateField("Name", "table"), DataOperator.Equal)
                //new ApiScriptClause("table", null, DataOperator.,
                //    new ApiScriptFilteringDefinition(
                //        new ApiScriptClause("CreationDate", DbFieldFactory.Create("CreationDate", "MyTable", nameof(DbSchemas.Iam), null), DataOperator.GreaterOrEqual)))
                ))
            .Sort(
                orderByQuery,
                log,
                new ApiScriptSortingDefinition(
                    new ApiScriptField("CreationDate", DbFactory.CreateField("CreationDate", "table"))
                    , new ApiScriptField("Id", DbFactory.CreateField("Name", "table"))
                    , new ApiScriptField("LastModificationDate", DbFactory.CreateField("LastModificationDate", "table"))));

        public static IDbQuery GetMyTable(string name, string dataModuleName = "module")
            => DbFactory.CreateBasicSelect("GetMyTable", DbFactory.CreateTable(nameof(DbMyTable).Substring(2), null, dataModuleName))
                .AsDistinct()
                .WithFields(
                    DbFactory.CreateField("table").AsAll(),
                    DbFactory.CreateField("Field1", "table"),
                    DbFactory.CreateField("Field2", "table"))
                .From(
                    DbFactory.CreateFromStatement(DbFactory.CreateTable(nameof(DbMyTable).Substring(2), "schema1").WithAlias("table"))
                        .WithJointures(
                            DbFactory.CreateJoinStatement(
                                DbQueryJointureKind.Left,
                                DbFactory.CreateTable("DbTable1".Substring(2), "schema2").WithAlias("table1"),
                                DbFactory.CreateField("table1key", "table1"),
                                DbFactory.CreateField(nameof(DbMyTable.ExecutionStatusReferenceId), "table")),
                            DbFactory.CreateJoinStatement(
                                DbQueryJointureKind.Left,
                                DbFactory.CreateTable("DbTable1".Substring(2), "schema2").WithAlias("table2"),
                                DbFactory.CreateField("table1key", "table2"),
                                DbFactory.CreateField("Field1", "table"))))
                .WithIdFields(DbFactory.CreateFieldAsParameter(nameof(DbMyTable.Name), nameof(DbMyTable).Substring(2), "name"))
                .WithParameters(
                    DbFactory.CreateParameter("name", DataValueType.Text, name)
                );


        [StoredDbQuery("delete_table")]
        public static IDbQuery DeleteMyTable()
            => DbFactory.CreateBasicDelete("DeleteMyTable", DbFactory.CreateTable(nameof(DbMyTable).Substring(2), "schema1", "dataModuleName"))
                    .WithIdFields(
                        DbFactory.CreateFieldAsParameter(nameof(DbMyTable.MyTableId), "id"),
                        DbFactory.CreateFieldAsParameter(nameof(DbMyTable.Name), "name"))
                    .UsingParameters(
                        ElementSpecFactory.Create("dataModuleName", DataValueType.Text),
                        ElementSpecFactory.Create("id", DataValueType.Text),
                        ElementSpecFactory.Create("name", DataValueType.Text)
                    );

        public static IDbQuery UpdateMyTable(DbMyTable table, string dataModuleName = "module")
            => DbFactory.CreateBasicUpdate("UpdateMyTable", DbFactory.CreateTable(nameof(DbMyTable).Substring(2), "schema1"))
                .WithTableAlias("u_table1")
                .WithFields(
                    DbFactory.CreateField(nameof(DbMyTable.Name), table?.Name),
                    DbFactory.CreateFieldAsQuery("fieldA",
                        DbFactory.CreateBasicSelect(DbFactory.CreateTable("table1", "schema1", dataModuleName))
                            .AsDistinct()
                            .WithFields(DbFactory.CreateField("id"))
                            .WithIdFields(DbFactory.CreateField("name", "myname"))))
                .From(
                    DbFactory.CreateFromStatement(DbFactory.CreateTable(nameof(DbMyTable).Substring(2), "schema1").WithAlias("table1"))
                        .WithJointures(
                            DbFactory.CreateJoinStatement(DbQueryJointureKind.Left,
                                DbFactory.CreateTable("tableA", "schema1").WithAlias("item"),
                                DbFactory.CreateField("idA", "tableA"),
                                DbFactory.CreateField("idA", nameof(DbMyTable).Substring(2), "schema1"),
                                DataOperator.Lesser)))
                .WithIdFields(
                    DbFactory.CreateFieldAsOther("fieldA", "u_item", DbFactory.CreateField("fieldA", "item")),
                    DbFactory.CreateFieldAsLiteral(nameof(DbMyTable.Name), "table1", table?.Name, DataValueType.Text),
                    DbFactory.CreateFieldAsLiteral("fieldA", "item", "categoryName", DataValueType.Text));

        public static IDbQuery InsertMyTable(DbMyTable table, string dataModuleName = "module")
            => DbFactory.CreateBasicInsert("InsertMyTable", DbFactory.CreateTable(nameof(DbMyTable).Substring(2), null, dataModuleName), false)
                .WithFields(
                    DbFactory.CreateFieldAsScript(nameof(DbMyTable.CreationDate), "$sqlGetCurrentDate()".CreateScript()),
                    DbFactory.CreateFieldAsScript(nameof(DbMyTable.LastModificationDate), "$sqlGetCurrentDate()".CreateScript()),
                    DbFactory.CreateFieldAsParameter(nameof(DbMyTable.Name), "name"),
                    DbFactory.CreateFieldAsScript(nameof(DbMyTable.rowguid), "$sqlNewGuid()".CreateScript()),
                    DbFactory.CreateFieldAsParameter(nameof(DbMyTable.DisplayName), "displayName"),
                    DbFactory.CreateFieldAsParameter(nameof(DbMyTable.Description), "description")
                )
                .WithParameters(
                    DbFactory.CreateParameter("name", table?.Name),
                    DbFactory.CreateParameter("displayName", table?.DisplayName),
                    DbFactory.CreateParameter("description", table?.Description));
    }
}
