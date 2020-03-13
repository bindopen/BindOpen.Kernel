using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder_MSSqlServer class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        public DbQueryBuilder_MSSqlServer(
            IBdoScope scope = null)
            : base(scope)
        {
            Id = Id = "databases.mssqlserver$client";
        }

        #endregion

        // ------------------------------------------
        // BASIC QUERY BUILBING
        // ------------------------------------------

        #region Basic Query Building

        private string GetSqlText_Field(
            DbField field,
            IDataElementSet parameterSet,
            IBdoLog log,
            DbFieldViewMode viewMode = DbFieldViewMode.CompleteName,
            IBdoScriptVariableSet scriptVariableSet = null,
            string defaultDataModule = null,
            string defaultSchema = null,
            string defaultDataTable = null)
        {
            string queryString = "";

            if (field != null)
            {
                switch (viewMode)
                {
                    case DbFieldViewMode.CompleteName:
                    case DbFieldViewMode.CompleteNameOrValue:
                    case DbFieldViewMode.CompleteNameAsAlias:
                        if (field.IsAll)
                        {
                            string tableName = GetSqlText_Table(
                                field.DataModule,
                                field.Schema,
                                field.DataTable,
                                field.DataTableAlias,
                                viewMode,
                                defaultDataModule,
                                defaultSchema);

                            queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                            queryString += "*";
                        }
                        else
                        {
                            if ((viewMode != DbFieldViewMode.CompleteNameOrValue) || (field.Value == null))
                            {
                                string tableName = GetSqlText_Table(
                                    field.DataModule,
                                    field.Schema,
                                    field.DataTable ?? defaultDataTable,
                                    field.DataTableAlias,
                                    viewMode,
                                    defaultDataModule,
                                    defaultSchema);

                                queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                                queryString += GetSqlText_Field(
                                    field,
                                    parameterSet,
                                    log,
                                    viewMode == DbFieldViewMode.CompleteNameAsAlias ?
                                        DbFieldViewMode.OnlyNameAsAlias :
                                        DbFieldViewMode.OnlyName,
                                    scriptVariableSet,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable);
                            }
                            else
                            {
                                queryString += GetSqlText_Field(
                                    field,
                                    parameterSet,
                                    log,
                                    DbFieldViewMode.OnlyValue,
                                    scriptVariableSet,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable);

                                if (viewMode == DbFieldViewMode.CompleteNameAsAlias)
                                {
                                    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));
                                }
                            }
                        }
                        break;
                    case DbFieldViewMode.OnlyName:
                        if (!string.IsNullOrEmpty(field.Alias))
                        {
                            queryString += GetSqlText_Field(field.Alias);
                        }
                        else if (field.IsNameAsScript)
                        {
                            string name = _scope?.Interpreter.Interprete(field.Name.CreateScript(), scriptVariableSet, log) ?? "";
                            queryString += GetSqlText_Field(name);
                        }
                        else
                        {
                            queryString += GetSqlText_Field(field.Name);
                        }
                        break;
                    case DbFieldViewMode.OnlyNameAsAlias:
                        if (field.IsNameAsScript)
                        {
                            string name = _scope?.Interpreter.Interprete(field.Name.CreateScript(), scriptVariableSet, log) ?? "";
                            queryString += GetSqlText_Field(name);
                        }
                        else
                        {
                            queryString += GetSqlText_Field(field.Name);
                        }
                        queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));

                        break;
                    case DbFieldViewMode.OnlyValue:
                        string value = _scope?.Interpreter.Interprete(field.Value, scriptVariableSet, log) ?? "";

                        if (field.Query != null)
                        {
                            string subQueryText = "";
                            if (field.Query is DbSingleQuery singleQuery)
                            {
                                subQueryText = GetSqlText_Query(singleQuery, parameterSet, scriptVariableSet, log);
                            }

                            queryString += "(" + subQueryText + ")";
                        }
                        else
                        {
                            queryString += GetSqlText_Value(value, field.ValueType);
                        }
                        break;
                    case DbFieldViewMode.NameEqualsValue:
                        queryString += GetSqlText_Field(
                            field,
                            parameterSet,
                            log,
                            DbFieldViewMode.CompleteName,
                            scriptVariableSet,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable);

                        queryString += "=";

                        queryString += GetSqlText_Field(
                            field,
                            parameterSet,
                            log,
                            DbFieldViewMode.OnlyValue,
                            scriptVariableSet,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable);

                        break;
                    default:
                        break;
                }
            }

            return queryString;
        }

        private string GetSqlText_Table(
            DbTable table,
            DbFieldViewMode viewMode = DbFieldViewMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null)
        {
            if (table != null)
            {
                return GetSqlText_Table(
                   table.DataModule,
                   table.Schema,
                   table.Name,
                   table.Alias,
                   viewMode,
                   defaultDataModule,
                   defaultSchema);
            }

            return "";
        }

        private string GetSqlText_Table(
            string tableDataModule,
            string tableSchema,
            string tableName,
            string tableAlias,
            DbFieldViewMode viewMode = DbFieldViewMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null)
        {
            string queryString = "";

            if ((viewMode == DbFieldViewMode.CompleteName) && (!string.IsNullOrEmpty(tableAlias)))
            {
                queryString += GetSqlText_Table(tableAlias);
            }
            else if (!string.IsNullOrEmpty(tableName))
            {
                if ((viewMode == DbFieldViewMode.CompleteName) || (viewMode == DbFieldViewMode.CompleteNameAsAlias))
                {

                    if (string.IsNullOrEmpty(tableDataModule))
                        tableDataModule = defaultDataModule;
                    if (!string.IsNullOrEmpty(tableDataModule))
                    {
                        tableDataModule = GetDatabaseName(tableDataModule);
                    }

                    if (string.IsNullOrEmpty(tableSchema))
                    {
                        tableSchema = defaultSchema;
                    }
                    queryString += DbFluent.Table(tableName, tableSchema, tableDataModule);
                }
                else
                {
                    queryString += GetSqlText_Table(tableName);
                }

                if (viewMode == DbFieldViewMode.CompleteNameAsAlias)
                {
                    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableAlias), " as " + GetSqlText_Table(tableAlias));
                }
            }

            return queryString;
        }

        private string GetSqlText_From(
            IDbQueryFromClause queryFrom,
            IDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoLog log)
        {
            string queryString = "";
            //foreach (DbQueryJoinedTable queryJoin in queryFrom.)
            //{
            //    switch (queryJoin.Kind)
            //    {
            //        case DbQueryJoinKind.Inner:
            //            {
            //                queryString += " inner join ";
            //                break;
            //            }
            //        case DbQueryJoinKind.Left:
            //            {
            //                queryString += " left join ";
            //                break;
            //            }
            //        case DbQueryJoinKind.Right:
            //            {
            //                queryString += " right join ";
            //                break;
            //            }
            //        case DbQueryJoinKind.Union:
            //            {
            //                queryString += " inner join ";
            //                break;
            //            }
            //    }

            //    if (queryJoin.Query != null)
            //    {
            //        string subQuery = BuildQuery(queryJoin.Query, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
            //        queryString += "(" + subQuery + ")";
            //        if (!string.IsNullOrEmpty(queryJoin.Query.Alias))
            //        {
            //            queryString += " as " + queryJoin.Query.Alias;
            //        }
            //    }
            //    else
            //    {
            //        if (queryJoin.Table == null || (string.IsNullOrEmpty(queryJoin.Table.Name) && string.IsNullOrEmpty(queryJoin.Table.Alias)))
            //        {
            //            queryJoin.Table = DbFluent.Table(query?.DataTable).WithAlias(query?.DataTableAlias);
            //        }
            //        queryString += GetSqlText_Table(queryJoin.Table, DbFieldViewMode.CompleteNameAsAlias);
            //    }

            //    if (queryJoin.Kind != DbQueryJoinKind.None)
            //    {
            //        queryString += " on ";
            //        string expression = _scope?.Interpreter.Interprete(queryJoin.Condition, scriptVariableSet, log) ?? String.Empty;
            //        queryString += expression;
            //    }
            //}
            //if (queryFrom.UnionStatement?.Query != null)
            //{
            //    switch (queryFrom.UnionStatement.Type)
            //    {
            //        case DbQueryUnionKind.Union:
            //            {
            //                queryString += " union ";
            //                break;
            //            }
            //    }
            //    string subQuery = BuildQuery(queryFrom.UnionStatement.Query, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
            //    queryString += "(" + subQuery + ")";
            //    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(queryFrom.UnionStatement.Query.Alias), " as " + queryFrom.UnionStatement.Query.Alias);
            //}

            return queryString;
        }

        #endregion
    }
}