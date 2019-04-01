using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Configuration.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.Extensions.Indexes;
using BindOpen.Framework.Core.Extensions.Indexes.Scriptwords;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.System.Scripting
{
    /// <summary>
    /// This class represents a script interpreter. A script interpreter allows to interpret a script
    /// using script dictionnaries and data context, providing a log for the interpretation task.
    /// </summary>
    public class ScriptInterpreter : DataItem, IScriptInterpreter
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private IAppScope _appScope = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public IScriptWordIndex Index { get; set; } = new ScriptWordIndex();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptInterpreter class.
        /// </summary>
        public ScriptInterpreter()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptInterpreter class.
        /// </summary>
        /// <param name="appScope">The application scope used to interprete.</param>
        public ScriptInterpreter(
            IAppScope appScope)
        {
            _appScope = appScope;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Loads the specified libraries.
        /// </summary>
        /// <param name="libraryNames">The names of libraries to load.</param>
        public void LoadDefinitions(string[] libraryNames = null)
        {
            if ((Index != null) && (_appScope.AppExtension != null))
            {
                Index.Definitions = _appScope.AppExtension.GetItemDefinitions<IScriptWordDefinition>(libraryNames);
            }
        }

        #endregion

        // ------------------------------------------
        // EVALUATION
        // ------------------------------------------

        #region Evaluation

        // Evaluation ------------------------------------

        /// <summary>
        /// Evaluates the specified data expression.
        /// </summary>
        /// <param name="dataExpression">The data expression to consider.</param>
        /// <param name="resultScript">The result script to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Literal or script value according to the specified default mode.</returns>
        public object Evaluate(
            IDataExpression dataExpression,
            out string resultScript,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            resultScript = "";
            if (dataExpression != null)
                return EvaluateScript(dataExpression.Text, dataExpression.Kind, out resultScript, scriptVariableSet, log);
            return null;
        }

        /// <summary>
        /// Evaluates the specified data expression.
        /// </summary>
        /// <param name="dataExpression">The data expression to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Literal or script value according to the specified default mode.</returns>
        public object Evaluate(
            IDataExpression dataExpression,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            string resultScript = "";
            return Evaluate(dataExpression, out resultScript, scriptVariableSet, log);
        }

        /// <summary>
        /// Evaluates the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="resultScript">The result script to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public object Evaluate(
            string script,
            out string resultScript,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            resultScript = "";
            return EvaluateScript(script, DataExpressionKind.Script, out resultScript, scriptVariableSet, log);
        }

        /// <summary>
        /// Evaluates the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public object Evaluate(
            string script,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            return EvaluateScript(script, DataExpressionKind.Script, out string resultScript, scriptVariableSet, log);
        }

        // Interpretation ------------------------------------

        /// <summary>
        /// Interprets the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="dataExpression">The data expression to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public string Interprete(
            IDataExpression dataExpression,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            Evaluate(dataExpression, out string resultScript, scriptVariableSet, log);
            return resultScript;
        }

        /// <summary>
        /// Interprets the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public string Interprete(
            string script,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            string resultScript = "";
            EvaluateScript(script, DataExpressionKind.Script, out resultScript, scriptVariableSet, log);
            return resultScript;
        }

        /// <summary>
        /// Interprets the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="resultScript">The result script to consider.</param>
        /// <param name="dataExpressionKind">The expression kind to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        private object EvaluateScript(
            string script,
            DataExpressionKind dataExpressionKind,
            out string resultScript,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            Object item = null;

            resultScript = script ?? "";

            int index;
            int scriptWordBeginIndex;

            switch (dataExpressionKind)
            {
                case DataExpressionKind.Auto:
                    if (script != null)
                    {
                        scriptWordBeginIndex = script.IndexOf("{{");
                        while (scriptWordBeginIndex > -1)
                        {
                            index = script.GetIndexOfNextString("}}", scriptWordBeginIndex + 1);

                            if ((scriptWordBeginIndex > -1) && (index > -1))
                            {
                                string stringValue;
                                EvaluateScript(script, DataExpressionKind.Script, out stringValue, scriptVariableSet, log);

                                resultScript = resultScript.Replace(
                                    resultScript.GetSubstring(scriptWordBeginIndex, index + 1), stringValue);
                                scriptWordBeginIndex = script.IndexOf("{{", index + 1);
                            }
                        }
                    }
                    break;
                case DataExpressionKind.Script:
                    ILog subLog = new Log();

                    // we parse the text to interpretate
                    index = 0;
                    scriptWordBeginIndex = index;

                    // we get the next function or variable
                    item = Interprete(
                        script, out resultScript, ref index, 0, scriptVariableSet, false, subLog);

                    if (log != null)
                    {
                        if (log.Append(subLog).HasErrorsOrExceptions())
                        {
                            log.AddError(
                                title: "Error occured while interpreting script",
                                description: "Error while interpreting the script '" + script + "'. " +
                                    "The result is '" + resultScript + "'.",
                                resultCode: "SCRIPTINTERPRETATIONERROR"
                                );
                        }
                    }

                    break;
            }

            return item;
        }

        #endregion

        // ------------------------------------------
        // INTERPRETATION
        // ------------------------------------------

        #region Interpretation

        // Returns the standard-interpretated text
        private object Interprete(
            string script,
            out string resultScript,
            ref int index,
            int offsetIndex,
            IScriptVariableSet scriptVariableSet = null,
            bool isSimulationModeOn = false,
            ILog log = null)
        {
            Object evaluatedValue = resultScript = script;
            if (resultScript == null)
                resultScript = "";

            if (script != null)
            {
                //resultScript = resultScript.Replace("\n", " ").Replace("\t", " ");

                // we parse the text to interpretate
                index = 0;
                while (index < resultScript.Length)
                {
                    int scriptWordBeginIndex = index;

                    // we get the next function or variable
                    IScriptWord scriptWord = FindNextScriptWord(
                        ref resultScript,
                        null,
                        ref index,
                        offsetIndex,
                        scriptVariableSet,
                        isSimulationModeOn,
                        log);

                    // if the script word has been found
                    if (scriptWord != null)
                    {
                        // we replace the script word by its value
                        string evaluatedString = "";
                        evaluatedValue = scriptWord.Item = EvaluateWord(
                            scriptWord, index + offsetIndex, scriptVariableSet, isSimulationModeOn, log);
                        if (evaluatedValue != null)
                            evaluatedString = evaluatedValue.ToString();
                        //we retrieve the index of the root script word
                        IScriptWord rootScSriptWord = scriptWord.Root();
                        if (rootScSriptWord != null)
                        {
                            scriptWordBeginIndex = resultScript.IndexOf(
                               rootScSriptWord.Kind == ScriptItemKind.Function ?
                               ScriptParsingHelper.Symbol_Fun + rootScSriptWord.Name :
                               ScriptParsingHelper.Symbol_Var + rootScSriptWord.Name);
                        }

                        if (scriptWordBeginIndex > -1)
                        {
                            resultScript = resultScript.Replace(
                               resultScript.Substring(scriptWordBeginIndex, index - scriptWordBeginIndex + 1), evaluatedString);
                        }

                        index = scriptWordBeginIndex + evaluatedString.Length - 1;
                    }
                    else
                    {
                        evaluatedValue = resultScript;
                    }

                    // we increment the index
                    index++;
                }
            }

            return evaluatedValue;
        }

        /// <summary>
        /// Finds the next script words at the specified index.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="parentScriptWord">The parent script word to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <param name="offsetIndex">The offset index to consider.</param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="isSimulationModeOn">Indicates whether the simulation mode is on.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public IScriptWord FindNextScriptWord(
            ref string script,
            IScriptWord parentScriptWord,
            ref int index,
            int offsetIndex,
            IScriptVariableSet scriptVariableSet = null,
            bool isSimulationModeOn = false,
            ILog log = null)
        {
            if (parentScriptWord != null && parentScriptWord.Definition == null)
            {
                if (log != null)
                {
                    ILogEvent logEvent = log.AddError(
                    title: "Syntax error: Function named '" + parentScriptWord.Name + "' not defined. Position " + (index + offsetIndex),
                    resultCode: "SCRIPT_NOTEXISTINGWORD");
                    logEvent.Detail = new DataElementSet();
                    logEvent.Detail.AddElement("Position", (index + offsetIndex).ToString());
                }
                return null;
            }

            IScriptWord scriptWord = null;

            // we retrieve the type of the next script word
            ScriptItemKind scriptItemKind = ScriptItemKind.None;
            int nextIndex = -1;
            int nextFuncIndex = script.IndexOf(ScriptParsingHelper.Symbol_Fun, index);
            int nextVarIndex = script.IndexOf(ScriptParsingHelper.Symbol_Var, index);

            // if the script word begins and ends with a quote then it is a variable
            if (script.Trim().StartsWith("'") && script.Trim().EndsWith("'"))
            {
                index = script.Length;
                scriptItemKind = ScriptItemKind.None;
            }
            // else if the next word is a variable
            else if (parentScriptWord != null && script.GetSubstring(index, index) == ".")
            {
                index++;
                scriptItemKind = ScriptItemKind.Function;
            }
            else if ((nextVarIndex > -1) && ((nextFuncIndex == -1) || (nextVarIndex <= nextFuncIndex)))
            {
                index = nextVarIndex + 2;
                scriptItemKind = ScriptItemKind.Variable;
            }
            // else if the next word is a function
            else if ((nextFuncIndex > -1) && ((nextVarIndex == -1) || (nextFuncIndex < nextVarIndex)))
            {
                index = nextFuncIndex + 1;
                scriptItemKind = ScriptItemKind.Function;
            }
            else
            {
                index = script.Length;
            }

            // if a next script word has been found then
            if (scriptItemKind != ScriptItemKind.None)
            {
                if (scriptItemKind == ScriptItemKind.Function)
                {
                    // we look for the next "(" character.
                    nextIndex = ScriptParsingHelper.GetIndexOfNextString(script, "(", index);
                    if (nextIndex >= script.Length)
                    {
                        if (log != null)
                        {
                            ILogEvent logEvent = log.AddError(
                                title: "Syntax Error: Required character '(' for functions missing. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_SYNTAXERROR");
                            logEvent.Detail = new DataElementSet();
                            logEvent.Detail.AddElement("Position", (index + offsetIndex).ToString());
                        }
                        return null;
                    }

                    // we instantiate the script word
                    scriptWord = new ScriptWord
                    {
                        Name = script.GetSubstring(index, nextIndex - 1).Trim()
                    };
                    index = nextIndex;
                    // we retrieve the function parameters
                    int scriptWordParameterCount = 0;
                    while ((script.Substring(nextIndex, 1) != ")") && (nextIndex < script.Length))
                    {
                        nextIndex = ScriptParsingHelper.GetIndexOfNextString(script, ",", nextIndex + 1);
                        // if the next index is out of range
                        if (nextIndex >= script.Length)
                        {
                            if (log != null)
                            {
                                IEvent logEvent = log.AddError(
                                    title: "Syntax Error: Character ')' not found for function. Position " + (index + offsetIndex),
                                    resultCode: "SCRIPT_SYNTAXERROR");
                                logEvent.Detail = new DataElementSet();
                                logEvent.Detail.AddElement("Position", (index + offsetIndex).ToString());
                            }
                            return null;
                        }
                        else
                        {
                            // else we get the parameter value and we interprete it
                            string scriptWordParameterValue = script.Substring(index + 1, nextIndex - index - 1).Trim();
                            if (scriptWordParameterValue != "")
                            {
                                Object parameterValue = null;
                                string parameterText = "";

                                int aSubIndex = 0;
                                parameterValue = Interprete(
                                    scriptWordParameterValue,
                                    out parameterText,
                                    ref aSubIndex,
                                    offsetIndex + index + 1,
                                    scriptVariableSet,
                                    isSimulationModeOn,
                                    log);
                                parameterText = parameterText.Trim();
                                //script = script.Replace(script.Substring(index + 1, nextIndex - index - 1), aEvaluatedScriptWordParameterValue);

                                script = script.Remove(index + 1, nextIndex - index - 1);
                                script = script.Insert(index + 1, parameterText);

                                nextIndex = index + 1 + parameterText.Length;

                                index = nextIndex;

                                IDataElement dataElement = DataElement.Create(
                                    (parameterValue == null ? DataValueType.Text : parameterValue.GetValueType()),
                                    "Parameter" + scriptWordParameterCount.ToString());
                                if (dataElement != null)
                                {
                                    if (dataElement.Specification != null)
                                        dataElement.Specification.MaximumItemNumber = 1;
                                    dataElement.SetItem(parameterValue);

                                    scriptWord.ParameterDetail.AddElement(dataElement);
                                    scriptWordParameterCount++;
                                }
                            }
                            else
                            {
                                index = nextIndex = index + 1;
                            }
                        }
                    }
                }
                else if (scriptItemKind == ScriptItemKind.Variable)
                {
                    // we look for the next ")" character.
                    nextIndex = ScriptParsingHelper.GetIndexOfNextString(script, ")", index + 2);
                    if (nextIndex >= script.Length)
                    {
                        if (log != null)
                        {
                            ILogEvent logEvent = log.AddError(
                                title: "Syntax Error: Character ')' needed for function has not been found. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_SYNTAXERROR");
                            logEvent.Detail = new DataElementSet();
                            logEvent.Detail.AddElement("Position", (index + offsetIndex).ToString());
                        }
                        return null;
                    }

                    // we instantiate the script word
                    scriptWord = new ScriptWord
                    {
                        // we retrieve the function name
                        Name = script.GetSubstring(index, nextIndex - 1).Trim()
                    };
                    index = nextIndex;
                }

                if (scriptWord != null)
                {
                    scriptWord.Kind = scriptItemKind;

                    // we try to find the corresponding defined function
                    List<IScriptWordDefinition> scriptWordDefinitions =
                        Index.GetDefinitionsWithExactName(scriptWord.Name, parentScriptWord?.Definition);
                    if (scriptWordDefinitions.Count == 0)
                    {
                        if (log != null)
                        {
                            ILogEvent logEvent = log.AddError(
                                title: "Function named '" + scriptWord.Name + "' not defined",
                                description: "Syntax error: Function named '" + scriptWord.Name + "' not defined" +
                                    (parentScriptWord == null ? "" : " for parent function '" + parentScriptWord.Name + "'") +
                                    ". Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_NOTEXISTINGWORD");
                            logEvent.Detail = new DataElementSet();
                            logEvent.Detail.AddElement("Position", (index + offsetIndex).ToString());
                        }
                        return null;
                    }
                    else
                    {
                        IScriptWordDefinition scriptWordDefinition = null;
                        foreach (IScriptWordDefinition currentScriptWordDefinition in scriptWordDefinitions)
                        {
                            if (Index.IsWordMatching(scriptWord, currentScriptWordDefinition))
                            {
                                scriptWordDefinition = currentScriptWordDefinition;
                                break;
                            }
                        }

                        // if no defined script word match then
                        if (scriptWordDefinition == null)
                        {
                            if (log != null)
                            {
                                ILogEvent logEvent = log.AddError(
                                    title: "Invalid arguments: Function called '" + scriptWord.Name + "' has invalid parameters. Either the number of parameters does not match or their value types. Position " + (index + offsetIndex),
                                    resultCode: "SCRIPT_INVALIDARGUMENT"
                                    );
                                logEvent.Detail = new DataElementSet();
                                logEvent.Detail.AddElement("Position", (index + offsetIndex).ToString());
                            }
                            return null;
                        }
                        else
                        {
                            // else we affect the correct method name and is unlimited properties
                            if (scriptWordDefinition.RuntimeFunctionName == null)
                            {
                                if (log != null)
                                {
                                    ILogEvent logEvent = log.AddError(
                                        title: "Invalid definition: Method not defined for function called '" + scriptWord.Name + "'. Position " + (index + offsetIndex),
                                        resultCode: "SCRIPT_DEFINITION");
                                    logEvent.Detail = new DataElementSet();
                                    logEvent.Detail.AddElement("Position", (index + offsetIndex).ToString());
                                }
                                return null;
                            }
                            scriptWord.SetDefinition(scriptWordDefinition);
                            scriptWord.Parent = parentScriptWord;

                            // if the script word is a variable then we retrieve the sub script word
                            if (script.GetSubstring(nextIndex + 1, nextIndex + 1) == ".")
                            {
                                // we evaluate the variable value
                                scriptWord.Item = EvaluateWord(
                                    scriptWord, index, scriptVariableSet, isSimulationModeOn, log);

                                nextIndex++;
                                scriptWord = FindNextScriptWord(
                                    ref script,
                                    scriptWord,
                                    ref nextIndex,
                                    offsetIndex,
                                    scriptVariableSet,
                                    isSimulationModeOn,
                                    log);
                                if (scriptWord != null) index = nextIndex;
                            }
                            if (script.GetSubstring(nextIndex + 1, nextIndex + 1) == " ")
                            {
                                index = nextIndex + 1;
                            }
                        }
                    }
                }
            }

            // we return the result
            return scriptWord;
        }

        // Returns the result of the script word scriptWord with the specified parameter values
        private string EvaluateWord(
            IScriptWord scriptWord,
            int offsetIndex,
            IScriptVariableSet scriptVariableSet = null,
            bool isSimulationModeOn = false,
            ILog log = null)
        {
            if (_appScope == null) return null;
            if ((scriptWord == null) || (scriptWord.Definition == null)) return null;

            string resultString = "<Evaluation_Error />";
            if (isSimulationModeOn)
            {
                resultString = "<Evaluation_Ok />";
            }
            else if (scriptWord.Definition.RuntimeFunction != null)
            {
                try
                {
                    object[] parameters = (scriptWord.ParameterDetail == null ?
                        new object[0] : scriptWord.ParameterDetail.GetElementItemObjects().ToArray());
                    resultString = scriptWord.Definition.RuntimeFunction(_appScope, scriptVariableSet, scriptWord, parameters);
                }
                catch (Exception ex)
                {
                    if (log != null)
                    {
                        ILogEvent logEvent = log.AddError(
                            "Raised the following exception: " + ex.ToString(),
                            EventCriticality.High,
                            "",
                            "Evaluation Error: Error when tempting to evaluate function " +
                            "(Name='" + scriptWord.Name + "';BusinessLibraryName='" + scriptWord.Definition.LibraryName + "')" +
                            ". Position " + (offsetIndex) + ".",
                            "SCRIPT_EVALUATION"
                            );
                        logEvent.Detail = new DataElementSet();
                        logEvent.Detail.AddElement("Position", offsetIndex.ToString());
                    }
                }
            }

            return resultString;
        }

        #endregion
    }
}