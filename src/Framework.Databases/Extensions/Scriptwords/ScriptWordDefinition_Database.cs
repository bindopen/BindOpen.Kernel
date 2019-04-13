using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Runtime.Scriptwords;
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
        public static String Fun_SqlCount(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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
        public static String Fun_SqlSum(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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
        public static String Fun_SqlAverage(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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
        public static String Fun_SqlGetCurrentDate(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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
        public static String Fun_SqlTrue(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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
        public static String Fun_SqlIf(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String condition = parameters.GetStringAtIndex(0);
            String value1 = parameters.GetStringAtIndex(1);
            String value2 = parameters.GetStringAtIndex(2);

            String text = "";
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
        public static String Fun_SqlNot(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);

            String text = "";
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
        public static String Fun_SqlOr(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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
        public static String Fun_SqlAnd(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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
        public static String Fun_SqlXor(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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
        public static String Fun_SqlEq(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlDiff(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlGt(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            String text = "";
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
        public static String Fun_SqlGte(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            String text = "";
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
        public static String Fun_SqlLt(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            String text = "";
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
        public static String Fun_SqlLte(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            String text = "";
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
        public static String Fun_SqlIsNull(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlConvertToText(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlText(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlLike(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlReplace(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);
            String value3 = parameters.GetStringAtIndex(2);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlConcatenate(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlNull(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlDatabase(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text = "<DatabaseBuilderMissing>";
            else
            {
                value1 = ScriptParsingHelper.GetValueFromText(value1);

                String instanceName = appScope?.DataSourceService?.GetInstanceName(value1);
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
        public static String Fun_SqlDatabase_SqlSchema(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
            {
                text += "<DatabaseBuilderMissing>";
            }
            else
            {
                value1 = ScriptParsingHelper.GetValueFromText(value1);
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
        public static String Fun_SqlDatabase_SqlTable(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
            else
            {
                value1 = ScriptParsingHelper.GetValueFromText(value1);
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
        public static String Fun_SqlDatabase_SqlTable_SqlField(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);

            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
            else
            {
                value1 = ScriptParsingHelper.GetValueFromText(value1);
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
        public static String Fun_SqlNewGuid(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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
        public static String Fun_SqlRandom(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
            if (scriptVariableSet?.Has(ScriptVariableKey_Database.DbBuilder) != true)
                text += "<DatabaseBuilderMissing>";
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
        public static String Fun_SqlIn(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            String text = "";
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
        public static String Fun_SqlList(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            String text = "";
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