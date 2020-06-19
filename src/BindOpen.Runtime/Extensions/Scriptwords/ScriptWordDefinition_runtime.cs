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
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_AppModuleName(BdoScriptwordFunctionScope scope)
        {
            if (scope?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(scope?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            return appHost?.HostOptions.AppModule?.Name ?? string.Empty;
        }

        /// <summary>
        /// Evaluates the script word $(applicationInstanceName).
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_ApplicationInstanceName(BdoScriptwordFunctionScope scope)
        {
            if (scope?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(scope?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            return appHost?.HostOptions.HostSettings?.ApplicationInstanceName ?? string.Empty;
        }

        /// <summary>
        /// Evaluates the script word $ISEMPTY.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_IsEmpty(BdoScriptwordFunctionScope scope)
        {
            string value = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Evaluates the script word $TEXT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Text(BdoScriptwordFunctionScope scope)
        {
            string value = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            return value.ToQuoted();
        }

        /// <summary>
        /// Evaluates the script word $FORMAT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_FormatText(BdoScriptwordFunctionScope scope)
        {
            string textValue = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string formatText = scope?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            return string.Format(textValue, formatText);
        }

        /// <summary>
        /// Evaluates the script word $IF.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_If(BdoScriptwordFunctionScope scope)
        {
            string condition = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            return string.Equals(condition, "true", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Evaluates the script word $NOT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Not(BdoScriptwordFunctionScope scope)
        {
            string condition = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            return !string.Equals(condition, "true", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Evaluates the script word $OR.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Or(BdoScriptwordFunctionScope scope)
        {
            bool b = false;
            foreach (var param in scope?.Scriptword?.Parameters)
            {
                b |= string.Equals(param?.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            return b;
        }

        /// <summary>
        /// Evaluates the script word $AND.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_And(BdoScriptwordFunctionScope scope)
        {
            bool b = true;
            foreach (var param in scope?.Scriptword?.Parameters)
            {
                b &= string.Equals(param?.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            return b;
        }

        /// <summary>
        /// Evaluates the script word $XOR.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Xor(BdoScriptwordFunctionScope scope)
        {
            bool b = true;
            foreach (var param in scope?.Scriptword?.Parameters)
            {
                b ^= string.Equals(param?.ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            return b;
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_IsEqual(BdoScriptwordFunctionScope scope)
        {
            string value1 = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string value2 = scope?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "true" : "false");
        }

        /// <summary>
        /// Evaluates the script word $ISDIFFERENT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_IsDifferent(BdoScriptwordFunctionScope scope)
        {
            string value1 = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string value2 = scope?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "false" : "true");
        }

        /// <summary>
        /// Evaluates the script word $(GETCURRENTDATETIME).
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_GetCurrentDateTime()
        {
            return DateTime.Now.ToString();
        }

        /// <summary>
        /// Evaluates the script word $(GETCURRENTDATE).FORMAT(aFormat).
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_DateTime_Format(BdoScriptwordFunctionScope scope)
        {
            string format = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            format = BdoScriptParsingHelper.GetValueFromText(format);
            DateTime dateTime = DateTime.Now;

            if ((scope?.Scriptword.Parent != null) && (scope?.Scriptword.Parent.Item != null))
            {
                if (!DateTime.TryParse(scope?.Scriptword.Parent.Item.ToString(), out dateTime))
                    dateTime = DateTime.Now;
            }

            return dateTime.ToString(format);
        }

        /// <summary>
        /// Evaluates the script word $(GETCURRENTDATE).TIMESTAMP().
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_DateTime_TimeStamp(BdoScriptwordFunctionScope scope)
        {
            DateTime dateTime = DateTime.Now;

            if ((scope?.Scriptword.Parent != null) && (scope?.Scriptword.Parent.Item != null))
            {
                if (!DateTime.TryParse(scope?.Scriptword.Parent.Item.ToString(), out dateTime))
                    dateTime = DateTime.Now;
            }

            return dateTime.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// Evaluates the script word $(GETCURRENTDATE).SUBSTRACT(aYear, aMonth, aDay, aHour, aMinute, aSecond).
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_DateTime_Add(BdoScriptwordFunctionScope scope)
        {
            string year = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string month = scope?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();
            string day = scope?.Scriptword?.Parameters?.GetObjectAtIndex(2)?.ToString();
            string hour = scope?.Scriptword?.Parameters?.GetObjectAtIndex(3)?.ToString();
            string minute = scope?.Scriptword?.Parameters?.GetObjectAtIndex(4)?.ToString();
            string second = scope?.Scriptword?.Parameters?.GetObjectAtIndex(5)?.ToString();

            DateTime dateTime = DateTime.Now;

            if ((scope?.Scriptword.Parent != null) && (scope?.Scriptword.Parent.Item != null))
            {
                if (DateTime.TryParse(scope?.Scriptword.Parent.Item.ToString(), out dateTime))
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
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_DataModule(BdoScriptwordFunctionScope scope)
        {
            return scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
        }

        /// <summary>
        /// Evaluates the script word %DATAMODULE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_DataModule_Name(BdoScriptwordFunctionScope scope)
        {
            string text = scope?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();

            if (scope?.Scriptword.Parent?.Item != null)
            {
                if (scope.Scope?.Context != null)
                {
                    var datamoduleName = scope?.Scriptword.Parent.Item.ToString().ToUpper();

                    Hashtable dataModuleInstances = (Hashtable)scope.Scope.Context.GetSystemItem("DatabaseNames");
                    if (dataModuleInstances?.Contains(datamoduleName) == true)
                        text += dataModuleInstances[datamoduleName];
                }
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $(EMPTY).
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_GetEmpty()
        {
            return "''";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_TAB).
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_Literal_Tab()
        {
            return "\t";
        }

        /// <summary>
        /// Evaluates the script word $(LITERAL_CR).
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_Literal_Cr()
        {
            return "\n";
        }

        // Folders -------------------------------------------

        /// <summary>
        /// Evaluates the script word $(application.folderPath).
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword("application.folderpath")]
        public static object Var_ApplicationFolderPath(BdoScriptwordFunctionScope scope)
        {
            if (scope?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(scope?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            return appHost.GetKnownPath(BdoHostPathKind.RootFolder) ?? string.Empty;
        }

        /// <summary>
        /// Evaluates the script word $(roaming.folderPath).
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword("application.roaming.folderpath")]
        public static object Var_ApplicationRoamingFolderPath(BdoScriptwordFunctionScope scope)
        {
            if (scope?.Scope?.Context == null)
                return "<!--Application scope missing-->";
            if (!(scope?.Scope.Context.GetSystemItem("bdoHost") is IBdoHost appHost))
                return "<!--BindOpen host missing-->";

            return appHost.GetKnownPath(BdoHostPathKind.RoamingFolder) ?? string.Empty;
        }

        /// <summary>
        /// Evaluates the script word $(myDocuments.folderPath).
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "mydocuments.folderpath")]
        public static object Var_MyDocumentsFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).EndingWith(@"\");
        }
    }
}