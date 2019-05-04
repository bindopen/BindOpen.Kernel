using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Extensions.Connectors;
using BindOpen.Framework.Databases.Extensions.Scriptwords;

namespace BindOpen.Framework.Databases.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly DatabaseConnectorKind _databaseConnectorKind = DatabaseConnectorKind.None;

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        protected readonly IAppScope _appScope = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder class.
        /// </summary>
        /// <param name="databaseKind">The kind of database to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbQueryBuilder(
            DatabaseConnectorKind databaseKind,
            IAppScope appScope = null)
        {
            this._databaseConnectorKind = databaseKind;
            this._appScope = appScope;
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
            if (this._appScope?.DataSourceDepot == null)
                return dataModuleName;
            else
                return this._appScope?.DataSourceDepot.GetInstanceOtherwiseModuleName(dataModuleName);
        }

        /// <summary>
        /// Builds the specified simple database data query and put the result into the specified string query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="queryString">The output string query.</param>
        /// <returns>The log of the build task.</returns>
        public ILog BuildQuery(
            IDbDataQuery query,
            IScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            queryString = "";
            if (query is BasicDbDataQuery basicDbDataQuery)
                return this.BuildQuery(basicDbDataQuery, scriptVariableSet, out queryString);
            else if (query is AdvancedDbDataQuery advancedDbDataQuery)
                return this.BuildQuery(advancedDbDataQuery, scriptVariableSet, out queryString);

            return null;
        }

        // Builds simple query ----------------------

        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        protected virtual ILog Build(
            IBasicDbDataQuery query,
            IScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            queryString = "";
            return new Log();
        }

        /// <summary>
        /// Builds the specified simple database data query and put the result into the specified string query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="queryString">The output string query.</param>
        /// <returns>The log of the build task.</returns>
        public ILog BuildQuery(
            IBasicDbDataQuery query,
            IScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            queryString = "";

            // we instantiate the logger and the script interpreter
            ILog log = new Log();

            // we check that the data query exists
            if (query == null)
            {
                log.AddError(
                    "[QUERYBUILDER_SIMPLEQUERY_DATAQUERYMISSING] Build simple query not possible because data query to interprete was missing",
                    description: "Could not build query '" + (query.Name ?? "(Undefinied)") + "' because its data module was missing.");
            }
            else
            {
                try
                {
                    // we instantiate the interpretation variables containing the database provider

                    (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);

                    log.AddEvents(this.Build(query, scriptVariableSet, out queryString));
                }
                catch (Exception ex)
                {
                    log.AddError(
                        "Error trying to build query '" + (query.Name ?? "(Undefinied)") + "'",
                        description: ex.ToString() + ". Built query is : '" + queryString + "'.");
                }
            }

            return log;
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
        protected virtual ILog Build(
            IAdvancedDbDataQuery query,
            IScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            queryString = "";
            return new Log();
        }

        /// <summary>
        /// Builds the specified advanced database data query and put the result
        /// into the specified string query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="queryString">The output string query.</param>
        /// <returns>The log of the build task.</returns>
        public ILog BuildQuery(
            IAdvancedDbDataQuery query,
            IScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            queryString = "";
            ILog log = new Log();

            // we check that the data query exists
            if (query == null)
            {
                log.AddError(
                    "[QUERYBUILDER_SIMPLEQUERY_DATAQUERYMISSING] Build simple query not possible because data query to interprete was missing.",
                    description: "Could not build query '" + (query.Name ?? "(Undefinied)") + "' because its data module was missing."
                    );
            }
            else
            {
                try
                {
                    // we instantiate the interpretation variables containing the database provider

                    (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);

                    log.AddEvents(this.Build(query, scriptVariableSet, out queryString));
                }
                catch (Exception ex)
                {
                    log.AddError(
                        "Error trying to build query '" + (query.Name ?? "(Undefinied)") + "'",
                        description: ex.ToString() + ". Built query is : '" + queryString + "'.");
                }
            }

            return log;
        }

        #endregion
    }
}