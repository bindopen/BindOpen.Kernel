using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Data.Queries.ApiScript;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public static class Queries
    {
        public static IAdvancedDbQuery GetMyTables(
            string dataModuleName = "",
            ILog log = null,
            string filterQuery = null,
            string orderByQuery = null,
            int? pageSize = null,
            string pageToken = null)
        => new AdvancedDbQuery(DbQueryKind.Select, dataModuleName, null, null)
        {
            Name = "GetMyTables",
            IsDistinct = true,
            Fields =
            {
                new DbField(null, "table").WithAll(),
                new DbField("Field1", "table"),
                new DbField("Field2", "table"),
            },
            FromClauses =
            {
                new DbQueryFromStatement()
                {
                    JointureStatements=
                    {
                        new DbQueryJointureStatement(
                            DbQueryJointureKind.None,
                            new DbTable(nameof(DbMyTable).Substring(2), "schema1", null).WithAlias("table")),

                        new DbQueryJointureStatement(
                            DbQueryJointureKind.Left,
                            new DbTable("DbTable1".Substring(2), "schema2", null).WithAlias("table1"),
                            new DbField("table1key", "table1"),
                            new DbField(nameof(DbMyTable.ExecutionStatusReferenceId), "table")),

                        new DbQueryJointureStatement(
                            DbQueryJointureKind.Left,
                            new DbTable("DbTable1".Substring(2), "schema2", null).WithAlias("table2"),
                            new DbField("table1key", "table2"),
                            new DbField("Field1", "table"))
                    }
                }
            }
        }
            .Filter(
                filterQuery,
                log,
                new ApiScriptFilteringDefinition(
                    new ApiScriptClause("startCreationDate", new DbField("CreationDate", "table")),
                    new ApiScriptClause("endCreationDate", new DbField("CreationDate", "table")),
                    new ApiScriptClause("name", new DbField("Name", "table"), DataOperator.Equal)
                //new ApiScriptClause("table", null, DataOperator.,
                //    new ApiScriptFilteringDefinition(
                //        new ApiScriptClause("CreationDate", new DbField("CreationDate", "MyTable", nameof(DbSchemas.Iam), null), DataOperator.GreaterOrEqual)))
                ))
            .Sort(
                orderByQuery,
                log,
                new ApiScriptSortingDefinition(
                    new ApiScriptField("CreationDate", new DbField("CreationDate", "table"))
                    , new ApiScriptField("Id", new DbField("Name", "table"))
                    , new ApiScriptField("LastModificationDate", new DbField("LastModificationDate", "table"))
            ));

        public static IDbQuery GetMyTable(string name, string dataModuleName = "module")
            => new BasicDbQuery(DbQueryKind.Select, dataModuleName, null, nameof(DbMyTable).Substring(2))
            {
                Name = "GetMyTable",
                IsDistinct = true,
                Fields =
            {
                new DbField("table").WithAll(),
                new DbField("Field1", "table"),
                new DbField("Field2", "table"),
            },
                FromClauses =
            {
                new DbQueryFromStatement()
                {
                    JointureStatements=
                    {
                        new DbQueryJointureStatement(
                            DbQueryJointureKind.None,
                            new DbTable(nameof(DbMyTable).Substring(2), "schema1", null).WithAlias("table")),

                        new DbQueryJointureStatement(
                            DbQueryJointureKind.Left,
                            new DbTable("DbTable1".Substring(2), "schema2", null).WithAlias("table1"),
                            new DbField("table1key", "table1"),
                            new DbField(nameof(DbMyTable.ExecutionStatusReferenceId), "table")),

                        new DbQueryJointureStatement(
                            DbQueryJointureKind.Left,
                            new DbTable("DbTable1".Substring(2), "schema2", null).WithAlias("table2"),
                            new DbField("table1key", "table2"),
                            new DbField("Field1", "table"))
                    }
                }
            },
                IdFields =
            {
                new DbField(nameof(DbMyTable.Name), nameof(DbMyTable).Substring(2), DataValueType.Text, name)
            }
            };

        public static IDbQuery DeleteMyTable(string name, string dataModuleName = "module")
            => new BasicDbQuery(DbQueryKind.Delete, dataModuleName, "schema1", nameof(DbMyTable).Substring(2))
            {
                Name = "DeleteMyTable",
                IdFields =
            {
                new DbField(nameof(DbMyTable.Name), DataValueType.Text, name)
            }
            };

        public static IDbQuery UpdateMyTable(DbMyTable table, string dataModuleName = "module")
            => new BasicDbQuery(DbQueryKind.Update, null, "schema1", nameof(DbMyTable).Substring(2))
            {
                Name = "UpdateMyTable",
                DataTableAlias = "u_table1",
                Fields =
                {
                    new DbField(nameof(DbMyTable.Name), DataValueType.Text, table?.Name),
                    new DbField("fieldA",
                        new BasicDbQuery(DbQueryKind.Select, dataModuleName, "schema1","table1")
                        {
                            IsDistinct = true,
                            Fields = { new DbField("id") },
                            IdFields = { new DbField("name", DataValueType.Text, "myname") }
                        })
                },
                FromClauses =
                {
                    new DbQueryFromStatement()
                    {
                        JointureStatements=
                        {
                            new DbQueryJointureStatement(
                                DbQueryJointureKind.None,
                                new DbTable(nameof(DbMyTable).Substring(2), "schema1", null).WithAlias("table1")),

                            new DbQueryJointureStatement(
                                DbQueryJointureKind.Left,
                                new DbTable("fieldA", "schema1", null).WithAlias("item"),
                                new DbField("idA", ""),
                                new DbField("idA", "")),
                        }
                    }
                },
                IdFields =
                {
                    new DbField("fieldA", "u_item", new DbField("fieldA", "item")),
                    new DbField(nameof(DbMyTable.Name), "table1", DataValueType.Text, table?.Name),
                    new DbField("fieldA", "item", DataValueType.Text, "categoryName"),
                }
            };

        public static IDbQuery InsertMyTable(DbMyTable table, string dataModuleName = "module")
            => new BasicDbQuery(DbQueryKind.Insert, dataModuleName, null, nameof(DbMyTable).Substring(2))
            {
                Name = "InsertMyTable",
                Fields =
            {
                new DbField(nameof(DbMyTable.CreationDate), DataExpressionFactory.CreateScript("$sqlGetCurrentDate()")),
                new DbField(nameof(DbMyTable.LastModificationDate), DataExpressionFactory.CreateScript("$sqlGetCurrentDate()")),
                new DbField(nameof(DbMyTable.Name), table?.Name, DataValueType.Text),
                new DbField(nameof(DbMyTable.rowguid), DataExpressionFactory.CreateScript("$sqlNewGuid()")),
                new DbField(nameof(DbMyTable.DisplayName), table?.DisplayName, DataValueType.Text),
                new DbField(nameof(DbMyTable.Description), table?.Description, DataValueType.Text),
                //new DbField(nameof(DbTable.), table?.Description, DataValueType.Text),
            }
            };
    }
}
