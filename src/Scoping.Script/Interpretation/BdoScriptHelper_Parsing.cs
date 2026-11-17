using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Provides utility methods for parsing, indexing, and identifying script tokens within raw strings.
/// </summary>
public static partial class BdoScriptHelper
{
    // ------------------------------------------
    // CONSTANTS
    // ------------------------------------------

    #region Constants

    /// <summary>The marker identifying the start of a variable (e.g., "$(").</summary>
    public static readonly string Symbol_Var = "$(";

    /// <summary>The marker identifying the start of a function or shorthand variable (e.g., "$").</summary>
    public static readonly string Symbol_Fun = "$";

    /// <summary>Standard syntax characters used to delimit script components.</summary>
    public static readonly string[] SyntaxItems = { "-", ">", "(", ")", "," };

    #endregion

    // ------------------------------------------
    // VALUE EXTRACTION
    // ------------------------------------------

    #region Values

    /// <summary>
    /// Extracts the raw value from a script literal, handling single quotes and escaped characters.
    /// </summary>
    /// <param name="text">The raw text segment from the script.</param>
    /// <returns>A cleaned string value.</returns>
    public static string GetValueFromScript(this string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        if (text == "''") return string.Empty;

        // Unescape double-single quotes: '' -> '
        text = text.Replace("''", "'");

        // Remove surrounding single quotes if present
        if (text.Length > 1 && text.StartsWith("'") && text.EndsWith("'"))
        {
            text = text[1..^1];
        }
        return text;
    }

    #endregion

    // ------------------------------------------
    // SMART INDEXING (Context-Aware)
    // ------------------------------------------

    #region Indexes

    /// <summary>
    /// Finds the index of a string while respecting BOSL punctuation (skipping over content inside quotes or parentheses).
    /// </summary>
    public static int IndexOfScript(this string st, string stv, int indexDeb)
    {
        int index = indexDeb;
        while (index < st.Length)
        {
            string current = st.Substring(index, 1);

            // If we hit a quote, jump to the end of the string literal
            if (current == "'" && stv != "'")
            {
                index = st.IndexOfScript("'", index + 1) + 1;
            }
            // If we hit a parenthesis, jump to the closing one
            else if (current == "(" && stv != "(")
            {
                index = st.IndexOfScript(")", index + 1) + 1;
            }
            // Match found
            else if (current == stv) return index;
            // Logical break for parameter lists
            else if (current == ")" && stv == ",") return index;
            else index++;
        }
        return index;
    }

    #endregion

    // ------------------------------------------
    // ITEM DISCOVERY (Tokenization)
    // ------------------------------------------

    #region Items

    /// <summary>
    /// Scans a script string and produces a flat list of <see cref="ScriptItem"/> tokens.
    /// </summary>
    public static List<ScriptItem> FindScriptItems(this string script)
    {
        List<ScriptItem> scriptItems = new();
        int index = 0;
        int lastNoneIndex = index;

        while (index < script.Length)
        {
            string currentChar = script.Substring(index, 1);

            // 1. Handle String Literals
            if (currentChar == "'")
            {
                AddNoneItem(script, scriptItems, lastNoneIndex, index);
                int nextIndex = script.IndexOfScript("'", index + 1);
                string content = script.Substring(index, nextIndex - index + 1);
                scriptItems.Add(content.FindScriptItem(index));
                index = nextIndex + 1;
                lastNoneIndex = index;
            }
            // 2. Handle Symbols ($ and #)
            else if (currentChar == Symbol_Fun || currentChar == "#")
            {
                int nextIndex = script.IndexOfScript("(", index);
                string content = script.Substring(index, nextIndex - index + 1);
                ScriptItem item = content.FindScriptItem(index);

                if (item.Kind != ScriptTokenKinds.None)
                {
                    AddNoneItem(script, scriptItems, lastNoneIndex, index);
                    scriptItems.Add(item);
                    index = (item.Kind == ScriptTokenKinds.Literal) ? nextIndex + 2 : nextIndex;
                    lastNoneIndex = index;
                }
                else index++;
            }
            // 3. Handle Syntax
            else if (SyntaxItems.Contains(currentChar))
            {
                AddNoneItem(script, scriptItems, lastNoneIndex, index);
                scriptItems.Add(currentChar.FindScriptItem(index));
                index++;
                lastNoneIndex = index;
            }
            else index++;
        }
        AddNoneItem(script, scriptItems, lastNoneIndex, index);
        return scriptItems;
    }

    /// <summary>
    /// Identifies and creates a script item from the specified string fragment.
    /// </summary>
    /// <param name="name">The string fragment to analyze.</param>
    /// <param name="index">The character index where this fragment starts in the source script.</param>
    /// <returns>Returns a <see cref="ScriptItem"/> identifying the token kind and its name.</returns>
    /// <remarks>
    /// This method uses pattern matching (symbols like $, #, or quotes) to categorize the 
    /// string into tokens used by the parser.
    /// </remarks>
    private static ScriptItem FindScriptItem(this string name, int index)
    {
        name = name.Trim();

        // Initialize the default script item as 'None'
        ScriptItem scriptItem = new(
            ScriptTokenKinds.None,
            name,
            index);

        // 1. Check if it's a Text Literal (starts with a single quote)
        if ((name.Length >= 1) && (name[0] == '\''))
        {
            scriptItem.Kind = ScriptTokenKinds.Text;
            scriptItem.WithName(name);
        }
        // 2. Check if it's a Variable (starts with Symbol_Fun '$' and ends with an opening parenthesis for name capture)
        // Note: In BOSL, variables are often identified as $(varName) or $varName
        else if ((name.Length >= 2) && (name.StartsWith(Symbol_Fun) && name.EndsWith("(")))
        {
            // Validate that the inner content contains only alphanumeric characters or underscores
            bool isValid = true;
            foreach (char ch in name[1..^1])
            {
                isValid = char.IsLetterOrDigit(ch) || (ch == '_');
                if (!isValid) break;
            }

            if (isValid)
            {
                scriptItem.WithName(name[0..^1]); // Remove the trailing '('

                // Distinguish between standard variables and literal-prefixed functions
                if (scriptItem.Name.ToUpper().StartsWith("$LITERAL_"))
                    scriptItem.Kind = ScriptTokenKinds.Literal;
                else
                    scriptItem.Kind = ScriptTokenKinds.Variable;
            }
        }
        // 3. Check if it's a Function (starts with '#' and ends with '(')
        else if ((name.Length >= 2) && (name.StartsWith("#") && name.EndsWith("(")))
        {
            bool isValid = true;
            foreach (char ch in name[1..^1])
            {
                isValid = char.IsLetterOrDigit(ch) || (ch == '_');
                if (!isValid) break;
            }

            if (isValid)
            {
                scriptItem.Kind = ScriptTokenKinds.Function;
                scriptItem.WithName(name[0..^1]);
            }
        }
        // 4. Check if it's a Syntax Item (matches known operators or separators)
        else if (SyntaxItems.Contains(name))
        {
            scriptItem.Kind = ScriptTokenKinds.Syntax;
            scriptItem.WithName(name);
        }

        return scriptItem;
    }

    /// <summary>
    /// Helper to capture text segments that aren't recognized as specific script tokens.
    /// </summary>
    private static void AddNoneItem(string script, List<ScriptItem> items, int start, int end)
    {
        if (start == end) return;
        string name = script[start..end];
        if (items.Count > 0 && items[^1].Kind == ScriptTokenKinds.None)
            items[^1].WithName(items[^1].Name + name);
        else
            items.Add(new ScriptItem(ScriptTokenKinds.None, name, start));
    }

    #endregion
}