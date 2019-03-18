using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Configuration.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.System.Scripting
{
    /// <summary>
    /// This class represents a string parser.
    /// </summary>
    public static class ScriptParsingHelper
    {
        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        /// <summary>
        /// The leading character of the variable.
        /// </summary>
        public static readonly String Symbol_Var = "$(";

        /// <summary>
        /// The leading character of the function.
        /// </summary>
        public static readonly String Symbol_Fun ="$";

        /// <summary>
        /// The syntax items of this class.
        /// </summary>
        public static readonly String[] SyntaxItems = { "-", ">", "(", ")", "," };

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
        public static String GetValueFromText(this String text)
        {
            if (text == "''")
            {
                text = "";
            }
            else
            {
                text = text.Replace("''", "'");
                if (text.Length > 1)
                {
                    if ((text.Substring(0, 1) == "'") & (text.Substring(text.Length - 1, 1) == "'"))
                        text = text.Substring(1, text.Length - 2);
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
        public static String GetParameterValue(this String stringValue, String parameterName, Boolean isMatchCase =false)
        {
            int aStartIndex = -1;
            int aEndIndex = -1;
            if (isMatchCase)
            {
                aStartIndex = stringValue.IndexOf(parameterName + "='") + (parameterName + "='").Length;
                aEndIndex = ScriptParsingHelper.GetIndexOfNextString(stringValue, "'", aStartIndex + 1);
            }
            else
            {
                aStartIndex = stringValue.ToUpper().IndexOf(parameterName.ToUpper() + "='") + (parameterName + "='").Length;
                aEndIndex = ScriptParsingHelper.GetIndexOfNextString(stringValue, "'", aStartIndex + 1);
            }

            return stringValue.Substring(aStartIndex, aEndIndex - aStartIndex+1);
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
        public static int GetIndexOfNextString(String st, String stv, int indexDeb)
        {
            int index = indexDeb;
            Boolean b = false;
            while ((index < st.Length) & !b)
            {
                if ((st.Substring(index, 1) == "'") & (stv != "'"))
                    index = ScriptParsingHelper.GetIndexOfNextString(st, "'", index + 1) + 1;
                else if ((st.Substring(index, 1) == "(") & (stv != "("))
                    index = ScriptParsingHelper.GetIndexOfNextString(st, ")", index + 1) + 1;
                else if (st.Substring(index, 1) == stv)
                    b = true;
                else if ((st.Substring(index, 1) == ")") & (stv == ","))
                    b = true;
                else
                    index += 1;
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
        public static int GetIndexOfLastString(String st, String stv, int indexDeb)
        {
            int index = indexDeb;
            Boolean b = false;
            while ((index >= 0) & !b)
            {
                if ((st.Substring(index, 1) == "'") & (stv != "'"))
                    index = GetIndexOfLastString(st, "'", index - 1) - 1;
                else if ((st.Substring(index, 1) == ")") & (stv != ")"))
                    index = GetIndexOfLastString(st, "(", index - 1) - 1;
                else if (st.Substring(index, 1) == stv)
                    b = true;
                else if ((st.Substring(index, 1) == ")") & (stv == ","))
                    b = true;
                else
                    index -= 1;
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
        public static List<ScriptItem> FindScriptItems(String script)
        {
            List<ScriptItem> scriptItems = new List<ScriptItem>();
            ScriptItem scriptItem = null;

            int index = 0;
            int aLastNoneIndex = index;

            while (index < script.Length)
            {
                int aNextIndex = -1;

                String currentChar = script.Substring(index, 1);

                // if the current char is double quote
                if (currentChar == "'")
                {
                    String scriptItemName = "";

                    // if something between the last none index and the current then
                    if (aLastNoneIndex != index)
                    {
                        // we declare this something as a none-kind script item
                        scriptItemName = script.Substring(aLastNoneIndex, index - aLastNoneIndex);
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKind.None))
                            scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                        else
                            scriptItems.Add(new ScriptItem(
                                ScriptItemKind.None,
                                scriptItemName,
                                index));
                    }

                    // then we look for the next double quote
                    aNextIndex = ScriptParsingHelper.GetIndexOfNextString(script, "'", index + 1);
                    scriptItemName = script.Substring(index, aNextIndex - index + 1);
                    scriptItem = ScriptParsingHelper.FindScriptItem(scriptItemName, index);
                    if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                        scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                    else
                        scriptItems.Add(scriptItem);
                    index = aNextIndex + 1;
                    aLastNoneIndex = index;
                }
                // else if it is a "$" (start of variable item)
                else if (currentChar == ScriptParsingHelper.Symbol_Fun)
                {
                    String scriptItemName = "";

                    // then we look for the next "(" (end of variable item)
                    aNextIndex = ScriptParsingHelper.GetIndexOfNextString(script, "(", index);
                    scriptItemName = script.Substring(index, aNextIndex - index + 1);
                    scriptItem = ScriptParsingHelper.FindScriptItem(scriptItemName, index);
                    if (scriptItem.Kind == ScriptItemKind.Variable)
                    {
                        // if something between the last none index and the current then
                        if (aLastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            scriptItemName = script.Substring(aLastNoneIndex, index - aLastNoneIndex);
                            if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKind.None))
                                scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                            else
                                scriptItems.Add(new ScriptItem(
                                ScriptItemKind.None,
                                scriptItemName,
                                index));
                        }

                        // we record the script item
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                            scriptItems[scriptItems.Count - 1].Name += scriptItem.Name;
                        else
                            scriptItems.Add(scriptItem);
                        index = aNextIndex;
                        aLastNoneIndex = index;
                    }
                    // if it is a literal script word then
                    else if (scriptItem.Kind == ScriptItemKind.Literal)
                    {
                        // we insert "()" in the item name and we update the index
                        scriptItem.Name += "()";
                        aNextIndex += 2;

                        // if something between the last none index and the current then
                        if (aLastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            scriptItemName = script.Substring(aLastNoneIndex, index - aLastNoneIndex);
                            if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKind.None))
                                scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                            else
                                scriptItems.Add(
                                    new ScriptItem(
                                        ScriptItemKind.None,
                                        scriptItemName,
                                        index));
                        }

                        // we record the script item
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                            scriptItems[scriptItems.Count - 1].Name += scriptItem.Name;
                        else
                            scriptItems.Add(scriptItem);

                        index = aNextIndex;
                        aLastNoneIndex = index;
                    }
                    else
                        index += 1;
                }
                // else if it is a "#" (start of function item)
                else if (currentChar == "#")
                {
                    String scriptItemName = "";

                    // then we look for the next "(" (end of variable item)
                    aNextIndex = ScriptParsingHelper.GetIndexOfNextString(script, "(", index);
                    scriptItemName = script.Substring(index, aNextIndex - index + 1);
                    scriptItem = ScriptParsingHelper.FindScriptItem(scriptItemName, index);
                    if (scriptItem.Kind == ScriptItemKind.Function)
                    {
                        // if something between the last none index and the current then
                        if (aLastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            scriptItemName = script.Substring(aLastNoneIndex, index - aLastNoneIndex);
                            if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKind.None))
                                scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                            else
                                scriptItems.Add(
                                    new ScriptItem(
                                        ScriptItemKind.None,
                                        scriptItemName,
                                        index));
                        }

                        // we record the script item
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                            scriptItems[scriptItems.Count - 1].Name += scriptItem.Name;
                        else
                            scriptItems.Add(scriptItem);
                        index = aNextIndex;
                        aLastNoneIndex = index;
                    }
                    else
                        index += 1;
                }
                // else
                else
                {
                    // we retrieve the current item
                    scriptItem = ScriptParsingHelper.FindScriptItem(currentChar, index);

                    // if it is a syntax item
                    if (scriptItem.Kind == ScriptItemKind.Syntax)
                    {
                        String scriptItemName = "";

                        // if something between the last none index and the current then
                        if (aLastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            scriptItemName = script.Substring(aLastNoneIndex, index - aLastNoneIndex);
                            if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKind.None))
                                scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                            else
                                scriptItems.Add(
                                    new ScriptItem(
                                        ScriptItemKind.None,
                                        scriptItemName,
                                        index));
                        }

                        scriptItem = ScriptParsingHelper.FindScriptItem(currentChar, index);
                        if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == scriptItem.Kind))
                            scriptItems[scriptItems.Count - 1].Name += scriptItem.Name;
                        else
                            scriptItems.Add(scriptItem);

                        index += scriptItem.Name.Length;
                        aLastNoneIndex = index;
                    }
                    else
                        // in any remaning case, we increment the index
                        index += 1;
                }
            }

            // if something between the last none index and the current then
            if (aLastNoneIndex != index)
            {
                // we declare this something as a none-kind script item
                String scriptItemName = script.Substring(aLastNoneIndex, index - aLastNoneIndex);
                if ((scriptItems.Count > 0) && (scriptItems[scriptItems.Count - 1].Kind == ScriptItemKind.None))
                    scriptItems[scriptItems.Count - 1].Name += scriptItemName;
                else
                    scriptItems.Add(
                        new ScriptItem(
                            ScriptItemKind.None,
                            scriptItemName,
                            index));
            }

            return scriptItems;
        }

        // Returns the script item with the specified name from the specified index.
        private static ScriptItem FindScriptItem(String name, int index)
        {
            name = name.Trim();

            // we define the default script item to return
            ScriptItem scriptItem = new ScriptItem(
                ScriptItemKind.None,
                name,
                index);

            // if it is a text
            if ((name.Length >= 1) && (name[0] == '\''))
            {
                scriptItem.Index = index;
                scriptItem.Kind = ScriptItemKind.Text;
                scriptItem.Name = name;
            }
            // else if it is a variable
            else if ((name.Length >= 2) && ((name.Substring(0,1) == ScriptParsingHelper.Symbol_Fun) & (name[name.Length - 1] == '(')))
            {
                // we check that the name has only letters, numbers and underscore
                Boolean isGood = false;
                foreach (char aChar in name.Substring(1, name.Length - 2))
                {
                    isGood = char.IsLetter(aChar) | char.IsNumber(aChar) | (aChar == '_');
                    if (!isGood)
                        break;
                }
                if (isGood)
                {
                    scriptItem.Index = index;
                    scriptItem.Name = name.Substring(0, name.Length - 1);
                    // we indentify the literal script functions
                    if (scriptItem.Name.ToUpper().Substring(0, "$LITERAL_".Length) == "$LITERAL_")
                        scriptItem.Kind = ScriptItemKind.Literal;
                    else
                        scriptItem.Kind = ScriptItemKind.Variable;
                }
            }
            // else if it is a function
            else if ((name.Length >= 2) && ((name[0] == '#') & (name[name.Length - 1] == '(')))
            {
                // we check that the name has only letters, numbers and underscore
                Boolean isGood = false;
                foreach (char aChar in name.Substring(1, name.Length - 2))
                {
                    isGood = char.IsLetter(aChar) | char.IsNumber(aChar) | (aChar == '_');
                    if (!isGood)
                        break;
                }
                if (isGood)
                {
                    scriptItem.Index = index;
                    scriptItem.Kind = ScriptItemKind.Function;
                    scriptItem.Name = name.Substring(0, name.Length - 1);
                }
            }
            // else if it is a syntax item
            else if ((new List<String>(ScriptParsingHelper.SyntaxItems)).Contains(name))
            {
                scriptItem.Index = index;
                scriptItem.Kind = ScriptItemKind.Syntax;
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
        public static String GetScriptBlock(String script, int index = 0)
        {
            int aPrevSpaceIndex = -1;
            int aPrevVarIndex = -1;
            int aPrevFunIndex = -1;
            int aLastIndex = index;
            Boolean isEndFound = false;

            while (!isEndFound)
            {
                aPrevSpaceIndex = ScriptParsingHelper.GetIndexOfLastString(script, " ", aLastIndex);
                aPrevVarIndex = ScriptParsingHelper.GetIndexOfLastString(script, ScriptParsingHelper.Symbol_Fun, aLastIndex);
                aPrevFunIndex = (aLastIndex == index ? ScriptParsingHelper.GetIndexOfLastString(script, "#", aLastIndex) : -1);
                aLastIndex = Math.Max(Math.Max(Math.Max(aPrevSpaceIndex, aPrevVarIndex), aPrevFunIndex), 0);
                if ((aLastIndex >= ("." + ScriptParsingHelper.Symbol_Fun).Length) && (script.Substring(aLastIndex - 3, (")." + ScriptParsingHelper.Symbol_Fun).Length) == ")." + ScriptParsingHelper.Symbol_Fun))
                {
                    isEndFound = false;
                    aLastIndex -= 1;
                }
                else
                    isEndFound = true;
            }
            String aLeftStringBlock = script.Substring(aLastIndex, index - aLastIndex + 1);

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
        public static List<ScriptWordDefinition> GetWordDefinitions(
            ScriptInterpreter scriptInterpreter,
            String script,
            int index,
            Boolean isSuggest,
            ScriptVariableSet scriptVariableSet = null)
        {
            if (scriptInterpreter == null) return new List<ScriptWordDefinition>();

            // first we retrieve the script block at the index index
            String aStringBlock = ScriptParsingHelper.GetScriptBlock(script, index);

            // we retrieve the root script word

            String aStringBlockToParse = aStringBlock;
            if (aStringBlockToParse.Contains("." + ScriptParsingHelper.Symbol_Fun))
                aStringBlockToParse = aStringBlockToParse.Substring(0, aStringBlockToParse.IndexOf("." + ScriptParsingHelper.Symbol_Fun));
            Log log = new Log();
            int aTempIndex = 0;
            ScriptWord aRootScriptWord = scriptInterpreter.FindNextScriptWord(
                ref aStringBlockToParse,
                null,
                ref aTempIndex,
                0,
                scriptVariableSet,
                false,
                log);

            // if it is not null
            if (aRootScriptWord != null)
            {
                // we retrieve the last child script word
                ScriptWord lastChildScriptWord = aRootScriptWord.Last();
                if (!isSuggest)
                {
                    return (lastChildScriptWord.Definition == null ?
                            new List<ScriptWordDefinition>() :
                            lastChildScriptWord.Definition.Children);
                }
                else
                {
                    String currentScriptWordString = "";
                    if (aStringBlock.Contains("."))
                        currentScriptWordString = aStringBlock.Substring(aStringBlock.IndexOf(".") + 1, aStringBlock.Length - aStringBlock.IndexOf(".") - 1)
                            .Replace(ScriptParsingHelper.Symbol_Fun, "");
                    else
                        currentScriptWordString = aStringBlock;
                    return scriptInterpreter.Index.GetDefinitionsWithApproximativeName(currentScriptWordString, lastChildScriptWord.Definition);
                }
            }
            if (!isSuggest)
                return scriptInterpreter.Index.Definitions;
            else
                return scriptInterpreter.Index.GetDefinitionsWithApproximativeName(aStringBlock);
        }

        #endregion
    }
}
