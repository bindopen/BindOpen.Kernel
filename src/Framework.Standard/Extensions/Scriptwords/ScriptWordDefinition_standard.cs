using System;
using System.Collections;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Standard.Extensions.Scriptwords
{

    /// <summary>
    /// This class represents a 'Standard' script definition.
    /// </summary>
    public static class ScriptwordDefinition_standard
    {

        // ------------------------------------------
        // FUNCTIONS
        // ------------------------------------------

        #region Functions

        /// <summary>
        /// Evaluates the script word $ISEMPTY.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_IsEmpty(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);

            if ((value1 == "\"\"") | (value1 == String.Empty))
                return "%TRUE()";
            else
                return "%FALSE()";
        }

        /// <summary>
        /// Evaluates the script word $TEXT.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_Text(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            return "\"" + value1 + "\"";
        }

        /// <summary>
        /// Evaluates the script word $FORMAT.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_FormatText(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String textValue = parameters.GetStringAtIndex(0);
            String aFormatText = parameters.GetStringAtIndex(1);

            return String.Format(textValue, aFormatText);
        }

        /// <summary>
        /// Evaluates the script word $IF.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_If(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String aCondition = parameters.GetStringAtIndex(0);

            if (string.Equals(aCondition, "%TRUE()", StringComparison.OrdinalIgnoreCase))
                return parameters.GetStringAtIndex(1);
            else
                return parameters.GetStringAtIndex(2);
        }

        /// <summary>
        /// Evaluates the script word $NOT.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_Not(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            return (!string.Equals(value1, "%TRUE()", StringComparison.OrdinalIgnoreCase) ? "%True()" : "%False()");
        }

        /// <summary>
        /// Evaluates the script word $OR.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_Or(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            Boolean b = false;
            foreach (String st in parameters)
                b = (b | (string.Equals(st, "%TRUE()", StringComparison.OrdinalIgnoreCase)));
            return (b ? "%TRUE()" : "%FALSE()");
        }

        /// <summary>
        /// Evaluates the script word $AND.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_And(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            Boolean b = true;
            foreach (String st in parameters)
                b = (b & (string.Equals(st, "%TRUE()", StringComparison.OrdinalIgnoreCase)));
            return (b ? "%TRUE()" : "%FALSE()");
        }

        /// <summary>
        /// Evaluates the script word $XOR.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_Xor(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            Boolean b = true;
            foreach (String st in parameters)
                b = (b ^ (string.Equals(st, "%TRUE()", StringComparison.OrdinalIgnoreCase)));
            return (b ? "%TRUE()" : "%FALSE()");
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_IsEqual(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "%TRUE()" : "%FALSE()");
        }

        /// <summary>
        /// Evaluates the script word $ISDIFFERENT.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_IsDifferent(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "%FALSE()" : "%TRUE()");
        }

        /// <summary>
        /// Evaluates the script word $(GETCURRENTDATETIME).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_GetCurrentDateTime(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            return DateTime.Now.ToString();
        }


        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().FORMAT(aFormat).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_DateTime_Format(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String aFormat = parameters.GetStringAtIndex(0);

            aFormat = ScriptParsingHelper.GetValueFromText(aFormat);
            DateTime dateTime = System.DateTime.Now;

            if ((scriptWord.Parent != null) && (scriptWord.Parent.StringItem != null))
            {
                if (!DateTime.TryParse(scriptWord.Parent.StringItem.ToString(), out dateTime))
                    dateTime = System.DateTime.Now;
            }

            return dateTime.ToString(aFormat);
        }

        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().TIMESTAMP().
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_DateTime_TimeStamp(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            DateTime dateTime = System.DateTime.Now;

            if ((scriptWord.Parent != null) && (scriptWord.Parent.StringItem != null))
            {
                if (!DateTime.TryParse(scriptWord.Parent.StringItem.ToString(), out dateTime))
                    dateTime = System.DateTime.Now;
            }

            return dateTime.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().SUBSTRACT(aYear, aMonth, aDay, aHour, aMinute, aSecond).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_DateTime_Add(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String year = parameters.GetStringAtIndex(0);
            String month = parameters.GetStringAtIndex(1);
            String day = parameters.GetStringAtIndex(2);
            String hour = parameters.GetStringAtIndex(3);
            String minute = parameters.GetStringAtIndex(4);
            String second = parameters.GetStringAtIndex(5);

            DateTime dateTime = System.DateTime.Now;

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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_DataModule(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            return parameters.GetStringAtIndex(0);
        }

        /// <summary>
        /// Evaluates the script word %DATAMODULE.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Fun_DataModule_Name(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            String text = parameters.GetStringAtIndex(0);

            if (scriptWord.Parent?.StringItem != null)
            {
                if (appScope?.DataContext != null)
                {
                    Hashtable dataModuleInstances = (Hashtable)appScope.DataContext.GetSystemItem("DatabaseNames");
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_GetEmpty(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            return "\"\"";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_TAB).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_Literal_Tab(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            return "\t";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_CR).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_Literal_Cr(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            return "\n";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_CARRETPOS).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_Literal_CarretPos(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            return "%LITERAL_CARRETPOS()";
        }

        /// <summary>
        /// Evaluates the script word $(roaming.folderPath).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_RoamingFolderPath(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).GetEndedString(@"\");
        }

        /// <summary>
        /// Evaluates the script word $(myDocuments.folderPath).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_MyDocumentsFolderPath(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            Scriptword scriptWord,
            params object[] parameters)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).GetEndedString(@"\");
        }

        #endregion
    }
}