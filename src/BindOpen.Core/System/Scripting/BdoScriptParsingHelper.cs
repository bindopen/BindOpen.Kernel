using BindOpen.Extensions.Definition;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using System;
using System.Collections.Generic;

namespace BindOpen.System.Scripting
{
    /// <summary>
    /// This class represents a string parser.
    /// </summary>
    public static class BdoScriptParsingHelper
    {
        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        /// <summary>
        /// The leading character of the variable.
        /// </summary>
        public static readonly string Symbol_Var = "$(";

        /// <summary>
        /// The leading character of the function.
        /// </summary>
        public static readonly string Symbol_Fun = "$";

        /// <summary>
        /// The syntax items of this class.
        /// </summary>
        public static readonly string[] SyntaxItems = { "-", ">", "(", ")", "," };

        #endregion

        // ------------------------------------------
        // VALUES
        // ------------------------------------------

        #region Values

        /// <summary>
        /// Returns the string value from a text.
        /// </summary>
        /// <param name="text">The text from which the string value is retrieved.</param>
        /// <returns>The string value from a text</returns>
        public static string GetValueFromText(this string text)
        {
            if (text == "''")
            {
                text = string.Empty;
            }
            else
            {
                text = text?.Replace("''", "'");
                if (text?.Length > 1)
                {
                    if ((text.Substring(0, 1) == "'") && (text.Substring(text.Length - 1, 1) == "'"))
                    {
                        text = text[1..^1];
                    }
                }
            }
            return text;
        }

        /// <summary>
        /// Returns the parameter value included into the specified text.
        /// </summary>
        /// <remarks>The inluding text must be formated this way: parameter1="value";parameter2="value".</remarks>
        /// <param name="stringValue">The string to parse.</param>
        /// <param name="parameterName">Name of the parameter to consider.</param>
        /// <param name="isMatchCase">Indicates whether the search is case sensitive.</param>
        /// <returns>The value of the specified parameter.</returns>
        public static string GetParameterValue(this string stringValue, string parameterName, bool isMatchCase = false)
        {
            int startIndex;
            int endIndex;
            if (isMatchCase)
            {
                startIndex = stringValue.IndexOf(parameterName + "='") + (parameterName + "='").Length;
                endIndex = BdoScriptParsingHelper.GetIndexOfNextString(stringValue, "'", startIndex + 1);
            }
            else
            {
                startIndex = stringValue.ToUpper().IndexOf(parameterName.ToUpper() + "='") + (parameterName + "='").Length;
                endIndex = BdoScriptParsingHelper.GetIndexOfNextString(stringValue, "'", startIndex + 1);
            }

            return stringValue.Substring(startIndex, endIndex - startIndex + 1);
        }

        #endregion

        // ------------------------------------------
        // INDEXES
        // ------------------------------------------

        #region Indexes

        /// <summary>
        /// Returns the position of stv in st from index taking in account the closing ponctuation (,{,[. 
        /// </summary>
        /// <param name="st">The string to parse.</param>
        /// <param name="stv">The string to look for.</param>
        /// <param name="indexDeb">The start index.</param>
        /// <returns>The position of the next stv into st from index position.</returns>
        public static int GetIndexOfNextString(string st, string stv, int indexDeb)
        {
            int index = indexDeb;
            bool b = false;
            while ((index < st.Length) && !b)
            {
                if ((st.Substring(index, 1) == "'") && (stv != "'"))
                    index = BdoScriptParsingHelper.GetIndexOfNextString(st, "'", index + 1) + 1;
                else if ((st.Substring(index, 1) == "(") && (stv != "("))
                    index = BdoScriptParsingHelper.GetIndexOfNextString(st, ")", index + 1) + 1;
                else if (st.Substring(index, 1) == stv)
                    b = true;
                else if ((st.Substring(index, 1) == ")") && (stv == ","))
                    b = true;
                else
                    index++;
            }
            return index;
        }

        /// <summary>
        /// Returns the last position of stv in st from index taking in account the closing ponctuation (,{,[. 
        /// </summary>
        /// <param name="st">The string to parse.</param>
        /// <param name="stv">The string to look for.</param>
        /// <param name="indexDeb">The start index.</param>
        /// <returns>The position of the next stv into st from index position.</returns>
        public static int GetIndexOfLastString(string st, string stv, int indexDeb)
        {
            int index = indexDeb;
            bool b = false;
            while ((index >= 0) && !b)
            {
                if ((st.Substring(index, 1) == "'") && (stv != "'"))
                    index = GetIndexOfLastString(st, "'", index - 1) - 1;
                else if ((st.Substring(index, 1) == ")") && (stv != ")"))
                    index = GetIndexOfLastString(st, "(", index - 1) - 1;
                else if (st.Substring(index, 1) == stv)
                    b = true;
                else if ((st.Substring(index, 1) == ")") && (stv == ","))
                    b = true;
                else
                    index--;
            }
            return index;
        }

        #endregion

        // ------------------------------------------
        // ITEMS
        // ------------------------------------------

        #region Items

        /// <summary>
        /// Returns the items that are in the specified script.
        /// </summary>
        /// <param name="script">The script to analyse.</param>
        /// <returns>The items that are in the specified script.</returns>
        public static List<ScriptItem> FindScriptItems(string script)
        {
            List<ScriptItem> scriptItems = new List<ScriptItem>();
            int index = 0;
            int lastNoneIndex = index;

            while (index < script.Length)
            {
                string currentChar = script.Substring(index, 1);

                ScriptItem scriptItem;
                int nextIndex;
                // if the current char is double quote
                if (currentChar == "'")
                {
                    string scriptItemName;

                    // if something between the last none index and the current then
                    if (lastNoneIndex != index)
                    {
                        // we declare this something as a none-kind script item
                        scriptItemName = script[lastNoneIndex..index];
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKinds.None))
                        {
                            scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                        }
                        else
                        {
                            scriptItems.Add(new ScriptItem(
                               ScriptItemKinds.None,
                               scriptItemName,
                               index));
                        }
                    }

                    // then we look for the next double quote
                    nextIndex = BdoScriptParsingHelper.GetIndexOfNextString(script, "'", index + 1);
                    scriptItemName = script.Substring(index, nextIndex - index + 1);
                    scriptItem = BdoScriptParsingHelper.FindScriptItem(scriptItemName, index);
                    if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                        scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                    else
                        scriptItems.Add(scriptItem);
                    index = nextIndex + 1;
                    lastNoneIndex = index;
                }
                // else if it is a "$" (start of variable item)
                else if (currentChar == BdoScriptParsingHelper.Symbol_Fun)
                {
                    // then we look for the next "(" (end of variable item)
                    nextIndex = BdoScriptParsingHelper.GetIndexOfNextString(script, "(", index);
                    string scriptItemName = script.Substring(index, nextIndex - index + 1);
                    scriptItem = BdoScriptParsingHelper.FindScriptItem(scriptItemName, index);
                    if (scriptItem.Kind == ScriptItemKinds.Variable)
                    {
                        // if something between the last none index and the current then
                        if (lastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            scriptItemName = script[lastNoneIndex..index];
                            if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKinds.None))
                            {
                                scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                            }
                            else
                            {
                                scriptItems.Add(new ScriptItem(
                               ScriptItemKinds.None,
                               scriptItemName,
                               index));
                            }
                        }

                        // we record the script item
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                            scriptItems[scriptItems.Count - 1].Name += scriptItem.Name;
                        else
                            scriptItems.Add(scriptItem);
                        index = nextIndex;
                        lastNoneIndex = index;
                    }
                    // if it is a literal script word then
                    else if (scriptItem.Kind == ScriptItemKinds.Literal)
                    {
                        // we insert "()" in the item name and we update the index
                        scriptItem.Name += "()";
                        nextIndex += 2;

                        // if something between the last none index and the current then
                        if (lastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            scriptItemName = script[lastNoneIndex..index];
                            if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKinds.None))
                            {
                                scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                            }
                            else
                            {
                                scriptItems.Add(
                                   new ScriptItem(
                                       ScriptItemKinds.None,
                                       scriptItemName,
                                       index));
                            }
                        }

                        // we record the script item
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                            scriptItems[scriptItems.Count - 1].Name += scriptItem.Name;
                        else
                            scriptItems.Add(scriptItem);

                        index = nextIndex;
                        lastNoneIndex = index;
                    }
                    else
                    {
                        index += 1;
                    }
                }
                // else if it is a "#" (start of function item)
                else if (currentChar == "#")
                {
                    // then we look for the next "(" (end of variable item)
                    nextIndex = BdoScriptParsingHelper.GetIndexOfNextString(script, "(", index);
                    string scriptItemName = script.Substring(index, nextIndex - index + 1);
                    scriptItem = BdoScriptParsingHelper.FindScriptItem(scriptItemName, index);
                    if (scriptItem.Kind == ScriptItemKinds.Function)
                    {
                        // if something between the last none index and the current then
                        if (lastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            scriptItemName = script[lastNoneIndex..index];
                            if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKinds.None))
                            {
                                scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                            }
                            else
                            {
                                scriptItems.Add(
                                   new ScriptItem(
                                       ScriptItemKinds.None,
                                       scriptItemName,
                                       index));
                            }
                        }

                        // we record the script item
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                            scriptItems[scriptItems.Count - 1].Name += scriptItem.Name;
                        else
                            scriptItems.Add(scriptItem);
                        index = nextIndex;
                        lastNoneIndex = index;
                    }
                    else
                    {
                        index++;
                    }
                }
                // else
                else
                {
                    // we retrieve the current item
                    scriptItem = BdoScriptParsingHelper.FindScriptItem(currentChar, index);

                    // if it is a syntax item
                    if (scriptItem.Kind == ScriptItemKinds.Syntax)
                    {

                        // if something between the last none index and the current then
                        if (lastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            string scriptItemName = script[lastNoneIndex..index];
                            if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKinds.None))
                            {
                                scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                            }
                            else
                            {
                                scriptItems.Add(
                                   new ScriptItem(
                                       ScriptItemKinds.None,
                                       scriptItemName,
                                       index));
                            }
                        }

                        scriptItem = BdoScriptParsingHelper.FindScriptItem(currentChar, index);
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                            scriptItems[scriptItems.Count - 1].Name += scriptItem.Name;
                        else
                            scriptItems.Add(scriptItem);

                        index += scriptItem.Name.Length;
                        lastNoneIndex = index;
                    }
                    else
                    {
                        // in any remaning case, we increment the index
                        index++;
                    }
                }
            }

            // if something between the last none index and the current then
            if (lastNoneIndex != index)
            {
                // we declare this something as a none-kind script item
                string scriptItemName = script[lastNoneIndex..index];
                if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKinds.None))
                {
                    scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                }
                else
                {
                    scriptItems.Add(
                       new ScriptItem(
                           ScriptItemKinds.None,
                           scriptItemName,
                           index));
                }
            }

            return scriptItems;
        }

        // Returns the script item with the specified name from the specified index.
        private static ScriptItem FindScriptItem(string name, int index)
        {
            name = name.Trim();

            // we define the default script item to return
            ScriptItem scriptItem = new ScriptItem(
                ScriptItemKinds.None,
                name,
                index);

            // if it is a text
            if ((name.Length >= 1) && (name[0] == '\''))
            {
                scriptItem.Index = index;
                scriptItem.Kind = ScriptItemKinds.Text;
                scriptItem.Name = name;
            }
            // else if it is a variable
            else if ((name.Length >= 2) && ((name.Substring(0, 1) == BdoScriptParsingHelper.Symbol_Fun) & (name[^1] == '(')))
            {
                // we check that the name has only letters, numbers and underscore
                bool isGood = false;
                foreach (char ch in name[1..^1])
                {
                    isGood = char.IsLetter(ch) || char.IsNumber(ch) || (ch == '_');
                    if (!isGood)
                        break;
                }
                if (isGood)
                {
                    scriptItem.Index = index;
                    scriptItem.Name = name[0..^1];
                    // we indentify the literal script functions
                    if (scriptItem.Name.ToUpper().Substring(0, "$LITERAL_".Length) == "$LITERAL_")
                        scriptItem.Kind = ScriptItemKinds.Literal;
                    else
                        scriptItem.Kind = ScriptItemKinds.Variable;
                }
            }
            // else if it is a function
            else if ((name.Length >= 2) && ((name[0] == '#') && (name[^1] == '(')))
            {
                // we check that the name has only letters, numbers and underscore
                bool isGood = false;
                foreach (char ch in name[1..^1])
                {
                    isGood = char.IsLetter(ch) || char.IsNumber(ch) || (ch == '_');
                    if (!isGood)
                        break;
                }
                if (isGood)
                {
                    scriptItem.Index = index;
                    scriptItem.Kind = ScriptItemKinds.Function;
                    scriptItem.Name = name[0..^1];
                }
            }
            // else if it is a syntax item
            else if ((new List<string>(BdoScriptParsingHelper.SyntaxItems)).Contains(name))
            {
                scriptItem.Index = index;
                scriptItem.Kind = ScriptItemKinds.Syntax;
                scriptItem.Name = name;
            }
            return scriptItem;
        }

        #endregion

        // ------------------------------------------
        // BLOCKS
        // ------------------------------------------

        #region Blocks

        /// <summary>
        /// Returns the first script block in the specified script from the specified index.
        /// </summary>
        /// <param name="script">The script to analyse.</param>
        /// <param name="index">The index position to begin analyse.</param>
        /// <returns></returns>
        public static string GetScriptBlock(string script, int index = 0)
        {
            int lastIndex = index;
            bool isEndFound = false;

            while (!isEndFound)
            {
                int prevSpaceIndex = BdoScriptParsingHelper.GetIndexOfLastString(script, " ", lastIndex);
                int prevVarIndex = BdoScriptParsingHelper.GetIndexOfLastString(script, BdoScriptParsingHelper.Symbol_Fun, lastIndex);
                int prevFunIndex = (lastIndex == index ? BdoScriptParsingHelper.GetIndexOfLastString(script, "#", lastIndex) : -1);
                lastIndex = Math.Max(Math.Max(Math.Max(prevSpaceIndex, prevVarIndex), prevFunIndex), 0);
                if ((lastIndex >= ("." + BdoScriptParsingHelper.Symbol_Fun).Length) && (script.Substring(lastIndex - 3, (")." + BdoScriptParsingHelper.Symbol_Fun).Length) == ")." + BdoScriptParsingHelper.Symbol_Fun))
                {
                    isEndFound = false;
                    lastIndex--;
                }
                else
                {
                    isEndFound = true;
                }
            }
            string aLeftStringBlock = script.Substring(lastIndex, index - lastIndex + 1);

            return aLeftStringBlock.Trim();
        }

        #endregion

        // ------------------------------------------
        // DEFINITIONS
        // ------------------------------------------

        #region Definitions

        /// <summary>
        /// Gets the possible definitions corresponding to the specified script.
        /// </summary>
        /// <param name="scriptInterpreter">The script interpreter to consider.</param>
        /// <param name="script">The script to analyse.</param>
        /// <param name="index">The index position of the parent script word.</param>
        /// <param name="isSuggest">False if all the child script words of the parent must be returned.
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// True if only the child script words similar to the child script word present in the script
        /// must be returned.</param>
        /// <returns>Returns the possible child script word definitions.</returns>
        public static Dictionary<string, IBdoScriptwordDefinition> GetWordDefinitions(
            IBdoScriptInterpreter scriptInterpreter,
            string script,
            int index,
            bool isSuggest,
            IScriptVariableSet scriptVariableSet = null)
        {
            if (scriptInterpreter == null) return new Dictionary<string, IBdoScriptwordDefinition>();

            // first we retrieve the script block at the index index
            string stringBlock = BdoScriptParsingHelper.GetScriptBlock(script, index);

            // we retrieve the root script word

            string stringBlockToParse = stringBlock;
            if (stringBlockToParse.Contains("." + BdoScriptParsingHelper.Symbol_Fun))
                stringBlockToParse = stringBlockToParse.Substring(0, stringBlockToParse.IndexOf("." + BdoScriptParsingHelper.Symbol_Fun));
            var log = new BdoLog();
            int aTempIndex = 0;
            IBdoScriptword rootScriptword = scriptInterpreter.FindNextScriptword(
                stringBlockToParse,
                null,
                ref aTempIndex,
                0,
                scriptVariableSet,
                log);

            // if it is not null
            if (rootScriptword != null)
            {
                // we retrieve the last child script word
                IBdoScriptword lastChildScriptword = rootScriptword.Last();
                if (!isSuggest)
                {
                    return lastChildScriptword.Definition == null ?
                            new Dictionary<string, IBdoScriptwordDefinition>() :
                            new Dictionary<string, IBdoScriptwordDefinition>(lastChildScriptword.Definition.Children);
                }
                else
                {
                    string currentScriptwordString = stringBlock.Contains(".")
                        ? stringBlock.Substring(stringBlock.IndexOf(".") + 1, stringBlock.Length - stringBlock.IndexOf(".") - 1)
                            .Replace(BdoScriptParsingHelper.Symbol_Fun, string.Empty)
                        : stringBlock;
                    return scriptInterpreter.GetDefinitionsWithApproximativeName(currentScriptwordString, lastChildScriptword.Definition);
                }
            }
            if (!isSuggest)
                return scriptInterpreter.GetDefinitions();
            else
                return scriptInterpreter.GetDefinitionsWithApproximativeName(stringBlock);
        }

        #endregion
    }
}
