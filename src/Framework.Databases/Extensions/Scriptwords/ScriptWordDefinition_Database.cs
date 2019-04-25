using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries.Builders;

namespace BindOpen.Framework.Databases.Extensions.Scriptwords
{
    /// <summary>
    /// This class represents a 'Database' script word definition.
    /// </summary>
    [ScriptwordDefinition()]
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name="sqlCount")]
        public static string Fun_SqlCount(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlSum")]
        public static string Fun_SqlSum(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlAverage")]
        public static string Fun_SqlAverage(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlGetCurrentDate")]
        public static string Fun_SqlGetCurrentDate(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlTrue")]
        public static string Fun_SqlTrue(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlIf")]
        public static string Fun_SqlIf(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string condition = parameters.GetStringAtIndex(0);
            string value1 = parameters.GetStringAtIndex(1);
            string value2 = parameters.GetStringAtIndex(2);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlNot")]
        public static string Fun_SqlNot(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlOr")]
        public static string Fun_SqlOr(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlAnd")]
        public static string Fun_SqlAnd(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlXor")]
        public static string Fun_SqlXor(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlEq")]
        public static string Fun_SqlEq(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Equal(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlDiff")]
        public static string Fun_SqlDiff(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_NotEqual(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlGt")]
        public static string Fun_SqlGt(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Greater(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlGte")]
        public static string Fun_SqlGte(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_GreaterOrEqual(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlLt")]
        public static string Fun_SqlLt(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_Less(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlLte")]
        public static string Fun_SqlLte(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
            }
            else
            {
                DbQueryBuilder queryBuilder = (DbQueryBuilder)scriptVariableSet.GetValue(ScriptVariableKey_Database.DbBuilder);
                text += queryBuilder.GetSqlText_LessOrEqual(value1, value2);
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlIsNull")]
        public static string Fun_SqlIsNull(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlConvertToText")]
        public static string Fun_SqlConvertToText(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlText")]
        public static string Fun_SqlText(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlLike")]
        public static string Fun_SqlLike(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlReplace")]
        public static string Fun_SqlReplace(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);
            string value3 = parameters.GetStringAtIndex(2);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlConcatenate")]
        public static string Fun_SqlConcatenate(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlNull")]
        public static string Fun_SqlNull(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlDatabase")]
        public static string Fun_SqlDatabase(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text = "<DatabaseBuilderMissing>";
            }
            else
            {
                value1 = value1.GetValueFromText();

                string instanceName = appScope?.DataSourceService?.GetInstanceName(value1);
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword]
        public static string Fun_SqlDatabase_SqlSchema(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword]
        public static string Fun_SqlDatabase_SqlTable(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword]
        public static string Fun_SqlDatabase_SqlTable_SqlField(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlNewGuid")]
        public static string Fun_SqlNewGuid(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlRandom")]
        public static string Fun_SqlRandom(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlIn")]
        public static string Fun_SqlIn(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);
            string value2 = parameters.GetStringAtIndex(1);

            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">Variables that can be used for interpretation.</param>
        /// <param name="scriptWord">Script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword(Name = "sqlList")]
        public static string Fun_SqlList(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            string text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
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