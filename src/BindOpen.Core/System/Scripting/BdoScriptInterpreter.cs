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
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

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
        // EVALUATION
        // ------------------------------------------

        #region Evaluation

        // Expression

        /// <summary>
        /// Evaluates the specified data expression.
        /// </summary>
        /// <param name="expression">The data expression to consider.</param>
        /// <param name="resultScript">The result script to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Literal or script value according to the specified default mode.</returns>
        public object Evaluate(
            IDataExpression expression,
            out string resultScript,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            int index;
            int scriptwordBeginIndex;

            var script = expression?.Text ?? "";
            resultScript = script;
            switch (expression.Kind)
            {
                case DataExpressionKind.Auto:
                    if (!string.IsNullOrEmpty(script))
                    {
                        scriptwordBeginIndex = script.IndexOf("{{");
                        while (scriptwordBeginIndex > -1)
                        {
                            index = script.GetIndexOfNextString("}}", scriptwordBeginIndex + 1);

                            if ((scriptwordBeginIndex > -1) && (index > -1))
                            {
                                Evaluate(DataExpressionFactory.CreateScript(script), out string stringValue, scriptVariableSet, log);
                                stringValue = stringValue.Substring(2);
                                stringValue = stringValue[0..^2];

                                resultScript = resultScript.Replace(
                                    resultScript.GetSubstring(scriptwordBeginIndex, index + 1), stringValue);
                                scriptwordBeginIndex = script.IndexOf("{{", index + 1);
                            }
                        }

                        return null;
                    }
                    break;
                case DataExpressionKind.Script:
                    if (!string.IsNullOrEmpty(script))
                    {
                        IBdoLog subLog = new BdoLog();

                        // we parse the text to interpretate
                        index = 0;
                        scriptwordBeginIndex = index;

                        // we get the next function or variable
                        var item = Evaluate(script, out resultScript, ref index, 0, scriptVariableSet, subLog);

                        if (subLog.AddEvents(log).HasErrorsOrExceptions())
                        {
                            log?.AddError(
                                title: "Error occured while interpreting script",
                                description: "Error while interpreting the script '" + script + "'. " +
                                    "The result is '" + resultScript + "'.",
                                resultCode: "SCRIPTINTERPRETATIONERROR"
                                );
                        }

                        return item;
                    }
                    break;
                case DataExpressionKind.Word:
                    if (expression.Word != null)
                    {
                        var cloned = new BdoScriptword
                        {
                            Name = expression.Word.Name
                        };
                        cloned.SetDefinition(expression.Word.Definition);
                        //cloned.SetConfiguration(scriptword.Configuration);

                        switch (expression.Word.Kind)
                        {
                            case ScriptItemKinds.Function:
                                resultScript = BdoScriptParsingHelper.Symbol_Fun + expression.Word.Name + "(";

                                if (expression.Word.Parameters?.Count > 0)
                                {
                                    //ICI: Update result string
                                    cloned.Parameters = new List<object>();
                                    foreach (var paramValue in expression.Word.Parameters)
                                    {
                                        if (paramValue is BdoScriptword scriptwordParam)
                                        {
                                            var paramObject = Evaluate(scriptwordParam.CreateExpression(), scriptVariableSet, log);
                                            cloned.Parameters.Add(paramObject);
                                        }
                                        else
                                        {
                                            cloned.Parameters.Add(paramValue);
                                        }
                                    }
                                }
                                break;
                            case ScriptItemKinds.Variable:
                                resultScript = BdoScriptParsingHelper.Symbol_Var + "(" + expression.Word.Name;
                                break;
                            default:
                                resultScript = expression.Word.Name;
                                return resultScript;
                        }

                        resultScript += ")";
                        return InterpreteScriptword(cloned, scriptVariableSet, log);
                    }
                    break;
            }

            return expression?.Text;
        }

        /// <summary>
        /// Evaluates the specified data expression.
        /// </summary>
        /// <param name="expression">The data expression to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Literal or script value according to the specified default mode.</returns>
        public object Evaluate(
            IDataExpression expression,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return Evaluate(expression, out _, scriptVariableSet, log);
        }

        // String

        /// <summary>
        /// Evaluates the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="expressionKind">The expression kind to consider.</param>
        /// <param name="resultScript">The result script to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public object Evaluate(
            string script,
            DataExpressionKind expressionKind,
            out string resultScript,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return Evaluate(DataExpressionFactory.Create(expressionKind, script), out resultScript, scriptVariableSet, log);
        }

        /// <summary>
        /// Evaluates the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="expressionKind">The expression kind to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public object Evaluate(
            string script,
            DataExpressionKind expressionKind,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return Evaluate(script, expressionKind, out _, scriptVariableSet, log);
        }

        // Script word

        /// <summary>
        /// Evaluates the specified script word using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public object Evaluate(
            BdoScriptword scriptword,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return Evaluate(scriptword.CreateExpression(), scriptVariableSet, log);
        }

        #endregion

        // ------------------------------------------
        // INTERPRETATION
        // ------------------------------------------

        #region Interpretation

        /// <summary>
        /// Interprets the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="expression">The data expression to consider.</param>
        /// <param name="expressionKind">The expression kind to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public string Interprete(
            IDataExpression expression,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            Evaluate(expression, out string resultScript, scriptVariableSet, log);
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
            DataExpressionKind expressionKind,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            Evaluate(DataExpressionFactory.Create(expressionKind, script), out string resultScript, scriptVariableSet, log);
            return resultScript;
        }

        #endregion

        // ------------------------------------------
        // PRIVATE METHODS
        // ------------------------------------------

        #region Private_Methods

        // Returns the standard-interpretated text
        private object Evaluate(
            string script,
            out string resultScript,
            ref int index,
            int offsetIndex,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            object evaluatedValue = resultScript = script;

            if (script == null)
            {
                resultScript = "";
            }
            else
            {
                // we parse the text to interpretate
                index = 0;
                while (index < resultScript.Length)
                {
                    int scriptwordBeginIndex = index;

                    // we get the next function or variable
                    var scriptword = FindNextScriptword(
                        ref resultScript,
                        null,
                        ref index,
                        offsetIndex,
                        scriptVariableSet,
                        log);

                    // if the script word has been found
                    if (scriptword != null)
                    {
                        // we replace the script word by its value
                        string evaluatedString = "";
                        evaluatedValue = scriptword.Item = InterpreteScriptword(
                            scriptword, scriptVariableSet, log, index + offsetIndex);
                        if (evaluatedValue != null)
                            evaluatedString = evaluatedValue.ToString();

                        //we retrieve the index of the root script word
                        IBdoScriptword rootScSriptWord = scriptword.Root();
                        if (rootScSriptWord != null)
                        {
                            scriptwordBeginIndex = resultScript.IndexOf(
                               rootScSriptWord.Kind == ScriptItemKinds.Function ?
                               BdoScriptParsingHelper.Symbol_Fun + rootScSriptWord.Name :
                               BdoScriptParsingHelper.Symbol_Var + rootScSriptWord.Name);
                        }

                        if (scriptwordBeginIndex > -1)
                        {
                            resultScript = resultScript.Replace(
                               resultScript.Substring(scriptwordBeginIndex, index - scriptwordBeginIndex + 1), evaluatedString);
                        }

                        index = scriptwordBeginIndex + evaluatedString.Length - 1;
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
        /// <param name="log"></param>
        /// <returns></returns>
        public IBdoScriptword FindNextScriptword(
            ref string script,
            IBdoScriptword parentScriptword,
            ref int index,
            int offsetIndex,
            IScriptVariableSet scriptVariableSet = null,
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
                    logEvent.Detail.Add(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                }
                return null;
            }

            IBdoScriptword scriptword = null;

            // we retrieve the type of the next script word
            ScriptItemKinds scriptItemKind = ScriptItemKinds.None;
            int nextIndex = -1;

            // if the script word begins and ends with a quote then it is a constant
            if (script.Trim().StartsWith("'") && script.Trim().EndsWith("'"))
            {
                index = script.Length;
            }
            // else if the next word is a variable
            else if (parentScriptword != null && script.GetSubstring(index, index) == ".")
            {
                index++;
                scriptItemKind = ScriptItemKinds.Function;
            }
            else
            {
                int nextFuncIndex = script.IndexOf(BdoScriptParsingHelper.Symbol_Fun, index);

                // if a potential function or variable was found
                if (nextFuncIndex > -1)
                {
                    if (nextFuncIndex < script.Length - 1 &&
                        script.Substring(nextFuncIndex, 2) == BdoScriptParsingHelper.Symbol_Var)
                    {
                        index = nextFuncIndex + 2;
                        scriptItemKind = ScriptItemKinds.Variable;
                    }
                    else
                    {
                        index = nextFuncIndex + 1;
                        scriptItemKind = ScriptItemKinds.Function;
                    }
                }
                else
                {
                    index = script.Length;
                }
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
                            logEvent.Detail.Add(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                        }
                        return null;
                    }

                    // we instantiate the script word
                    scriptword = new BdoScriptword
                    {
                        Name = script.GetSubstring(index, nextIndex - 1).Trim()
                    };
                    index = nextIndex;

                    // we retrieve the function parameters
                    int scriptwordParameterCount = 0;
                    while ((script.Substring(nextIndex, 1) != ")") && (nextIndex < script.Length))
                    {
                        nextIndex = BdoScriptParsingHelper.GetIndexOfNextString(script, ",", nextIndex + 1);

                        // if the next index is out of range
                        if (nextIndex >= script.Length)
                        {
                            if (log != null)
                            {
                                IBdoEvent logEvent = log.AddError(
                                    title: "Syntax Error: Character ')' not found for function. Position " + (index + offsetIndex),
                                    resultCode: "SCRIPT_SYNTAXERROR");
                                logEvent.Detail = new DataElementSet();
                                logEvent.Detail.Add(ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                            }
                            return null;
                        }
                        else
                        {
                            // else we get the parameter value and we interprete it
                            string parameterStringValue = script.Substring(index + 1, nextIndex - index - 1).Trim();
                            if (!string.IsNullOrEmpty(parameterStringValue))
                            {
                                int subIndex = 0;
                                object parameterValue = Evaluate(
                                    parameterStringValue,
                                    out string parameterText,
                                    ref subIndex,
                                    offsetIndex + index + 1,
                                    scriptVariableSet,
                                    log);
                                parameterText = parameterText.Trim();

                                script = script.Remove(index + 1, nextIndex - index - 1);
                                script = script.Insert(index + 1, parameterText);

                                nextIndex = index + 1 + parameterText.Length;

                                index = nextIndex;

                                scriptword.Parameters.Add(parameterValue);
                                scriptwordParameterCount++;
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
                            logEvent.Detail = ElementFactory.CreateSet(
                                ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                        }
                        return null;
                    }

                    // we instantiate the script word
                    scriptword = new BdoScriptword
                    {
                        // we retrieve the function name
                        Name = script.GetSubstring(index, nextIndex - 1).Trim()
                    };
                    index = nextIndex;
                }

                if (scriptword != null)
                {
                    scriptword.Kind = scriptItemKind;
                    scriptword.Parent = parentScriptword;

                    if (FindScriptwordDefinition(scriptword, log, index, offsetIndex))
                    {
                        // if the script word is a variable then we retrieve the sub script word
                        if (scriptword?.Definition?.Dto?.Kind == ScriptItemKinds.Variable
                            && script.GetSubstring(nextIndex + 1, nextIndex + 1) == ".")
                        {
                            // we evaluate the variable value
                            scriptword.Item = InterpreteScriptword(scriptword, scriptVariableSet, log, index);

                            nextIndex++;
                            scriptword = FindNextScriptword(
                                ref script,
                                scriptword,
                                ref nextIndex,
                                offsetIndex,
                                scriptVariableSet,
                                log);
                            if (scriptword != null) index = nextIndex;
                        }
                        if (script.GetSubstring(nextIndex + 1, nextIndex + 1) == " ")
                        {
                            index = nextIndex + 1;
                        }
                    }
                }
            }

            // we return the result
            return scriptword;
        }

        private bool FindScriptwordDefinition(
            IBdoScriptword scriptword,
            IBdoLog log = null,
            int index = 0,
            int offsetIndex = 0)
        {
            if (scriptword != null)
            {
                // we try to find the corresponding defined function
                var scriptwordDefinitions = GetDefinitionsWithExactName(scriptword.Name, scriptword.Parent?.Definition);

                if (scriptwordDefinitions.Count == 0)
                {
                    IBdoLogEvent logEvent = log?.AddError(
                        title: "Function named '" + scriptword.Name + "' not defined",
                        description: "Syntax error: Function named '" + scriptword.Name + "' not defined" +
                            (scriptword.Parent == null ? "" : " for parent function '" + scriptword.Parent?.Name + "'") +
                            ". Position " + (index + offsetIndex),
                        resultCode: "SCRIPT_NOTEXISTINGWORD");
                    if (logEvent != null)
                    {
                        logEvent.Detail = ElementFactory.CreateSet(
                            ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                    }
                }
                else
                {
                    IBdoScriptwordDefinition scriptwordDefinition = null;
                    foreach (var pair in scriptwordDefinitions)
                    {
                        if (IsWordMatching(scriptword, pair.Value))
                        {
                            scriptwordDefinition = pair.Value;
                            break;
                        }
                    }

                    // if no defined script word match then
                    if (scriptwordDefinition == null)
                    {
                        if (log != null)
                        {
                            IBdoLogEvent logEvent = log.AddError(
                                title: "Invalid arguments: Function called '" + scriptword.Name + "' has invalid parameters. Either the number of parameters does not match or their value types. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_INVALIDARGUMENT"
                                );
                            logEvent.Detail = ElementFactory.CreateSet(
                                ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                        }
                    }
                    else
                    {
                        // else we affect the correct method name and is unlimited properties
                        if (scriptwordDefinition.RuntimeFunction == null)
                        {
                            if (log != null)
                            {
                                IBdoLogEvent logEvent = log.AddError(
                                    title: "Invalid definition: Method not defined for function called '" + scriptword.Name + "'. Position " + (index + offsetIndex),
                                    resultCode: "SCRIPT_DEFINITION");
                                logEvent.Detail = ElementFactory.CreateSet(
                                        ElementFactory.CreateScalar("Position", (index + offsetIndex).ToString()));
                            }
                        }
                        scriptword.SetDefinition(scriptwordDefinition);

                        return true;
                    }
                }
            }

            return false;
        }

        // Returns the result of the script word scriptword with the specified parameter values
        private string InterpreteScriptword(
            IBdoScriptword scriptword,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null,
            int offsetIndex = 0)
        {
            if (_scope == null) return null;

            if (scriptword?.Definition == null)
            {
                FindScriptwordDefinition(scriptword);
            }
            if (scriptword?.Definition == null) return null;

            string resultString = "<Evaluation_Error />";
            if (scriptword.Definition.RuntimeFunction != null)
            {
                try
                {
                    var variable = new BdoScriptwordFunctionVariable(_scope, scriptVariableSet, scriptword);
                    resultString = scriptword.Definition.RuntimeFunction(variable);
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
                            "(Name='" + scriptword.Name + "';BusinessLibraryName='" + scriptword.Definition.Dto?.LibraryId + "')" +
                            ". Position " + (offsetIndex) + ".",
                            "SCRIPT_EVALUATION"
                            );
                        logEvent.Detail = ElementFactory.CreateSet(
                            ElementFactory.CreateScalar("Position", offsetIndex.ToString()));
                    }
                }
            }

            return resultString;
        }

        /// <summary>
        /// Determines whether the specified script word corresponds to the specified definition.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <param name="scriptwordDefinition">The script word definition to consider.</param>
        /// <returns></returns>
        public bool IsWordMatching(IBdoScriptword scriptword, IBdoScriptwordDefinition scriptwordDefinition)
        {
            if (scriptwordDefinition == null || scriptwordDefinition.Dto == null) return false;

            var definitionDto = scriptwordDefinition.Dto;

            // we check the number of parameters
            if ((!definitionDto.IsRepeatedParameters) && ((scriptword.Parameters == null) && (definitionDto.ParameterSpecification != null) |
                (definitionDto.ParameterSpecification == null) && (scriptword.Parameters != null)))
            {
                return false;
            }

            if ((scriptword.Parameters == null) && ((!definitionDto.IsRepeatedParameters) || (definitionDto.ParameterSpecification == null)))
                return true;
            if ((!definitionDto.IsRepeatedParameters) && (scriptword.Parameters?.Count != definitionDto.ParameterSpecification?.Count))
                return false;

            if ((!definitionDto.IsRepeatedParameters) && ((definitionDto.MaxParameterNumber != -1) && (scriptword.Parameters?.Count > definitionDto.MaxParameterNumber)))
                return false;
            if ((!definitionDto.IsRepeatedParameters) && ((definitionDto.MinParameterNumber != -1) && (scriptword.Parameters?.Count < definitionDto.MinParameterNumber)))
                return false;

            // we search the defined script word parameters

            if (definitionDto?.ParameterSpecification?.Items != null)
            {
                var index = 0;
                foreach (var param in scriptword.Parameters)
                {
                    var paramSpecification = definitionDto?.ParameterSpecification.Items[index];

                    // we check that the value type of the current script word parameter corresponds to the defined one (considering the en-US culture info)

                    if (definitionDto.IsRepeatedParameters && definitionDto.RepeatedParameterValueType != DataValueTypes.Any)
                    {
                        return definitionDto.RepeatedParameterValueType.IsCompatibleWith(param.GetValueType());
                    }
                    else if (paramSpecification.ValueType != DataValueTypes.Any)
                    {
                        return !paramSpecification.IsCompatibleWithItem(param);
                    }
                    index++;
                }
            }

            return true;
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

            base.Dispose(isDisposing);
        }

        #endregion
    }
}