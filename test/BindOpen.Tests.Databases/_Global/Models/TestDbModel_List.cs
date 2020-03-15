using BindOpen.Data.Common;
using BindOpen.Data.Models;
using BindOpen.Data.Queries;
using BindOpen.System.Diagnostics;
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
        /// <param name="q"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageToken"></param>
        /// <returns></returns>
        internal IDbQuery ListEmployees1(string q = null, string orderBy = null, int? pageSize = null, int? pageToken = null,
            IBdoLog log = null, string filterQuery = null, string orderByQuery = null)
            => DbFluent.SelectQuery("GetMyTables", DbFluent.Table())
                .WithLimit(100)
                .AsDistinct()
                .WithFields(
                    DbFluent.FieldAsAll(DbFluent.Table("table")),
                    DbFluent.Field("Field1", DbFluent.Table("table")),
                    DbFluent.Field("Field2", DbFluent.Table("table")))
                .From(
                    DbFluent.Table(nameof(DbRegionalDirectorate).Substring(2), "schema1").WithAlias("table"),
                    DbFluent.Table(DbQueryJoinKind.Left, DbFluent.Table("DbTable1".Substring(2), "schema2").WithAlias("table1"))
                        .WithCondition(
                            DbFluent.And(
                                DbFluent.Eq(
                                    DbFluent.Field("table1key", DbFluent.Table("table1")),
                                    DbFluent.Field(nameof(DbRegionalDirectorate.Code), DbFluent.Table("table"))),
                                DbFluent.Eq(
                                    DbFluent.Field("table2key", DbFluent.Table("table2")),
                                    DbFluent.Field(nameof(DbRegionalDirectorate.LabelEN), DbFluent.Table("table"))))),
                    DbFluent.Table(DbQueryJoinKind.Left, DbFluent.Table("DbTable1".Substring(2), "schema2").WithAlias("table2"))
                        .WithCondition(
                            DbFluent.Eq(
                                DbFluent.Field("table1key", DbFluent.Table("table2")),
                                DbFluent.Field("Field1", DbFluent.Table("table"))))
                )
                .Filter(
                    filterQuery,
                    log,
                    new ApiScriptFilteringDefinition(
                        new ApiScriptClause("startCreationDate", DbFluent.Field("CreationDate", DbFluent.Table("table"))),
                        new ApiScriptClause("endCreationDate", DbFluent.Field("CreationDate", DbFluent.Table("table"))),
                        new ApiScriptClause("name", DbFluent.Field("Name", DbFluent.Table("table")), DataOperator.Equal)
                    //new ApiScriptClause(DbFluent.Table("table"), null, DataOperator.,
                    //    new ApiScriptFilteringDefinition(
                    //        new ApiScriptClause("CreationDate", DbFieldFactory.Create("CreationDate", "MyTable", nameof(DbSchemas.Iam), null), DataOperator.GreaterOrEqual)))
                    ))
                .Sort(
                    orderByQuery,
                    log,
                    new ApiScriptSortingDefinition(
                        new ApiScriptField("CreationDate", DbFluent.Field("CreationDate", DbFluent.Table("table")))
                        , new ApiScriptField("Id", DbFluent.Field("Name", DbFluent.Table("table")))
                        , new ApiScriptField("LastModificationDate", DbFluent.Field("LastModificationDate", DbFluent.Table("table")))));
    }
}
