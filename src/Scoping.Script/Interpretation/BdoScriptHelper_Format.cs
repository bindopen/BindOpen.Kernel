using System;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This static class provides utility methods for formatting and cleaning script strings.
/// </summary>
public static partial class BdoScriptHelper
{
    /// <summary>
    /// Formats a script string by adding indentation and carriage returns for better readability.
    /// </summary>
    /// <param name="script">The raw script string to format.</param>
    /// <param name="tabulationNumber">The initial number of tabulations to apply.</param>
    /// <returns>Returns a formatted, indented script string.</returns>
    public static string FormatScript(this string script, int tabulationNumber = 0)
    {
        if (string.IsNullOrEmpty(script)) return string.Empty;

        // Step 1: Clean the script of existing formatting if we are starting at the root
        string alignedScript = (tabulationNumber == 0 ?
            script.CleanScript().Trim() :
            script);

        int index = -1;
        int scriptWordDepth = 0;

        // Step 2: Iterate through the script to identify structural markers (parentheses and commas)
        while (index < alignedScript.Length)
        {
            index = alignedScript.IndexOfScript("(", index + 1);

            if (index != -1 && index < alignedScript.Length)
            {
                int nextIndex = index + 1;
                bool hasSeveralParameters = false;

                // Analyze parameters inside the parentheses
                while ((nextIndex < alignedScript.Length) && (alignedScript.Substring(nextIndex, 1) != ")"))
                {
                    nextIndex = alignedScript.IndexOfScript(",", nextIndex);

                    if (nextIndex != -1 && nextIndex < alignedScript.Length)
                    {
                        // Handle formatting after the opening '('
                        if (alignedScript[index] == '(')
                        {
                            if (alignedScript[nextIndex] == ',')
                            {
                                tabulationNumber++;
                                alignedScript = alignedScript.AddCR(index + 1, ref index, ref nextIndex, tabulationNumber);
                                index += 1 + tabulationNumber;
                            }
                            else
                            {
                                string parameterValue = alignedScript.Substring(index + 1, nextIndex - index - 1);

                                // If the parameter itself contains a nested function or variable
                                if ((parameterValue.Contains(BdoScriptHelper.Symbol_Var) || parameterValue.Contains(BdoScriptHelper.Symbol_Fun)) &&
                                    (parameterValue.Contains('(')))
                                {
                                    tabulationNumber++;
                                    alignedScript = alignedScript.AddCR(index + 1, ref index, ref nextIndex, tabulationNumber);
                                    index += 1 + tabulationNumber;
                                    hasSeveralParameters = true;
                                }
                            }
                        }

                        // Recursively format sub-scripts within parameters
                        string subScript = alignedScript.Substring(index + 1, nextIndex - index - 1);
                        string alignedSubScript = subScript.Trim().FormatScript(tabulationNumber);
                        alignedScript = alignedScript.Remove(index + 1, subScript.Length).Insert(index + 1, alignedSubScript);

                        nextIndex = index + alignedSubScript.Length + 1;

                        // Add carriage return after commas in multi-parameter functions
                        if (nextIndex < alignedScript.Length && alignedScript[nextIndex] == ',')
                        {
                            alignedScript = alignedScript.AddCR(nextIndex + 1, ref nextIndex, ref index, tabulationNumber);
                            nextIndex += tabulationNumber;
                            hasSeveralParameters = true;
                        }
                        // Handle fluent chaining formatting: ).$ or ).:
                        else if (nextIndex <= alignedScript.Length - 3 && alignedScript.Substring(nextIndex, 2) == ").")
                        {
                            nextIndex += 3;
                            tabulationNumber++;
                            alignedScript = alignedScript.AddCR(nextIndex, ref index, ref nextIndex, tabulationNumber);
                            nextIndex -= 1;
                            scriptWordDepth++;
                        }
                        // Close indentation when the function ends
                        else if (nextIndex < alignedScript.Length && alignedScript[nextIndex] == ')' && hasSeveralParameters)
                        {
                            tabulationNumber -= (1 + scriptWordDepth);
                            alignedScript = alignedScript.AddCR(nextIndex, ref index, ref nextIndex, tabulationNumber);
                            scriptWordDepth = 0;
                        }

                        index = nextIndex + 1;
                        nextIndex = index;
                    }
                }
            }
            else break;
        }

        return alignedScript;
    }

    /// <summary>
    /// Removes control characters (CR, LF, Tab) from the script to normalize it before formatting.
    /// </summary>
    private static string CleanScript(this string script)
    {
        return script?
            .Replace("\n", " ")
            .Replace("\r", " ")
            .Replace("\t", " ");
    }

    /// <summary>
    /// Inserts a newline followed by the appropriate number of tabulations at the specified index.
    /// </summary>
    private static string AddCR(this string script, int index, ref int indexToUpdate1, ref int indexToUpdate2, int tabulationNumber)
    {
        string indent = "\n" + new string('\t', Math.Max(0, tabulationNumber));
        script = script.Insert(index, indent);

        if (indexToUpdate1 >= index) indexToUpdate1 += indent.Length;
        if (indexToUpdate2 >= index) indexToUpdate2 += indent.Length;

        return script;
    }
}