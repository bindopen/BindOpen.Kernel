using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Scripting;
using System;
using System.Collections;

namespace BindOpen.Extensions.Scriptwords
{
    /// <summary>
    /// This class represents a 'Runtime' script word definition.
    /// </summary>
    [BdoScriptwordDefinition]
    public static class ScriptwordDefinition_runtime
    {
        /// <summary>
        /// Evaluates the script word $(applicationModuleName).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_AppModuleName(BdoScriptwordFunctionVariable variable)
        {
            if (variable?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(variable?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            var output = appHost?.HostOptions.AppModule?.Name;
            variable.Scriptword.Item = output;
            return output ?? "";
        }

        /// <summary>
        /// Evaluates the script word $(applicationInstanceName).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_ApplicationInstanceName(BdoScriptwordFunctionVariable variable)
        {
            if (variable?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(variable?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            var output = appHost?.HostOptions.HostSettings?.ApplicationInstanceName;
            variable.Scriptword.Item = output;
            return output ?? "";
        }

        /// <summary>
        /// Evaluates the script word $ISEMPTY.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_IsEmpty(BdoScriptwordFunctionVariable variable)
        {
            string value = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            var output = string.IsNullOrEmpty(value);
            variable.Scriptword.Item = output;
            return output ? "true" : "false";
        }

        /// <summary>
        /// Evaluates the script word $TEXT.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_Text(BdoScriptwordFunctionVariable variable)
        {
            string value = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            var output = "'" + value + "'";
            variable.Scriptword.Item = output;
            return output;
        }

        /// <summary>
        /// Evaluates the script word $FORMAT.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_FormatText(BdoScriptwordFunctionVariable variable)
        {
            string textValue = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string formatText = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            var output = string.Format(textValue, formatText);
            variable.Scriptword.Item = output;
            return output;
        }

        /// <summary>
        /// Evaluates the script word $IF.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_If(BdoScriptwordFunctionVariable variable)
        {
            string condition = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0).ToString();

            var output = string.Equals(condition, "true", StringComparison.OrdinalIgnoreCase);
            variable.Scriptword.Item = output;
            return output.ToString();
        }

        /// <summary>
        /// Evaluates the script word $NOT.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_Not(BdoScriptwordFunctionVariable variable)
        {
            string condition = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0).ToString();

            var output = !string.Equals(condition, "true", StringComparison.OrdinalIgnoreCase);
            variable.Scriptword.Item = output;
            return output.ToString();
        }

        /// <summary>
        /// Evaluates the script word $OR.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_Or(BdoScriptwordFunctionVariable variable)
        {
            bool b = false;
            foreach (var param in variable?.Scriptword?.Parameters)
            {
                b |= string.Equals(param?.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            var output = b;
            variable.Scriptword.Item = output;
            return output.ToString();
        }

        /// <summary>
        /// Evaluates the script word $AND.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_And(BdoScriptwordFunctionVariable variable)
        {
            bool b = true;
            foreach (var param in variable?.Scriptword?.Parameters)
            {
                b &= string.Equals(param?.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            var output = b;
            variable.Scriptword.Item = output;
            return output.ToString();
        }

        /// <summary>
        /// Evaluates the script word $XOR.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_Xor(BdoScriptwordFunctionVariable variable)
        {
            bool b = true;
            foreach (var param in variable?.Scriptword?.Parameters)
            {
                b ^= string.Equals(param?.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            var output = b;
            variable.Scriptword.Item = output;
            return output.ToString();
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_IsEqual(BdoScriptwordFunctionVariable variable)
        {
            string value1 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0).ToString();
            string value2 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1).ToString();

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "true" : "false");
        }

        /// <summary>
        /// Evaluates the script word $ISDIFFERENT.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_IsDifferent(BdoScriptwordFunctionVariable variable)
        {
            String value1 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0).ToString();
            String value2 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1).ToString();

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "false" : "true");
        }

        /// <summary>
        /// Evaluates the script word $(GETCURRENTDATETIME).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_GetCurrentDateTime(BdoScriptwordFunctionVariable variable)
        {
            return DateTime.Now.ToString();
        }


        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().FORMAT(aFormat).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DateTime_Format(BdoScriptwordFunctionVariable variable)
        {
            String aFormat = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0).ToString();

            aFormat = BdoScriptParsingHelper.GetValueFromText(aFormat);
            DateTime dateTime = DateTime.Now;

            if ((variable?.Scriptword.Parent != null) && (variable?.Scriptword.Parent.StringItem != null))
            {
                if (!DateTime.TryParse(variable?.Scriptword.Parent.StringItem.ToString(), out dateTime))
                    dateTime = DateTime.Now;
            }

            return dateTime.ToString(aFormat);
        }

        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().TIMESTAMP().
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DateTime_TimeStamp(BdoScriptwordFunctionVariable variable)
        {
            DateTime dateTime = DateTime.Now;

            if ((variable?.Scriptword.Parent != null) && (variable?.Scriptword.Parent.StringItem != null))
            {
                if (!DateTime.TryParse(variable?.Scriptword.Parent.StringItem.ToString(), out dateTime))
                    dateTime = DateTime.Now;
            }

            return dateTime.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().SUBSTRACT(aYear, aMonth, aDay, aHour, aMinute, aSecond).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DateTime_Add(BdoScriptwordFunctionVariable variable)
        {
            String year = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0).ToString();
            String month = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1).ToString();
            String day = variable?.Scriptword?.Parameters?.GetObjectAtIndex(2).ToString();
            String hour = variable?.Scriptword?.Parameters?.GetObjectAtIndex(3).ToString();
            String minute = variable?.Scriptword?.Parameters?.GetObjectAtIndex(4).ToString();
            String second = variable?.Scriptword?.Parameters?.GetObjectAtIndex(5).ToString();

            DateTime dateTime = DateTime.Now;

            if ((variable?.Scriptword.Parent != null) && (variable?.Scriptword.Parent.StringItem != null))
            {
                if (DateTime.TryParse(variable?.Scriptword.Parent.StringItem.ToString(), out dateTime))
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
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DataModule(BdoScriptwordFunctionVariable variable)
        {
            return variable?.Scriptword?.Parameters?.GetObjectAtIndex(0).ToString();
        }

        /// <summary>
        /// Evaluates the script word %DATAMODULE.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_DataModule_Name(BdoScriptwordFunctionVariable variable)
        {
            String text = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0).ToString();

            if (variable?.Scriptword.Parent?.StringItem != null)
            {
                if (scope?.Context != null)
                {
                    Hashtable dataModuleInstances = (Hashtable)scope.Context.GetSystemItem("DatabaseNames");
                    if (dataModuleInstances?.Contains(variable?.Scriptword.Parent.StringItem.ToUpper()) == true)
                        text += dataModuleInstances[variable?.Scriptword.Parent.StringItem.ToUpper()];
                }
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $(EMPTY).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_GetEmpty(BdoScriptwordFunctionVariable variable)
        {
            return "''";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_TAB).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_Literal_Tab(BdoScriptwordFunctionVariable variable)
        {
            return "\t";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_CR).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_Literal_Cr(BdoScriptwordFunctionVariable variable)
        {
            return "\n";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_CARRETPOS).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Var_Literal_CarretPos(BdoScriptwordFunctionVariable variable)
        {
            return "%LITERAL_CARRETPOS()";
        }

        // Folders -------------------------------------------

        /// <summary>
        /// Evaluates the script word $(application.folderPath).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword("application.folderpath")]
        public static string Var_ApplicationFolderPath(BdoScriptwordFunctionVariable variable)
        {
            if (variable?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(variable?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            var output = appHost.GetKnownPath(BdoHostPathKind.RootFolder);
            variable.Scriptword.Item = output;
            return output ?? "";
        }

        /// <summary>
        /// Evaluates the script word $(roaming.folderPath).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword("application.roaming.folderpath")]
        public static string Var_ApplicationRoamingFolderPath(BdoScriptwordFunctionVariable variable)
        {
            if (variable?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(variable?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            var output = appHost.GetKnownPath(BdoHostPathKind.RoamingFolder);
            variable.Scriptword.Item = output;
            return output ?? "";
        }

        /// <summary>
        /// Evaluates the script word $(myDocuments.folderPath).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "mydocuments.folderpath")]
        public static string Var_MyDocumentsFolderPath(BdoScriptwordFunctionVariable variable)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).GetEndedString(@"\");
        }
    }
}