using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Script
{
    /// <summary>
    /// This class represents a string parser.
    /// </summary>
    public static partial class BdoScriptHelper
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
        /// <param key="text">The text from which the string value is retrieved.</param>
        /// <returns>The string value from a text</returns>
        public static string GetValueFromScript(this string text)
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

        #endregion

        // ------------------------------------------
        // INDEXES
        // ------------------------------------------

        #region Indexes

        /// <summary>
        /// Returns the position of stv in st from index taking in account the closing ponctuation (,{,[. 
        /// </summary>
        /// <param key="st">The string to parse.</param>
        /// <param key="stv">The string to look for.</param>
        /// <param key="indexDeb">The start index.</param>
        /// <returns>The position of the next stv into st from index position.</returns>
        public static int IndexOfScript(this string st, string stv, int indexDeb)
        {
            int index = indexDeb;
            bool b = false;
            while ((index < st.Length) && !b)
            {
                if ((st.Substring(index, 1) == "'") && (stv != "'"))
                {
                    index = st.IndexOfScript("'", index + 1) + 1;
                }
                else if ((st.Substring(index, 1) == "(") && (stv != "("))
                {
                    index = st.IndexOfScript(")", index + 1) + 1;
                }
                else if (st.Substring(index, 1) == stv)
                {
                    b = true;
                }
                else if ((st.Substring(index, 1) == ")") && (stv == ","))
                {
                    b = true;
                }
                else
                {
                    index++;
                }
            }
            return index;
        }

        /// <summary>
        /// Returns the last position of stv in st from index taking in account the closing ponctuation (,{,[. 
        /// </summary>
        /// <param key="st">The string to parse.</param>
        /// <param key="stv">The string to look for.</param>
        /// <param key="indexDeb">The start index.</param>
        /// <returns>The position of the next stv into st from index position.</returns>
        public static int LastIndexOfScript(this string st, string stv, int indexDeb)
        {
            int index = indexDeb;
            bool b = false;
            while ((index >= 0) && !b)
            {
                if ((st.Substring(index, 1) == "'") && (stv != "'"))
                {
                    index = LastIndexOfScript(st, "'", index - 1) - 1;
                }
                else if ((st.Substring(index, 1) == ")") && (stv != ")"))
                {
                    index = LastIndexOfScript(st, "(", index - 1) - 1;
                }
                else if (st.Substring(index, 1) == stv)
                {
                    b = true;
                }
                else if ((st.Substring(index, 1) == ")") && (stv == ","))
                {
                    b = true;
                }
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
        /// <param key="script">The script to analyse.</param>
        /// <returns>The items that are in the specified script.</returns>
        public static List<ScriptItem> FindScriptItems(this string script)
        {
            List<ScriptItem> scriptItems = new();
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
                        if ((scriptItems.Count > 0) && (scriptItems[^1].Kind == ScriptItemKinds.None))
                        {
                            scriptItems[^1].WithName(scriptItems[^1].Name + scriptItemName);
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
                    nextIndex = script.IndexOfScript("'", index + 1);
                    scriptItemName = script.Substring(index, nextIndex - index + 1);
                    scriptItem = scriptItemName.FindScriptItem(index);
                    if ((scriptItems.Count > 0) && (scriptItems[^1].Kind == scriptItem.Kind))
                    {
                        scriptItems[^1].WithName(scriptItems[^1].Name + scriptItemName);
                    }
                    else
                    {
                        scriptItems.Add(scriptItem);
                    }

                    index = nextIndex + 1;
                    lastNoneIndex = index;
                }
                // else if it is a "$" (start of variable item)
                else if (currentChar == Symbol_Fun)
                {
                    // then we look for the next "(" (end of variable item)
                    nextIndex = script.IndexOfScript("(", index);
                    string scriptItemName = script.Substring(index, nextIndex - index + 1);
                    scriptItem = scriptItemName.FindScriptItem(index);
                    if (scriptItem.Kind == ScriptItemKinds.Variable)
                    {
                        // if something between the last none index and the current then
                        if (lastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            scriptItemName = script[lastNoneIndex..index];
                            if ((scriptItems.Count > 0) && (scriptItems[^1].Kind == ScriptItemKinds.None))
                            {
                                scriptItems[^1].WithName(scriptItems[^1].Name + scriptItemName);
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
                        if ((scriptItems.Count > 0) && (scriptItems[^1].Kind == scriptItem.Kind))
                            scriptItems[^1].WithName(scriptItems[^1].Name + scriptItemName);
                        else
                            scriptItems.Add(scriptItem);
                        index = nextIndex;
                        lastNoneIndex = index;
                    }
                    // if it is a literal script word then
                    else if (scriptItem.Kind == ScriptItemKinds.Literal)
                    {
                        // we insert "()" in the item name and we update the index
                        scriptItem.WithName("()");
                        nextIndex += 2;

                        // if something between the last none index and the current then
                        if (lastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            scriptItemName = script[lastNoneIndex..index];
                            if ((scriptItems.Count > 0) && (scriptItems[^1].Kind == ScriptItemKinds.None))
                            {
                                scriptItems[^1].WithName(scriptItems[^1].Name + scriptItemName);
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
                        if ((scriptItems.Count > 0) && (scriptItems[^1].Kind == scriptItem.Kind))
                            scriptItems[^1].WithName(scriptItems[^1].Name + scriptItemName);
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
                // else
                else
                {
                    // we retrieve the current item
                    scriptItem = currentChar.FindScriptItem(index);

                    // if it is a syntax item
                    if (scriptItem.Kind == ScriptItemKinds.Syntax)
                    {

                        // if something between the last none index and the current then
                        if (lastNoneIndex != index)
                        {
                            // we declare this something as a none-kind script item
                            string scriptItemName = script[lastNoneIndex..index];
                            if ((scriptItems.Count > 0) && (scriptItems[^1].Kind == ScriptItemKinds.None))
                            {
                                scriptItems[^1].WithName(scriptItems[^1].Name + scriptItemName);
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

                        scriptItem = currentChar.FindScriptItem(index);
                        if ((scriptItems.Count > 0) && (scriptItems[^1].Kind == scriptItem.Kind))
                            scriptItems[^1].WithName(scriptItems[^1].Name + scriptItem.Name);
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
                if ((scriptItems.Count > 0) && (scriptItems[^1].Kind == ScriptItemKinds.None))
                {
                    scriptItems[^1].WithName(scriptItems[^1].Name + scriptItemName);
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
        private static ScriptItem FindScriptItem(this string name, int index)
        {
            name = name.Trim();

            // we define the default script item to return
            ScriptItem scriptItem = new(
                ScriptItemKinds.None,
                name,
                index);

            // if it is a text
            if ((name.Length >= 1) && (name[0] == '\''))
            {
                scriptItem.Index = index;
                scriptItem.Kind = ScriptItemKinds.Text;
                scriptItem.WithName(name);
            }
            // else if it is a variable
            else if ((name.Length >= 2) && ((name[..1] == Symbol_Fun) & (name[^1] == '(')))
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
                    scriptItem.WithName(name[0..^1]);
                    // we indentify the literal script functions
                    if (scriptItem.Name.ToUpper()[.."$LITERAL_".Length] == "$LITERAL_")
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
                    scriptItem.WithName(name[0..^1]);
                }
            }
            // else if it is a syntax item
            else if (SyntaxItems.Contains(name))
            {
                scriptItem.Index = index;
                scriptItem.Kind = ScriptItemKinds.Syntax;
                scriptItem.WithName(name);
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
        /// <param key="script">The script to analyse.</param>
        /// <param key="index">The index position to begin analyse.</param>
        /// <returns></returns>
        public static string GetScriptBlock(this string script, int index = 0)
        {
            int lastIndex = index;
            bool isEndFound = false;

            while (!isEndFound)
            {
                int prevSpaceIndex = script.LastIndexOfScript(" ", lastIndex);
                int prevVarIndex = script.LastIndexOfScript(Symbol_Fun, lastIndex);
                int prevFunIndex = (lastIndex == index ? script.LastIndexOfScript("#", lastIndex) : -1);
                lastIndex = Math.Max(Math.Max(Math.Max(prevSpaceIndex, prevVarIndex), prevFunIndex), 0);
                if ((lastIndex >= ("." + Symbol_Fun).Length)
                    && (script.Substring(lastIndex - 3, (")." + Symbol_Fun).Length) == ")." + Symbol_Fun))
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

        //// ------------------------------------------
        //// DEFINITIONS
        //// ------------------------------------------

        //#region Definitions

        ///// <summary>
        ///// Gets the possible definitions corresponding to the specified script.
        ///// </summary>
        ///// <param key="scriptInterpreter">The script interpreter to consider.</param>
        ///// <param key="script">The script to analyse.</param>
        ///// <param key="index">The index position of the parent script word.</param>
        ///// <param key="isSuggest">False if all the child script words of the parent must be returned.
        ///// <param key="varSet">The variable element set to use.</param>
        ///// True if only the child script words similar to the child script word present in the script
        ///// must be returned.</param>
        ///// <returns>Returns the possible child script word definitions.</returns>
        //public static ITBdoSet<IBdoFunctionDefinition> GetWordDefinitions(
        //    this IBdoScriptInterpreter scriptInterpreter,
        //    string script,
        //    int index,
        //    bool isSuggest,
        //    IBdoMetaSet varSet = null,
        //    IBdoLog log = null)
        //{
        //    if (scriptInterpreter == null) return new TBdoSet<IBdoFunctionDefinition>();

        //    // first we retrieve the script block at the index index

        //    string stringBlock = script.GetScriptBlock(index);

        //    // we retrieve the root script word

        //    string stringBlockToParse = stringBlock;
        //    if (stringBlockToParse.Contains("." + Symbol_Fun))
        //    {
        //        stringBlockToParse = stringBlockToParse[..stringBlockToParse.IndexOf("." + Symbol_Fun)];
        //    }
        //    int tempIndex = 0;

        //    IBdoScriptword rootScriptword = scriptInterpreter.FindNextWord(
        //        stringBlockToParse,
        //        null,
        //        ref tempIndex,
        //        0,
        //        varSet,
        //        log);

        //    // if it is not null
        //    if (rootScriptword != null)
        //    {
        //        // we retrieve the last child script word

        //        if (!isSuggest)
        //        {
        //            return default;
        //        }
        //        else
        //        {
        //            string currentScriptwordString = stringBlock.Contains('.')
        //                ? stringBlock.Substring(stringBlock.IndexOf(".") + 1, stringBlock.Length - stringBlock.IndexOf(".") - 1)
        //                    .Replace(Symbol_Fun, string.Empty)
        //                : stringBlock;

        //            return scriptInterpreter.GetDefinitionsWithName(currentScriptwordString, false);
        //        }
        //    }

        //    return !isSuggest ? scriptInterpreter.GetDefinitions() : scriptInterpreter.GetDefinitionsWithName(stringBlock, false);
        //}

        //#endregion
    }
}
