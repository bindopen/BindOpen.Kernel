using System;

namespace BindOpen.System.Scripting
{

    /// <summary>
    /// This class represents a script format helper.
    /// </summary>
    public static class BdoScriptFormatHelper
    {

        // ------------------------------------------
        // FORMAT
        // ------------------------------------------

        #region Fomat

        /// <summary>
        /// Formats the specified script string considering the specified number of tabulations.
        /// </summary>
        /// <param name="script">The script to format.</param>
        /// <param name="tabulationNumber">The number of tabulations.</param>
        /// <returns>Returns the formated script.</returns>
        public static string Format(String script, int tabulationNumber = 0)
        {
            String alignedScript = (tabulationNumber == 0 ?
                BdoScriptFormatHelper.Format(BdoScriptFormatHelper.CleanScript(script), 0).Trim() :
                script);

            int index = -1;
            int scriptWordDepth = 0;

            while (index < alignedScript.Length)
            {
                index = BdoScriptParsingHelper.GetIndexOfNextString(alignedScript, "(", index + 1);

                // if there is a "("
                if (index < alignedScript.Length)
                {
                    int nextIndex = index + 1;

                    bool aHasSeveralParameters = false;

                    while ((nextIndex < alignedScript.Length) && (alignedScript.Substring(nextIndex, 1) != ")"))
                    {
                        nextIndex = BdoScriptParsingHelper.GetIndexOfNextString(alignedScript, ",", nextIndex);

                        // if the next index is not out of range
                        if (nextIndex < alignedScript.Length)
                        {
                            // if it is the first parameter then
                            if (alignedScript[index] == '(')
                            {
                                // if we have a comma (meaning we have several parameters)
                                if (alignedScript[nextIndex] == ',')
                                {
                                    // we add a tabulation
                                    tabulationNumber += 1;

                                    // we apply a carriage return with tab after the "("
                                    BdoScriptFormatHelper.AddCRToScript(ref alignedScript, index + 1, ref index, ref nextIndex, tabulationNumber);
                                    index += 1 + tabulationNumber;
                                }
                                // else
                                else
                                {
                                    String parameterValue = alignedScript.Substring(index + 1, nextIndex - index - 1);

                                    //  if the parameter contains (% or $) and "(" (normally meaning it contains function or variable) then
                                    if ((parameterValue.Contains(BdoScriptParsingHelper.Symbol_Var) | parameterValue.Contains(BdoScriptParsingHelper.Symbol_Fun)) &
                                        (parameterValue.Contains("(")))
                                    {
                                        // we add a tabulation
                                        tabulationNumber += 1;

                                        // we apply a carriage return with tab after the "("
                                        BdoScriptFormatHelper.AddCRToScript(ref alignedScript, index + 1, ref index, ref nextIndex, tabulationNumber);
                                        index += 1 + tabulationNumber;

                                        // we consider we have several parameters (used when we close the ")")
                                        aHasSeveralParameters = true;
                                    }
                                }
                            }

                            // we get the parameter value and we align it
                            string subScript = alignedScript.Substring(index + 1, nextIndex - index - 1);
                            string alignedSubScript = BdoScriptFormatHelper.Format(subScript.Trim(), tabulationNumber);
                            alignedScript = alignedScript.Remove(index + 1, subScript.Length);
                            alignedScript = alignedScript.Insert(index + 1, alignedSubScript);

                            nextIndex = index + alignedSubScript.Length + 1;

                            // if we have a comma (meaning we have several parameters)
                            // and if it is the first parameter then
                            if (alignedScript[nextIndex] == ',')
                            {
                                // we apply a carriage return
                                BdoScriptFormatHelper.AddCRToScript(ref alignedScript, nextIndex + 1, ref nextIndex, ref index, tabulationNumber);
                                nextIndex += tabulationNumber;

                                // we remember we have several parameters
                                aHasSeveralParameters = true;
                            }
                            // else if we have a ").$" then
                            else if ((nextIndex <= alignedScript.Length - (")." + BdoScriptParsingHelper.Symbol_Fun).Length) && (alignedScript.Substring(nextIndex,
                                (")." + BdoScriptParsingHelper.Symbol_Fun).Length) == ")." + BdoScriptParsingHelper.Symbol_Fun))
                            {
                                nextIndex += 3;

                                // we add a tabulation
                                tabulationNumber += 1;

                                // we apply a carriage return with tab before the ")"
                                BdoScriptFormatHelper.AddCRToScript(ref alignedScript, nextIndex, ref index, ref nextIndex, tabulationNumber);
                                nextIndex -= 1;
                                scriptWordDepth += 1;
                            }
                            // else if we have a ")" and we have several parameters
                            else if ((alignedScript[nextIndex] == ')') & (aHasSeveralParameters))
                            {
                                // we remove tabulation(s)
                                tabulationNumber -= 1 + scriptWordDepth;

                                // we apply a carriage return with tab before the ")"
                                BdoScriptFormatHelper.AddCRToScript(ref alignedScript, nextIndex, ref index, ref nextIndex, tabulationNumber);
                                scriptWordDepth = 0;
                            }

                            // we update indexes
                            index = nextIndex + 1;
                            nextIndex = index;
                        }
                    }
                }
            }

            return alignedScript;
        }

        /// <summary>
        /// Returns the specified script literalized.
        /// </summary>
        /// <param name="script">The script to literalize.</param>
        /// <returns>The specified script literalized.</returns>
        public static string LiteralizeScript(String script)
        {
            // we convert the \n and \t into script functions
            script = script.Replace("\n", "$LITERAL_CR()");
            script = script.Replace("\t", "$LITERAL_TAB()");
            return script;
        }

        /// <summary>
        /// Returns the specified script deliteralized.
        /// </summary>
        /// <param name="script">The script to deliteralize.</param>
        /// <returns>The specified script deliteralized.</returns>
        public static string DeliteralizeScript(String script)
        {
            // we convert the literal script functions to \n and \t
            script = script.Replace("$LITERAL_CR()", "\n");
            script = script.Replace("$LITERAL_TAB()", "\t");
            return script;
        }

        // cleans the script from any CHAR 13 - CHAR 7 - CHAR 10
        private static String CleanScript(String script)
        {
            // we remove the carriage return (char 10 / char 13) and tabulation (char 9)
            script = script.Replace(Convert.ToChar(10), ' ');
            script = script.Replace(Convert.ToChar(13), ' ');
            script = script.Replace(Convert.ToChar(9), ' ');
            return script;
        }

        // Adds a carriage return with aTabulationNumber tabulations. updates the index.
        private static void AddCRToScript(
            ref String script,
            int index,
            ref int indexToUpdate1,
            ref int indexToUpdate2,
            int aTabulationNumber)
        {
            // we build the carriage string
            string aReturnString = "\n";
            for (int i = 0; i < aTabulationNumber; i++)
            {
                aReturnString += "\t";
            }

            // we add it            
            script = script.Insert(index, aReturnString);
            if (indexToUpdate1 >= index)
                indexToUpdate1 += aReturnString.Length;
            if (indexToUpdate2 >= index)
                indexToUpdate2 += aReturnString.Length;
        }

        #endregion
    }
}