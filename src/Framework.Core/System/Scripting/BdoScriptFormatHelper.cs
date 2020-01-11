using System;

namespace BindOpen.Framework.System.Scripting
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
        /// <param name="aTabulationNumber">The number of tabulations.</param>
        /// <returns>Returns the formated script.</returns>
        public static string Format(String script, int aTabulationNumber = 0)
        {
            String aAlignedScript = (aTabulationNumber == 0 ?
                BdoScriptFormatHelper.Format(BdoScriptFormatHelper.CleanScript(script), 0).Trim() :
                script);

            int index = -1;
            int aNextIndex = -1;
            int scriptWordDepth = 0;

            while (index < aAlignedScript.Length)
            {
                index = BdoScriptParsingHelper.GetIndexOfNextString(aAlignedScript, "(", index + 1);

                // if there is a "("
                if (index < aAlignedScript.Length)
                {
                    aNextIndex = index + 1;

                    bool aHasSeveralParameters = false;

                    while ((aNextIndex < aAlignedScript.Length) &&
                        (aAlignedScript.Substring(aNextIndex, 1) != ")"))
                    {
                        aNextIndex = BdoScriptParsingHelper.GetIndexOfNextString(aAlignedScript, ",", aNextIndex);

                        // if the next index is not out of range
                        if (aNextIndex < aAlignedScript.Length)
                        {
                            // if it is the first parameter then
                            if (aAlignedScript[index] == '(')
                            {
                                // if we have a comma (meaning we have several parameters)
                                if (aAlignedScript[aNextIndex] == ',')
                                {
                                    // we add a tabulation
                                    aTabulationNumber += 1;

                                    // we apply a carriage return with tab after the "("
                                    BdoScriptFormatHelper.AddCRToScript(ref aAlignedScript, index + 1, ref index, ref aNextIndex, aTabulationNumber);
                                    index += 1 + aTabulationNumber;
                                }
                                // else
                                else
                                {
                                    String parameterValue = aAlignedScript.Substring(index + 1, aNextIndex - index - 1);

                                    //  if the parameter contains (% or $) and "(" (normally meaning it contains function or variable) then
                                    if ((parameterValue.Contains(BdoScriptParsingHelper.Symbol_Var) | parameterValue.Contains(BdoScriptParsingHelper.Symbol_Fun)) &
                                        (parameterValue.Contains("(")))
                                    {
                                        // we add a tabulation
                                        aTabulationNumber += 1;

                                        // we apply a carriage return with tab after the "("
                                        BdoScriptFormatHelper.AddCRToScript(ref aAlignedScript, index + 1, ref index, ref aNextIndex, aTabulationNumber);
                                        index += 1 + aTabulationNumber;

                                        // we consider we have several parameters (used when we close the ")")
                                        aHasSeveralParameters = true;
                                    }
                                }
                            }

                            // we get the parameter value and we align it
                            String aSubScript = aAlignedScript.Substring(index + 1, aNextIndex - index - 1);
                            String aAlignedSubScript = BdoScriptFormatHelper.Format(aSubScript.Trim(), aTabulationNumber);
                            aAlignedScript = aAlignedScript.Remove(index + 1, aSubScript.Length);
                            aAlignedScript = aAlignedScript.Insert(index + 1, aAlignedSubScript);

                            aNextIndex = index + aAlignedSubScript.Length + 1;

                            // if we have a comma (meaning we have several parameters)
                            // and if it is the first parameter then
                            if (aAlignedScript[aNextIndex] == ',')
                            {
                                // we apply a carriage return
                                BdoScriptFormatHelper.AddCRToScript(ref aAlignedScript, aNextIndex + 1, ref aNextIndex, ref index, aTabulationNumber);
                                aNextIndex += aTabulationNumber;

                                // we remember we have several parameters
                                aHasSeveralParameters = true;
                            }
                            // else if we have a ").$" then
                            else if ((aNextIndex <= aAlignedScript.Length - (")." + BdoScriptParsingHelper.Symbol_Fun).Length) && (aAlignedScript.Substring(aNextIndex,
                                (")." + BdoScriptParsingHelper.Symbol_Fun).Length) == ")." + BdoScriptParsingHelper.Symbol_Fun))
                            {
                                aNextIndex += 3;

                                // we add a tabulation
                                aTabulationNumber += 1;

                                // we apply a carriage return with tab before the ")"
                                BdoScriptFormatHelper.AddCRToScript(ref aAlignedScript, aNextIndex, ref index, ref aNextIndex, aTabulationNumber);
                                aNextIndex -= 1;
                                scriptWordDepth += 1;
                            }
                            // else if we have a ")" and we have several parameters
                            else if ((aAlignedScript[aNextIndex] == ')') & (aHasSeveralParameters))
                            {
                                // we remove tabulation(s)
                                aTabulationNumber -= 1 + scriptWordDepth;

                                // we apply a carriage return with tab before the ")"
                                BdoScriptFormatHelper.AddCRToScript(ref aAlignedScript, aNextIndex, ref index, ref aNextIndex, aTabulationNumber);
                                scriptWordDepth = 0;
                            }

                            // we update indexes
                            index = aNextIndex + 1;
                            aNextIndex = index;
                        }
                    }
                }
            }

            return aAlignedScript;
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
            String aReturnString = "\n";
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