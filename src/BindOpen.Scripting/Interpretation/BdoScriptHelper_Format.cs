using System;

namespace BindOpen.Scripting
{

    /// <summary>
    /// This class represents a script format helper.
    /// </summary>
    public static partial class BdoScriptHelper
    {
        // ------------------------------------------
        // FORMAT
        // ------------------------------------------

        #region Fomat

        /// <summary>
        /// Formats the specified script string considering the specified number of tabulations.
        /// </summary>
        /// <param key="script">The script to format.</param>
        /// <param key="tabulationNumber">The number of tabulations.</param>
        /// <returns>Returns the formated script.</returns>
        public static string FormatScript(this string script, int tabulationNumber = 0)
        {
            string alignedScript = (tabulationNumber == 0 ?
                script.CleanScript().FormatScript(0).Trim() :
                script);

            int index = -1;
            int scriptWordDepth = 0;

            while (index < alignedScript.Length)
            {
                index = alignedScript.IndexOfFromScript("(", index + 1);

                // if there is a "("
                if (index < alignedScript.Length)
                {
                    int nextIndex = index + 1;

                    bool aHasSeveralParameters = false;

                    while ((nextIndex < alignedScript.Length) && (alignedScript.Substring(nextIndex, 1) != ")"))
                    {
                        nextIndex = alignedScript.IndexOfFromScript(",", nextIndex);

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
                                    alignedScript = alignedScript.AddCR(index + 1, ref index, ref nextIndex, tabulationNumber);
                                    index += 1 + tabulationNumber;
                                }
                                // else
                                else
                                {
                                    string parameterValue = alignedScript.Substring(index + 1, nextIndex - index - 1);

                                    //  if the parameter contains (% or $) and "(" (normally meaning it contains function or variable) then
                                    if ((parameterValue.Contains(BdoScriptHelper.Symbol_Var) || parameterValue.Contains(BdoScriptHelper.Symbol_Fun)) &
                                        (parameterValue.Contains('(')))
                                    {
                                        // we add a tabulation
                                        tabulationNumber += 1;

                                        // we apply a carriage return with tab after the "("
                                        alignedScript = alignedScript.AddCR(index + 1, ref index, ref nextIndex, tabulationNumber);
                                        index += 1 + tabulationNumber;

                                        // we consider we have several parameters (used when we close the ")")
                                        aHasSeveralParameters = true;
                                    }
                                }
                            }

                            // we get the parameter value and we align it
                            string subScript = alignedScript.Substring(index + 1, nextIndex - index - 1);
                            string alignedSubScript = subScript.Trim().FormatScript(tabulationNumber);
                            alignedScript = alignedScript.Remove(index + 1, subScript.Length);
                            alignedScript = alignedScript.Insert(index + 1, alignedSubScript);

                            nextIndex = index + alignedSubScript.Length + 1;

                            // if we have a comma (meaning we have several parameters)
                            // and if it is the first parameter then
                            if (alignedScript[nextIndex] == ',')
                            {
                                // we apply a carriage return
                                alignedScript = alignedScript.AddCR(nextIndex + 1, ref nextIndex, ref index, tabulationNumber);
                                nextIndex += tabulationNumber;

                                // we remember we have several parameters
                                aHasSeveralParameters = true;
                            }
                            // else if we have a ").$" then
                            else if ((nextIndex <= alignedScript.Length - (")." + BdoScriptHelper.Symbol_Fun).Length) && (alignedScript.Substring(nextIndex,
                                (")." + BdoScriptHelper.Symbol_Fun).Length) == ")." + BdoScriptHelper.Symbol_Fun))
                            {
                                nextIndex += 3;

                                // we add a tabulation
                                tabulationNumber += 1;

                                // we apply a carriage return with tab before the ")"
                                alignedScript = alignedScript.AddCR(nextIndex, ref index, ref nextIndex, tabulationNumber);
                                nextIndex -= 1;
                                scriptWordDepth += 1;
                            }
                            // else if we have a ")" and we have several parameters
                            else if ((alignedScript[nextIndex] == ')') & (aHasSeveralParameters))
                            {
                                // we remove tabulation(s)
                                tabulationNumber -= 1 + scriptWordDepth;

                                // we apply a carriage return with tab before the ")"
                                alignedScript = alignedScript.AddCR(nextIndex, ref index, ref nextIndex, tabulationNumber);
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

        // cleans the script from any CHAR 13 - CHAR 7 - CHAR 10
        private static string CleanScript(this string script)
        {
            // we remove the carriage return (char 10 / char 13) and tabulation (char 9)
            script = script.Replace(Convert.ToChar(10), ' ');
            script = script.Replace(Convert.ToChar(13), ' ');
            script = script.Replace(Convert.ToChar(9), ' ');
            return script;
        }

        // Adds a carriage return with aTabulationNumber tabulations. updates the index.
        private static string AddCR(
            this string script,
            int index,
            ref int indexToUpdate1,
            ref int indexToUpdate2,
            int tabulationNumber)
        {
            // we build the carriage string
            string returnString = "\n";
            for (int i = 0; i < tabulationNumber; i++)
            {
                returnString += "\t";
            }

            // we add it            
            script = script.Insert(index, returnString);
            if (indexToUpdate1 >= index)
            {
                indexToUpdate1 += returnString.Length;
            }
            else if (indexToUpdate2 >= index)
            {
                indexToUpdate2 += returnString.Length;
            }

            return script;
        }

        #endregion
    }
}