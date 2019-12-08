using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Extensions.Connectors;
using BindOpen.Framework.Databases.Extensions.Scriptwords;
using BindOpen.Framework.Core.Data.Depots.Datasources;
using System;

namespace BindOpen.Framework.Databases.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder : IdentifiedDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly DatabaseConnectorKind _databaseConnectorKind = DatabaseConnectorKind.None;

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        protected readonly IBdoScope _scope = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder class.
        /// </summary>
        /// <param name="databaseKind">The kind of database to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        public DbQueryBuilder(
            DatabaseConnectorKind databaseKind,
            IBdoScope scope = null)
        {
            _databaseConnectorKind = databaseKind;
            _scope = scope;
        }

        #endregion

        // ------------------------------------------
        // BASIC QUERY BUILBING
        // ------------------------------------------

        #region Basic Query Building

        /// <summary>
        /// Gets the database name corresponding to the specified data module name.
        /// </summary>
        /// <param name="dataModuleName">The data module name to consider.</param>
        /// <remarks>If not found, it returns the specified data module name.</remarks>
        protected string GetDatabaseName(string dataModuleName)
        {
            if (dataModuleName != null && dataModuleName.StartsWith(StringHelper.__UniqueToken) && dataModuleName.EndsWith(StringHelper.__UniqueToken))
            {
                return dataModuleName;
            }

            var dataSourceDepot = _scope?.DataStore?.Get<IBdoDatasourceDepot>();
            if (dataSourceDepot == null)
                return dataModuleName;
            else
                return dataSourceDepot.GetInstanceOtherwiseModuleName(dataModuleName);
        }

        /// <summary>
        /// Builds the specified simple database data query and put the result into the specified string query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="queryString">The output string query.</param>
        /// <returns>The log of the build task.</returns>
        public IBdoLog BuildQuery(
            IDbQuery query,
            IBdoScriptVariableSet scriptVariableSet,
            out string queryString) => BuildQuery(query, null, scriptVariableSet, out queryString);

        /// <summary>
        /// Builds the specified simple database data query and put the result into the specified string query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="queryString">The output string query.</param>
        /// <returns>The log of the build task.</returns>
        public IBdoLog BuildQuery(
            IDbQuery query,
            DataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            var log = new BdoLog();

            queryString = "";

            try
            {
                if (query is BasicDbQuery basicDbQuery)
                {
                    (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);
                    log.AddEvents(Build(basicDbQuery, scriptVariableSet, out queryString));
                }
                else if (query is AdvancedDbQuery advancedDbQuery)
                {
                    (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);
                    log.AddEvents(Build(advancedDbQuery, scriptVariableSet, out queryString));
                }
                else if (query is StoredDbQuery storedDbQuery)
                {
                    if (!storedDbQuery.QueryTexts.TryGetValue(Id, out queryString))
                    {
                        BuildQuery(storedDbQuery.Query, null, scriptVariableSet, out queryString);
                        storedDbQuery.QueryTexts.Add(Id, queryString);
                    }

                    if (!string.IsNullOrEmpty(queryString) && (parameterSet != null))
                    {
                        foreach (var element in parameterSet.Elements)
                        {
                            var name = element.Name;
                            if (string.IsNullOrEmpty(name) && storedDbQuery.ParameterSpecSet != null)
                            {
                                name = storedDbQuery.ParameterSpecSet[element.Index - 1]?.Name;
                            }
                            queryString = queryString.Replace(StringHelper.__UniqueToken + name + StringHelper.__UniqueToken, element.GetObject()?.ToString() ?? "", false);
                        }
                    }
                    return log;
                }
            }
            catch (Exception ex)
            {
                log.AddError(
                    "Error trying to build query '" + (query.Name ?? "(Undefinied)") + "'",
                    description: ex.ToString() + ". Built query is : '" + queryString + "'.");
            }

            return null;
        }

        // Builds simple query ----------------------

        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        protected virtual IBdoLog Build(
            IBasicDbQuery query,
            IBdoScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            queryString = "";
            return new BdoLog();
        }


        // Builds advanced query ----------------------

        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="queryString"></param>
        protected virtual IBdoLog Build(
            IAdvancedDbQuery query,
            IBdoScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            queryString = "";
            return new BdoLog();
        }

        #endregion
    }
}