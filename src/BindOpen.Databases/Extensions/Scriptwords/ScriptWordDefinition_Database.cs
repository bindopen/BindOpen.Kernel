using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Queries;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Scripting;

namespace BindOpen.Extensions.Scriptwords
{
    /// <summary>
    /// This class represents a 'Database' script word definition.
    /// </summary>
    [BdoScriptwordDefinition()]
    public static class ScriptwordDefinition_Database
    {
        // ------------------------------------------
        // FUNCTIONS
        // ------------------------------------------

        #region Functions

        // Aggregate

        /// <summary>
        /// Evaluates the script word $SQLCOUNT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlCount")]
        public static string Fun_SqlCount(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Count(parameters);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLSUM.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlSum")]
        public static string Fun_SqlSum(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Sum(parameters);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLAVG.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlAverage")]
        public static string Fun_SqlAverage(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Average(parameters);
            }

            return text;
        }

        // Date and time

        /// <summary>
        /// Evaluates the script word $SQLGETCURRENTDATE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlGetCurrentDate")]
        public static string Fun_SqlGetCurrentDate(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_CurrentDate(parameters);
            }

            return text;
        }

        // Date type

        // Logical

        /// <summary>
        /// Evaluates the script word $SQLTRUE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlTrue")]
        public static string Fun_SqlTrue(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_True();
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLIF.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlIf")]
        public static string Fun_SqlIf(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string condition = parameters.GetStringAtIndex(0);
            string value1 = parameters.GetStringAtIndex(1);
            string value2 = parameters.GetStringAtIndex(2);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_If(condition, value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLNOT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlNot")]
        public static string Fun_SqlNot(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Not(value1);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLOR.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlOr")]
        public static string Fun_SqlOr(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Or(parameters);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLAND.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlAnd")]
        public static string Fun_SqlAnd(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_And(parameters);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLXOR.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlXor")]
        public static string Fun_SqlXor(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Xor(parameters);
            }

            return text;
        }

        // Comparison
        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlEq")]
        public static string Fun_SqlEq(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Eq(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlDiff")]
        public static string Fun_SqlDiff(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Diff(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlGt")]
        public static string Fun_SqlGt(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Gt(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlGte")]
        public static string Fun_SqlGte(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Gte(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlLt")]
        public static string Fun_SqlLt(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Lt(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlLte")]
        public static string Fun_SqlLte(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Lte(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlIsNull")]
        public static string Fun_SqlIsNull(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_IsNull(value1);
            }

            return text;
        }

        // Conversion

        /// <summary>
        /// Evaluates the script word $SQLCONVERTTOTEXT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlConvertToText")]
        public static string Fun_SqlConvertToText(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_ConvertToText(value1);
            }

            return text;
        }

        // String

        /// <summary>
        /// Evaluates the script word $SQLTEXT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlText")]
        public static string Fun_SqlText(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Text(value1);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLLIKE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlLike")]
        public static string Fun_SqlLike(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Like(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLREPLACE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlReplace")]
        public static string Fun_SqlReplace(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);
            string value3 = parameters.GetStringAtIndex(2);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Replace(value1, value2, value3);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLCONCATENATE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlConcatenate")]
        public static string Fun_SqlConcatenate(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Concatenate(parameters);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLNULL.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlNull")]
        public static string Fun_SqlNull(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Null();
            }

            return text;
        }

        // Syntax

        /// <summary>
        /// Evaluates the script word %SQLDATABASE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlDatabase")]
        public static string Fun_SqlDatabase(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text = "<DatabaseBuilderMissing/>";
            }
            else
            {
                value1 = value1.GetValueFromText();

                string instanceName = scope?.DataStore?.Get<IBdoDatasourceDepot>()?.GetInstanceName(value1);
                if (string.IsNullOrEmpty(instanceName) || instanceName == StringHelper.__NoneString)
                    instanceName = value1;

                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Database(instanceName);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%SCHEMA.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_SqlDatabase_SqlSchema(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                value1 = value1.GetValueFromText();
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Schema(value1, scriptWord.Parent?.StringItem);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%TABLE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_SqlDatabase_SqlTable(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                value1 = value1.GetValueFromText();
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Table(value1, scriptWord.Parent?.StringItem);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word [[%DATABASE->]%TABLE->]%FIELD.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_SqlDatabase_SqlTable_SqlField(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                value1 = value1.GetValueFromText();
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Field(value1, scriptWord.Parent?.StringItem);
            }

            return text;
        }

        // System

        /// <summary>
        /// Evaluates the script word $SQLNEWGUID.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlNewGuid")]
        public static string Fun_SqlNewGuid(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_NewGuid();
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLRANDOM.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlRandom")]
        public static string Fun_SqlRandom(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Random();
            }

            return text;
        }

        // Comparison
        /// <summary>
        /// Evaluates the script word $SQLIN.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlIn")]
        public static string Fun_SqlIn(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_In(value1, value2);
            }

            return text;
        }

        // Comparison
        /// <summary>
        /// Evaluates the script word $SQLLIST.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlList")]
        public static string Fun_SqlList(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing/>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_List(parameters);
            }

            return text;
        }

        #endregion
    }
}