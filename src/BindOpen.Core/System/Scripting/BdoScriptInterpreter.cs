using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Diagnostics.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Scripting
{
    /// <summary>
    /// This class represents a script interpreter. A script interpreter allows to interpret a script
    /// using script dictionnaries and data context, providing a log for the interpretation task.
    /// </summary>
    public class BdoScriptInterpreter : DataItem, IBdoScriptInterpreter
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly IBdoScope _scope = null;

        private Dictionary<string, IBdoScriptwordDefinition> Definitions => _scope?.ExtensionStore?.GetItemDefinitions<IBdoScriptwordDefinition>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptInterpreter class.
        /// </summary>
        public BdoScriptInterpreter()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoScriptInterpreter class.
        /// </summary>
        /// <param name="scope">The application scope used to interprete.</param>
        public BdoScriptInterpreter(IBdoScope scope)
        {
            _scope = scope;
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
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
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
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return Evaluate(dataExpression, out _, scriptVariableSet, log);
        }

        /// <summary>
        /// Evaluates the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="dataExpressionKind">The expression kind to consider.</param>
        /// <param name="resultScript">The result script to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public object Evaluate(
            string script,
            DataExpressionKind dataExpressionKind,
            out string resultScript,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return EvaluateScript(script, DataExpressionKind.Script, out resultScript, scriptVariableSet, log);
        }

        /// <summary>
        /// Evaluates the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="dataExpressionKind">The expression kind to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public object Evaluate(
            string script,
            DataExpressionKind dataExpressionKind,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return EvaluateScript(script, DataExpressionKind.Script, out _, scriptVariableSet, log);
        }

        // Interpretation ------------------------------------

        /// <summary>
        /// Interprets the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="dataExpression">The data expression to consider.</param>
        /// <param name="expressionKind">The expression kind to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public string Interprete(
            IDataExpression dataExpression,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            Evaluate(dataExpression, out string resultScript, scriptVariableSet, log);
            return resultScript;
        }

        /// <summary>
        /// Interprets the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="expressionKind">The expression kind to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public string Interprete(
            string script,
            DataExpressionKind dataExpressionKind,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            EvaluateScript(script, dataExpressionKind, out string resultScript, scriptVariableSet, log);
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
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
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
                                EvaluateScript(script, DataExpressionKind.Script, out string stringValue, scriptVariableSet, log);
                                stringValue = stringValue.Substring(2);
                                stringValue = stringValue.Substring(0, stringValue.Length - 2);

                                resultScript = resultScript.Replace(
                                    resultScript.GetSubstring(scriptWordBeginIndex, index + 1), stringValue);
                                scriptWordBeginIndex = script.IndexOf("{{", index + 1);
                            }
                        }
                    }
                    break;
                case DataExpressionKind.Script:
                    IBdoLog subLog = new BdoLog();

                    // we parse the text to interpretate
                    index = 0;
                    scriptWordBeginIndex = index;

                    // we get the next function or variable
                    item = Interprete(script, out resultScript, ref index, 0, scriptVariableSet, false, subLog);

                    if (subLog.AddEvents(log).HasErrorsOrExceptions())
                    {
                        log?.AddError(
                            title: "Error occured while interpreting script",
                            description: "Error while interpreting the script '" + script + "'. " +
                                "The result is '" + resultScript + "'.",
                            resultCode: "SCRIPTINTERPRETATIONERROR"
                            );
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
            IBdoScriptVariableSet scriptVariableSet = null,
            bool isSimulationModeOn = false,
            IBdoLog log = null)
        {
            object evaluatedValue = resultScript = script;
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
                    IBdoScriptword scriptWord = FindNextScriptword(
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
                        IBdoScriptword rootScSriptWord = scriptWord.Root();
                        if (rootScSriptWord != null)
                        {
                            scriptWordBeginIndex = resultScript.IndexOf(
                               rootScSriptWord.Kind == ScriptItemKinds.Function ?
                               BdoScriptParsingHelper.Symbol_Fun + rootScSriptWord.Name :
                               BdoScriptParsingHelper.Symbol_Var + rootScSriptWord.Name);
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
        /// <param name="parentScriptword">The parent script word to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <param name="offsetIndex">The offset index to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="isSimulationModeOn">Indicates whether the simulation mode is on.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public IBdoScriptword FindNextScriptword(
            ref string script,
            IBdoScriptword parentScriptword,
            ref int index,
            int offsetIndex,
            IBdoScriptVariableSet scriptVariableSet = null,
            bool isSimulationModeOn = false,
            IBdoLog log = null)
        {
            if (parentScriptword != null && parentScriptword.Definition == null)
            {
                if (log != null)
                {
                    IBdoLogEvent logEvent = log.AddError(
                    title: "Syntax error: Function named '" + parentScriptword.Name + "' not defined. Position " + (index + offsetIndex),
                    resultCode: "SCRIPT_NOTEXISTINGWORD");
                    logEvent.Detail = new DataElementSet();
                    logEvent.Detail.AddElement(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                }
                return null;
            }

            IBdoScriptword scriptWord = null;

            // we retrieve the type of the next script word
            ScriptItemKinds scriptItemKind = ScriptItemKinds.None;
            int nextIndex = -1;
            int nextFuncIndex = script.IndexOf(BdoScriptParsingHelper.Symbol_Fun, index);
            int nextVarIndex = script.IndexOf(BdoScriptParsingHelper.Symbol_Var, index);

            // if the script word begins and ends with a quote then it is a variable
            if (script.Trim().StartsWith("'") && script.Trim().EndsWith("'"))
            {
                index = script.Length;
                scriptItemKind = ScriptItemKinds.None;
            }
            // else if the next word is a variable
            else if (parentScriptword != null && script.GetSubstring(index, index) == ".")
            {
                index++;
                scriptItemKind = ScriptItemKinds.Function;
            }
            else if ((nextVarIndex > -1) && ((nextFuncIndex == -1) || (nextVarIndex <= nextFuncIndex)))
            {
                index = nextVarIndex + 2;
                scriptItemKind = ScriptItemKinds.Variable;
            }
            // else if the next word is a function
            else if ((nextFuncIndex > -1) && ((nextVarIndex == -1) || (nextFuncIndex < nextVarIndex)))
            {
                index = nextFuncIndex + 1;
                scriptItemKind = ScriptItemKinds.Function;
            }
            else
            {
                index = script.Length;
            }

            // if a next script word has been found then
            if (scriptItemKind != ScriptItemKinds.None)
            {
                if (scriptItemKind == ScriptItemKinds.Function)
                {
                    // we look for the next "(" character.
                    nextIndex = BdoScriptParsingHelper.GetIndexOfNextString(script, "(", index);
                    if (nextIndex >= script.Length)
                    {
                        if (log != null)
                        {
                            IBdoLogEvent logEvent = log.AddError(
                                title: "Syntax Error: Required character '(' for functions missing. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_SYNTAXERROR");
                            logEvent.Detail = new DataElementSet();
                            logEvent.Detail.AddElement(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                        }
                        return null;
                    }

                    // we instantiate the script word
                    scriptWord = new BdoScriptword
                    {
                        Name = script.GetSubstring(index, nextIndex - 1).Trim()
                    };
                    index = nextIndex;
                    // we retrieve the function parameters
                    int scriptWordParameterCount = 0;
                    while ((script.Substring(nextIndex, 1) != ")") && (nextIndex < script.Length))
                    {
                        nextIndex = BdoScriptParsingHelper.GetIndexOfNextString(script, ",", nextIndex + 1);
                        // if the next index is out of range
                        if (nextIndex >= script.Length)
                        {
                            if (log != null)
                            {
                                BdoIEvent logEvent = log.AddError(
                                    title: "Syntax Error: Character ')' not found for function. Position " + (index + offsetIndex),
                                    resultCode: "SCRIPT_SYNTAXERROR");
                                logEvent.Detail = new DataElementSet();
                                logEvent.Detail.AddElement(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                            }
                            return null;
                        }
                        else
                        {
                            // else we get the parameter value and we interprete it
                            string scriptWordParameterValue = script.Substring(index + 1, nextIndex - index - 1).Trim();
                            if (!string.IsNullOrEmpty(scriptWordParameterValue))
                            {
                                int subIndex = 0;
                                object parameterValue = Interprete(
                                    scriptWordParameterValue,
                                    out string parameterText,
                                    ref subIndex,
                                    offsetIndex + index + 1,
                                    scriptVariableSet,
                                    isSimulationModeOn,
                                    log);
                                parameterText = parameterText.Trim();

                                script = script.Remove(index + 1, nextIndex - index - 1);
                                script = script.Insert(index + 1, parameterText);

                                nextIndex = index + 1 + parameterText.Length;

                                index = nextIndex;

                                IDataElement dataElement = ElementFactory.CreateScalar(
                                    "Parameter" + scriptWordParameterCount.ToString(),
                                    parameterValue == null ? DataValueType.Text : parameterValue.GetValueType());
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
                else if (scriptItemKind == ScriptItemKinds.Variable)
                {
                    // we look for the next ")" character.
                    nextIndex = BdoScriptParsingHelper.GetIndexOfNextString(script, ")", index + 2);
                    if (nextIndex >= script.Length)
                    {
                        if (log != null)
                        {
                            IBdoLogEvent logEvent = log.AddError(
                                title: "Syntax Error: Character ')' needed for function has not been found. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_SYNTAXERROR");
                            logEvent.Detail = new DataElementSet();
                            logEvent.Detail.AddElement(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                        }
                        return null;
                    }

                    // we instantiate the script word
                    scriptWord = new BdoScriptword
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
                    var scriptWordDefinitions = GetDefinitionsWithExactName(scriptWord.Name, parentScriptword?.Definition);

                    if (scriptWordDefinitions.Count == 0)
                    {
                        if (log != null)
                        {
                            IBdoLogEvent logEvent = log.AddError(
                                title: "Function named '" + scriptWord.Name + "' not defined",
                                description: "Syntax error: Function named '" + scriptWord.Name + "' not defined" +
                                    (parentScriptword == null ? "" : " for parent function '" + parentScriptword.Name + "'") +
                                    ". Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_NOTEXISTINGWORD");
                            logEvent.Detail = new DataElementSet();
                            logEvent.Detail.AddElement(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                        }
                        return null;
                    }
                    else
                    {
                        IBdoScriptwordDefinition scriptWordDefinition = null;
                        foreach (var pair in scriptWordDefinitions)
                        {
                            if (IsWordMatching(scriptWord, pair.Value))
                            {
                                scriptWordDefinition = pair.Value;
                                break;
                            }
                        }

                        // if no defined script word match then
                        if (scriptWordDefinition == null)
                        {
                            if (log != null)
                            {
                                IBdoLogEvent logEvent = log.AddError(
                                    title: "Invalid arguments: Function called '" + scriptWord.Name + "' has invalid parameters. Either the number of parameters does not match or their value types. Position " + (index + offsetIndex),
                                    resultCode: "SCRIPT_INVALIDARGUMENT"
                                    );
                                logEvent.Detail = new DataElementSet();
                                logEvent.Detail.AddElement(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                            }
                            return null;
                        }
                        else
                        {
                            // else we affect the correct method name and is unlimited properties
                            if (scriptWordDefinition.RuntimeFunction == null)
                            {
                                if (log != null)
                                {
                                    IBdoLogEvent logEvent = log.AddError(
                                        title: "Invalid definition: Method not defined for function called '" + scriptWord.Name + "'. Position " + (index + offsetIndex),
                                        resultCode: "SCRIPT_DEFINITION");
                                    logEvent.Detail = new DataElementSet();
                                    logEvent.Detail.AddElement(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                                }
                                return null;
                            }
                            scriptWord.SetDefinition(scriptWordDefinition);
                            scriptWord.Parent = parentScriptword;

                            // if the script word is a variable then we retrieve the sub script word
                            if (script.GetSubstring(nextIndex + 1, nextIndex + 1) == ".")
                            {
                                // we evaluate the variable value
                                scriptWord.Item = EvaluateWord(
                                    scriptWord, index, scriptVariableSet, isSimulationModeOn, log);

                                nextIndex++;
                                scriptWord = FindNextScriptword(
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
            IBdoScriptword scriptWord,
            int offsetIndex,
            IBdoScriptVariableSet scriptVariableSet = null,
            bool isSimulationModeOn = false,
            IBdoLog log = null)
        {
            if (_scope == null) return null;
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
                        Array.Empty<object>() : scriptWord.ParameterDetail?.Elements?.Select(p => p.GetObject()).ToArray());
                    resultString = scriptWord.Definition.RuntimeFunction(_scope, scriptVariableSet, scriptWord, parameters);
                }
                catch (Exception ex)
                {
                    if (log != null)
                    {
                        IBdoLogEvent logEvent = log.AddError(
                            "Raised the following exception: " + ex.ToString(),
                            BdoEventCriticality.High,
                            "",
                            "Evaluation Error: Error when tempting to evaluate function " +
                            "(Name='" + scriptWord.Name + "';BusinessLibraryName='" + scriptWord.Definition.Dto?.LibraryId + "')" +
                            ". Position " + (offsetIndex) + ".",
                            "SCRIPT_EVALUATION"
                            );
                        logEvent.Detail = new DataElementSet();
                        logEvent.Detail.AddElement(ElementFactory.CreateScalar("Position", offsetIndex.ToString()));
                    }
                }
            }

            return resultString;
        }

        /// <summary>
        /// Determines whether the specified script word corresponds to the specified definition.
        /// </summary>
        /// <param name="scriptWord">The script word to consider.</param>
        /// <param name="scriptWordDefinition">The script word definition to consider.</param>
        /// <returns></returns>
        public bool IsWordMatching(IBdoScriptword scriptWord, IBdoScriptwordDefinition scriptWordDefinition)
        {
            if (scriptWordDefinition == null || scriptWordDefinition.Dto == null) return false;

            var definitionDto = scriptWordDefinition.Dto;

            // we check the number of parameters
            if ((!definitionDto.IsRepeatedParameters) && ((scriptWord.ParameterDetail == null) && (definitionDto.ParameterSpecification != null) |
                (definitionDto.ParameterSpecification == null) && (scriptWord.ParameterDetail != null)))
            {
                return false;
            }

            if ((scriptWord.ParameterDetail == null) && ((!definitionDto.IsRepeatedParameters) || (definitionDto.ParameterSpecification == null)))
                return true;
            if ((!definitionDto.IsRepeatedParameters) && (scriptWord.ParameterDetail.Count != definitionDto.ParameterSpecification.Count))
                return false;

            if ((!definitionDto.IsRepeatedParameters) && ((definitionDto.MaxParameterNumber != -1) && (scriptWord.ParameterDetail.Count > definitionDto.MaxParameterNumber)))
                return false;
            if ((!definitionDto.IsRepeatedParameters) && ((definitionDto.MinParameterNumber != -1) && (scriptWord.ParameterDetail.Count < definitionDto.MinParameterNumber)))
                return false;

            // we search the defined script word parameters

            int parameterIndex = 0;
            //if ((definitionDto.IsRepeatedParameters) & (definitionDto.RepeatedParameterValueType != scriptWord.Definition.RepeatedParameterValueType))
            //    return false;
            if (definitionDto.ParameterSpecification.Items != null)
            {
                foreach (IDataElementSpec parameterSpecification in definitionDto.ParameterSpecification.Items)
                {
                    IScalarElement parameter = scriptWord.ParameterDetail[parameterIndex] as IScalarElement;

                    // we check that the value type of the current script word parameter corresponds to the defined one (considering the en-US culture info)
                    if (((definitionDto.IsRepeatedParameters) && (definitionDto.RepeatedParameterValueType == DataValueType.Text))
                        || ((!definitionDto.IsRepeatedParameters) && (parameterSpecification.ValueType == DataValueType.Text)))
                    {
                        String parameterValue = (parameter.GetObject() ?? "").ToString().Trim();

                        if (parameterValue.Length < 2)
                            return false;
                        if ((!parameterValue.StartsWith("'")) || (!parameterValue.EndsWith("'")))
                            return false;
                    }
                    else
                    {
                        if ((!definitionDto.IsRepeatedParameters) && (parameterSpecification.ValueType != DataValueType.Any))
                        {
                            return !parameterSpecification.CheckItem(parameter.GetObject(), parameter).HasErrorsOrExceptions();
                        }
                    }
                    parameterIndex++;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the script word definitions of this instance.
        /// </summary>
        /// <returns>Returns the script word definitions of this instance.</returns>
        public Dictionary<string, IBdoScriptwordDefinition> GetDefinitions() => Definitions;

        /// <summary>
        /// Returns the word definitions with the specified name.
        /// </summary>
        /// <param name="name">The name of the script words to return.</param>
        /// <param name="parentDefinition">The parent definition.</param>
        /// <returns>The script words with the specified name.</returns>
        public Dictionary<string, IBdoScriptwordDefinition> GetDefinitionsWithExactName(
            string name,
            IBdoScriptwordDefinition parentDefinition = null)
        {
            var matchingDefinitions = new Dictionary<string, IBdoScriptwordDefinition>();

            if (name != null)
            {
                Dictionary<string, IBdoScriptwordDefinition> poolScriptwordDefinitions = null;
                if (parentDefinition == null)
                    poolScriptwordDefinitions = Definitions;
                else if (parentDefinition.Children != null)
                    poolScriptwordDefinitions = parentDefinition.Children;

                matchingDefinitions = new Dictionary<string, IBdoScriptwordDefinition>();
                if (poolScriptwordDefinitions != null)
                    foreach (var def in poolScriptwordDefinitions.Where(p => p.Value?.Dto?.Name.KeyEquals(name) == true).Select(p => p.Value))
                    {
                        matchingDefinitions.Add(def.UniqueId, def);
                    }
            }

            return matchingDefinitions;
        }

        /// <summary>
        /// Gets the word definitions approximatively with the specified name.
        /// </summary>
        /// <param name="name">The name of the script words to return.</param>
        /// <param name="parentDefinition">The parent definition.</param>
        /// <returns>The script words with the specified name.</returns>
        public Dictionary<string, IBdoScriptwordDefinition> GetDefinitionsWithApproximativeName(
            string name,
            IBdoScriptwordDefinition parentDefinition = null)
        {
            var matchingDefinitions = new Dictionary<string, IBdoScriptwordDefinition>();
            if (name == null)
                return matchingDefinitions;

            Dictionary<string, IBdoScriptwordDefinition> poolScriptwordDefinitions = null;
            if (parentDefinition == null)
                poolScriptwordDefinitions = Definitions;
            else if (parentDefinition.Children != null)
                poolScriptwordDefinitions = new Dictionary<string, IBdoScriptwordDefinition>(parentDefinition.Children);

            matchingDefinitions = new Dictionary<string, IBdoScriptwordDefinition>();
            if (poolScriptwordDefinitions != null)
                foreach (var def in poolScriptwordDefinitions.Where(p => p.Value?.Dto?.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) > 0).Select(p => p.Value))
                {
                    matchingDefinitions.Add(def.UniqueId, def);
                }

            return matchingDefinitions;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _scope?.Dispose();

            _isDisposed = true;

            if (isDisposing)
            {
                GC.SuppressFinalize(this);
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}