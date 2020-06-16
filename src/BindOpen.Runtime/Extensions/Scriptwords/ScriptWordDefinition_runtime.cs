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
        public static object Var_AppModuleName(BdoScriptwordFunctionVariable variable)
        {
            if (variable?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(variable?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            return appHost?.HostOptions.AppModule?.Name ?? "";
        }

        /// <summary>
        /// Evaluates the script word $(applicationInstanceName).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_ApplicationInstanceName(BdoScriptwordFunctionVariable variable)
        {
            if (variable?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(variable?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            return appHost?.HostOptions.HostSettings?.ApplicationInstanceName ?? "";
        }

        /// <summary>
        /// Evaluates the script word $ISEMPTY.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_IsEmpty(BdoScriptwordFunctionVariable variable)
        {
            string value = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Evaluates the script word $TEXT.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Text(BdoScriptwordFunctionVariable variable)
        {
            string value = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            return "'" + value + "'";
        }

        /// <summary>
        /// Evaluates the script word $FORMAT.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_FormatText(BdoScriptwordFunctionVariable variable)
        {
            string textValue = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string formatText = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            return string.Format(textValue, formatText);
        }

        /// <summary>
        /// Evaluates the script word $IF.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_If(BdoScriptwordFunctionVariable variable)
        {
            string condition = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            return string.Equals(condition, "true", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Evaluates the script word $NOT.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Not(BdoScriptwordFunctionVariable variable)
        {
            string condition = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            return !string.Equals(condition, "true", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Evaluates the script word $OR.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Or(BdoScriptwordFunctionVariable variable)
        {
            bool b = false;
            foreach (var param in variable?.Scriptword?.Parameters)
            {
                b |= string.Equals(param?.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            return b;
        }

        /// <summary>
        /// Evaluates the script word $AND.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_And(BdoScriptwordFunctionVariable variable)
        {
            bool b = true;
            foreach (var param in variable?.Scriptword?.Parameters)
            {
                b &= string.Equals(param?.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            return b;
        }

        /// <summary>
        /// Evaluates the script word $XOR.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Xor(BdoScriptwordFunctionVariable variable)
        {
            bool b = true;
            foreach (var param in variable?.Scriptword?.Parameters)
            {
                b ^= string.Equals(param?.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            return b;
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_IsEqual(BdoScriptwordFunctionVariable variable)
        {
            string value1 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string value2 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "true" : "false");
        }

        /// <summary>
        /// Evaluates the script word $ISDIFFERENT.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_IsDifferent(BdoScriptwordFunctionVariable variable)
        {
            string value1 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string value2 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "false" : "true");
        }

        /// <summary>
        /// Evaluates the script word $(GETCURRENTDATETIME).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_GetCurrentDateTime(BdoScriptwordFunctionVariable variable)
        {
            return DateTime.Now.ToString();
        }


        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().FORMAT(aFormat).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_DateTime_Format(BdoScriptwordFunctionVariable variable)
        {
            string format = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            format = BdoScriptParsingHelper.GetValueFromText(format);
            DateTime dateTime = DateTime.Now;

            if ((variable?.Scriptword.Parent != null) && (variable?.Scriptword.Parent.Item != null))
            {
                if (!DateTime.TryParse(variable?.Scriptword.Parent.Item.ToString(), out dateTime))
                    dateTime = DateTime.Now;
            }

            return dateTime.ToString(format);
        }

        /// <summary>
        /// Evaluates the script word $GETCURRENTDATE().TIMESTAMP().
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_DateTime_TimeStamp(BdoScriptwordFunctionVariable variable)
        {
            DateTime dateTime = DateTime.Now;

            if ((variable?.Scriptword.Parent != null) && (variable?.Scriptword.Parent.Item != null))
            {
                if (!DateTime.TryParse(variable?.Scriptword.Parent.Item.ToString(), out dateTime))
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
        public static object Fun_DateTime_Add(BdoScriptwordFunctionVariable variable)
        {
            string year = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string month = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();
            string day = variable?.Scriptword?.Parameters?.GetObjectAtIndex(2)?.ToString();
            string hour = variable?.Scriptword?.Parameters?.GetObjectAtIndex(3)?.ToString();
            string minute = variable?.Scriptword?.Parameters?.GetObjectAtIndex(4)?.ToString();
            string second = variable?.Scriptword?.Parameters?.GetObjectAtIndex(5)?.ToString();

            DateTime dateTime = DateTime.Now;

            if ((variable?.Scriptword.Parent != null) && (variable?.Scriptword.Parent.Item != null))
            {
                if (DateTime.TryParse(variable?.Scriptword.Parent.Item.ToString(), out dateTime))
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
        public static object Fun_DataModule(BdoScriptwordFunctionVariable variable)
        {
            return variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
        }

        /// <summary>
        /// Evaluates the script word %DATAMODULE.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_DataModule_Name(BdoScriptwordFunctionVariable variable)
        {
            string text = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            if (variable?.Scriptword.Parent?.Item != null)
            {
                if (variable.Scope?.Context != null)
                {
                    var datamoduleName = variable?.Scriptword.Parent.Item.ToString().ToUpper();

                    Hashtable dataModuleInstances = (Hashtable)variable.Scope.Context.GetSystemItem("DatabaseNames");
                    if (dataModuleInstances?.Contains(datamoduleName) == true)
                        text += dataModuleInstances[datamoduleName];
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
        public static object Var_GetEmpty(BdoScriptwordFunctionVariable variable)
        {
            return "''";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_TAB).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_Literal_Tab(BdoScriptwordFunctionVariable variable)
        {
            return "\t";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_CR).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_Literal_Cr(BdoScriptwordFunctionVariable variable)
        {
            return "\n";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_CARRETPOS).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_Literal_CarretPos(BdoScriptwordFunctionVariable variable)
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
        public static object Var_ApplicationFolderPath(BdoScriptwordFunctionVariable variable)
        {
            if (variable?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(variable?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            return appHost.GetKnownPath(BdoHostPathKind.RootFolder) ?? "";
        }

        /// <summary>
        /// Evaluates the script word $(roaming.folderPath).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword("application.roaming.folderpath")]
        public static object Var_ApplicationRoamingFolderPath(BdoScriptwordFunctionVariable variable)
        {
            if (variable?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(variable?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            return appHost.GetKnownPath(BdoHostPathKind.RoamingFolder) ?? "";
        }

        /// <summary>
        /// Evaluates the script word $(myDocuments.folderPath).
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "mydocuments.folderpath")]
        public static object Var_MyDocumentsFolderPath(BdoScriptwordFunctionVariable variable)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).GetEndedString(@"\");
        }
    }
}