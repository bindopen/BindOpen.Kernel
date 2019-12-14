using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.Application.Hosts;
using System;
using System.Collections;

namespace BindOpen.Framework.Runtime.Extensions.Scriptwords
{
    /// <summary>
    /// This class represents a 'Runtime' script word definition.
    /// </summary>
    [BdoScriptwordDefinition]
    public static class ScriptwordDefinition_runtime
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Evaluates the script word $(applicationModuleName).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_ApplicationModuleName(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            if (scope == null)
                return "<!--Application scope missing-->";
            IBdoHost appHost = scope.Context.GetSystemItem("appHost") as IBdoHost;
            if (appHost == null)
                return "<!--Application manager missing-->";

            return appHost?.HostOptions.ApplicationModule?.Name ?? "";
        }

        /// <summary>
        /// Evaluates the script word $(applicationInstanceName).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_ApplicationInstanceName(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            if (scope == null || scope.Context == null)
                return "<!--Application scope missing-->";
            IBdoHost appHost = scope.Context.GetSystemItem("appHost") as IBdoHost;
            if (appHost == null)
                return "<!--Application manager missing-->";

            return appHost?.HostOptions.HostSettings?.ApplicationInstanceName ?? "";
        }

        /// <summary>
        /// Evaluates the script word $ISEMPTY.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_IsEmpty(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            string value1 = parameters.GetStringAtIndex(0);

            if ((value1 == "\"\"") | (value1 == String.Empty))
                return "true";
            else
                return "false";
        }

        /// <summary>
        /// Evaluates the script word $TEXT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_Text(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            return "\"" + value1 + "\"";
        }

        /// <summary>
        /// Evaluates the script word $FORMAT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_FormatText(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String textValue = parameters.GetStringAtIndex(0);
            String aFormatText = parameters.GetStringAtIndex(1);

            return String.Format(textValue, aFormatText);
        }

        /// <summary>
        /// Evaluates the script word $IF.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_If(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String aCondition = parameters.GetStringAtIndex(0);

            if (string.Equals(aCondition, "true", StringComparison.OrdinalIgnoreCase))
                return parameters.GetStringAtIndex(1);
            else
                return parameters.GetStringAtIndex(2);
        }

        /// <summary>
        /// Evaluates the script word $NOT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_Not(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            return (!string.Equals(value1, "true", StringComparison.OrdinalIgnoreCase) ? "true" : "false");
        }

        /// <summary>
        /// Evaluates the script word $OR.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_Or(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            Boolean b = false;
            foreach (String st in parameters)
                b = (b | (string.Equals(st, "true", StringComparison.OrdinalIgnoreCase)));
            return (b ? "true" : "false");
        }

        /// <summary>
        /// Evaluates the script word $AND.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_And(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            Boolean b = true;
            foreach (String st in parameters)
                b &= (string.Equals(st, "true", StringComparison.OrdinalIgnoreCase));
            return (b ? "true" : "false");
        }

        /// <summary>
        /// Evaluates the script word $XOR.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_Xor(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            Boolean b = true;
            foreach (String st in parameters)
                b = (b ^ (string.Equals(st, "true", StringComparison.OrdinalIgnoreCase)));
            return (b ? "true" : "false");
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_IsEqual(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "true" : "false");
        }

        /// <summary>
        /// Evaluates the script word $ISDIFFERENT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_IsDifferent(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "false" : "true");
        }

        /// <summary>
        /// Evaluates the script word $(GETCURRENTDATETIME).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_GetCurrentDateTime(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            return DateTime.Now.ToString();
        }


        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().FORMAT(aFormat).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DateTime_Format(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String aFormat = parameters.GetStringAtIndex(0);

            aFormat = BdoScriptParsingHelper.GetValueFromText(aFormat);
            DateTime dateTime = DateTime.Now;

            if ((scriptWord.Parent != null) && (scriptWord.Parent.StringItem != null))
            {
                if (!DateTime.TryParse(scriptWord.Parent.StringItem.ToString(), out dateTime))
                    dateTime = DateTime.Now;
            }

            return dateTime.ToString(aFormat);
        }

        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().TIMESTAMP().
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DateTime_TimeStamp(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            DateTime dateTime = DateTime.Now;

            if ((scriptWord.Parent != null) && (scriptWord.Parent.StringItem != null))
            {
                if (!DateTime.TryParse(scriptWord.Parent.StringItem.ToString(), out dateTime))
                    dateTime = DateTime.Now;
            }

            return dateTime.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().SUBSTRACT(aYear, aMonth, aDay, aHour, aMinute, aSecond).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DateTime_Add(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String year = parameters.GetStringAtIndex(0);
            String month = parameters.GetStringAtIndex(1);
            String day = parameters.GetStringAtIndex(2);
            String hour = parameters.GetStringAtIndex(3);
            String minute = parameters.GetStringAtIndex(4);
            String second = parameters.GetStringAtIndex(5);

            DateTime dateTime = DateTime.Now;

            if ((scriptWord.Parent != null) && (scriptWord.Parent.StringItem != null))
            {
                if (DateTime.TryParse(scriptWord.Parent.StringItem.ToString(), out dateTime))
                {
                    dateTime = dateTime.AddYears(int.Parse(year));
                    dateTime = dateTime.AddMonths(int.Parse(month));
                    dateTime = dateTime.AddDays(int.Parse(day));
                    dateTime = dateTime.AddHours(int.Parse(hour));
                    dateTime = dateTime.AddMinutes(int.Parse(minute));
                    dateTime = dateTime.AddSeconds(int.Parse(second));
                }
            }

            return dateTime.ToString();
        }

        /// <summary>
        /// Evaluates the script word %DATAMODULE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DataModule(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            return parameters.GetStringAtIndex(0);
        }

        /// <summary>
        /// Evaluates the script word %DATAMODULE.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DataModule_Name(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String text = parameters.GetStringAtIndex(0);

            if (scriptWord.Parent?.StringItem != null)
            {
                if (scope?.Context != null)
                {
                    Hashtable dataModuleInstances = (Hashtable)scope.Context.GetSystemItem("DatabaseNames");
                    if (dataModuleInstances?.Contains(scriptWord.Parent.StringItem.ToUpper()) == true)
                        text += dataModuleInstances[scriptWord.Parent.StringItem.ToUpper()];
                }
            }

            return text;
        }

        #endregion

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Evaluates the script word $(EMPTY).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_GetEmpty(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            return "\"\"";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_TAB).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_Literal_Tab(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            return "\t";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_CR).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_Literal_Cr(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            return "\n";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_CARRETPOS).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_Literal_CarretPos(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            return "%LITERAL_CARRETPOS()";
        }

        // Folders -------------------------------------------

        /// <summary>
        /// Evaluates the script word $(application.folderPath).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword("application.folderpath")]
        public static string Var_ApplicationFolderPath(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            if (scope == null)
                return "<!--Application scope missing-->";
            IBdoHost appHost = scope.Context.GetSystemItem("appHost") as IBdoHost;
            if (appHost == null)
                return "<!--Application manager missing-->";

            return appHost.GetKnownPath(BdoHostPathKind.RootFolder);
        }

        /// <summary>
        /// Evaluates the script word $(roaming.folderPath).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword("application.roaming.folderpath")]
        public static string Var_ApplicationRoamingFolderPath(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            if (scope == null)
                return "<!--Application scope missing-->";
            IBdoHost appHost = scope.Context.GetSystemItem("appHost") as IBdoHost;
            if (appHost == null)
                return "<!--Application manager missing-->";

            return appHost.GetKnownPath(BdoHostPathKind.RoamingFolder);
        }

        /// <summary>
        /// Evaluates the script word $(myDocuments.folderPath).
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "mydocuments.folderpath")]
        public static string Var_MyDocumentsFolderPath(
            IBdoScope scope,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).GetEndedString(@"\");
        }

        #endregion
    }
}