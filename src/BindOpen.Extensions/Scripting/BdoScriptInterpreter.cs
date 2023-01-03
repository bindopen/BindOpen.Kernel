using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a script interpreter. A script interpreter allows to interpret a script
    /// using script dictionnaries and data context, providing a log for the interpretation task.
    /// </summary>
    public class BdoScriptInterpreter : BdoItem, IBdoScriptInterpreter
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly IBdoScope _scope = null;

        private Dictionary<string, IBdoScriptwordDefinition> _definitions = null;

        private Dictionary<string, IBdoScriptwordDefinition> Definitions => _scope?.ExtensionStore?.GetItemDefinitions<IBdoScriptwordDefinition>() ?? _definitions;

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
        /// <param name="definitions">The definitions to consider.</param>
        public BdoScriptInterpreter(params IBdoScriptwordDefinition[] definitions)
        {
            _definitions = new Dictionary<string, IBdoScriptwordDefinition>();
            foreach (var definition in definitions)
            {
                if (definition != null)
                {
                    Definitions.Add(definition.Name, definition);
                }
            }
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

            if (Definitions != null && !string.IsNullOrEmpty(name))
            {
                foreach (var def in Definitions.Where(p => p.Value?.Name.BdoKeyEquals(name) == true).Select(p => p.Value))
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

            if (Definitions != null && !string.IsNullOrEmpty(name))
            {
                foreach (var def in Definitions.Where(p => p.Value?.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) > 0).Select(p => p.Value))
                {
                    matchingDefinitions.Add(def.UniqueId, def);
                }
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
        /// <param name="varElementSet">The variable element set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Literal or script value according to the specified default mode.</returns>
        public object Evaluate(
            IBdoExpression expression,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            int index;
            int scriptwordBeginIndex;

            var script = expression?.Text ?? string.Empty;
            switch (expression?.Kind)
            {
                case BdoExpressionKind.Auto:
                    if (!string.IsNullOrEmpty(script))
                    {
                        var resultScript = script;

                        scriptwordBeginIndex = script.IndexOf("{{");
                        while (scriptwordBeginIndex > -1)
                        {
                            index = script.IndexOfNextString("}}", scriptwordBeginIndex + 1);

                            if ((scriptwordBeginIndex > -1) && (index > -1))
                            {
                                var subScript = script[2..][0..^2];
                                subScript = Evaluate(
                                    subScript.AsExpression(BdoExpressionKind.Script),
                                    varElementSet,
                                    log)?.ToString();

                                resultScript = resultScript.Replace(
                                    resultScript.ToSubstring(scriptwordBeginIndex, index + 1), subScript);
                                scriptwordBeginIndex = script.IndexOf("{{", index + 1);
                            }
                        }

                        return resultScript;
                    }
                    break;
                case BdoExpressionKind.Script:
                    if (!string.IsNullOrEmpty(script))
                    {
                        // we parse the text to interpretate
                        index = 0;
                        scriptwordBeginIndex = index;

                        // we get the next function or variable
                        var subLog = log?.NewLog();
                        var result = Evaluate(script, ref index, 0, varElementSet, subLog);

                        if (subLog?.HasEvent(EventKinds.Error, EventKinds.Exception) == true)
                        {
                            log?.AddError(
                                title: "Error occured while interpreting script",
                                description: "Error while interpreting the script '" + script + "'. " +
                                    "The result is '" + result?.ToString() + "'.",
                                resultCode: "SCRIPTINTERPRETATIONERROR",
                                childLog: subLog
                                );
                        }

                        return result;
                    }
                    break;
                case BdoExpressionKind.Word:
                    if (expression.Word != null)
                    {
                        var cloned = new BdoScriptword(expression.Word.Kind)
                            .WithName(expression.Word.Definition?.Name)
                            .WithDefinition(expression.Word.Definition);

                        switch (expression.Word.Kind)
                        {
                            case ScriptItemKinds.Function:
                                if (expression.Word.Parameters?.Count > 0)
                                {
                                    cloned.WithParameters(new List<object>());
                                    foreach (var paramValue in expression.Word.Parameters)
                                    {
                                        if (paramValue is IBdoScriptword scriptwordParam)
                                        {
                                            var paramObject = Evaluate(
                                                scriptwordParam.AsExpression(),
                                                varElementSet,
                                                log);
                                            cloned.Parameters.Add(paramObject);
                                        }
                                        else if (paramValue is IBdoExpression expressionParam)
                                        {
                                            var paramObject = Evaluate(expressionParam, varElementSet, log);
                                            cloned.Parameters.Add(paramObject);
                                        }
                                        else
                                        {
                                            cloned.Parameters.Add(paramValue);
                                        }
                                    }
                                }
                                break;
                        }

                        return EvaluateScriptword(cloned, varElementSet, log);
                    }
                    break;
            }

            return expression?.Text;
        }

        /// <summary>
        /// Evaluates the specified data expression.
        /// </summary>
        /// <param name="expression">The data expression to consider.</param>
        /// <param name="varElementSet">The variable element set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Literal or script value according to the specified default mode.</returns>
        public T Evaluate<T>(
            IBdoExpression expression,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            return (T)Evaluate(expression, varElementSet, log);
        }

        // String

        /// <summary>
        /// Evaluates the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="expressionKind">The expression kind to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public object Evaluate(
            string script,
            BdoExpressionKind expressionKind = BdoExpressionKind.Auto,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            return Evaluate(
                script.AsExpression(expressionKind),
                varElementSet,
                log);
        }

        /// <summary>
        /// Evaluates the specified script using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="expressionKind">The expression kind to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public T Evaluate<T>(
            string script,
            BdoExpressionKind expressionKind = BdoExpressionKind.Auto,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            return (T)Evaluate(
                script.AsExpression(expressionKind),
                varElementSet,
                log);
        }

        // Script word

        /// <summary>
        /// Evaluates the specified script word using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public object Evaluate(
            IBdoScriptword scriptword,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            return Evaluate(scriptword.AsExpression(), varElementSet, log);
        }

        /// <summary>
        /// Evaluates the specified script word using the defined script words of this instance
        /// of the specified libraries.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the interpretation task.</returns>
        public T Evaluate<T>(
            IBdoScriptword scriptword,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            return (T)Evaluate(scriptword.AsExpression(), varElementSet, log);
        }

        #endregion

        // ------------------------------------------
        // PRIVATE METHODS
        // ------------------------------------------

        #region Private_Methods

        // Returns the standard-interpretated text
        private object Evaluate(
            string script,
            ref int index,
            int offsetIndex,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            if (!string.IsNullOrEmpty(script))
            {
                // we parse the text to interpretate
                index = 0;
                while (index < script.Length)
                {
                    // we get the next function or variable
                    var scriptword = FindNextScriptword(
                        script,
                        null,
                        ref index,
                        offsetIndex,
                        varElementSet,
                        log);

                    // we increment the index
                    index++;

                    // if the script word has been found
                    if (scriptword != null)
                    {
                        var obj = EvaluateScriptword(
                            scriptword, varElementSet, log, index + offsetIndex);

                        // we replace the script word by its value
                        return scriptword.WithItem(obj);
                    }
                    else
                    {
                        script = script?.Trim();
                        var textValue = script.ToUnquoted();
                        if (script != textValue)
                        {
                            // that means that is a string value
                            return textValue;
                        }
                        var value = script.ToObject();

                        return value is string ? null : value;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public IBdoScriptword FindNextScriptword(
            string script,
            IBdoLog log = null)
        {
            var index = 0;
            var offsetIndex = 0;

            return FindNextScriptword(
                script,
                null,
                ref index,
                offsetIndex,
                log: log);
        }

        /// <summary>
        /// Finds the next script words at the specified index.
        /// </summary>
        /// <param name="script">The script to consider.</param>
        /// <param name="parentScriptword">The parent script word to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <param name="offsetIndex">The offset index to consider.</param>
        /// <param name="varElementSet">The variable element set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public IBdoScriptword FindNextScriptword(
            string script,
            IBdoScriptword parentScriptword,
            ref int index,
            int offsetIndex,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            if (parentScriptword != null && parentScriptword.Definition == null)
            {
                log?.AddError(
                    title: "Syntax error: Function named '" + parentScriptword.Definition?.Name + "' not defined. Position " + (index + offsetIndex),
                    resultCode: "SCRIPT_NOTEXISTINGWORD")
                        .WithDetail(
                            BdoElements.NewScalar("Position", (index + offsetIndex).ToString()));

                return null;
            }

            if (string.IsNullOrEmpty(script)) return null;

            IBdoScriptword scriptword = null;

            // we retrieve the type of the next script word
            ScriptItemKinds scriptItemKind = ScriptItemKinds.None;
            int nextIndex = -1;

            script = script.Trim();
            // if the script word begins and ends with a quote then it is a constant
            if (script.StartsWith("'") && script.Trim().EndsWith("'"))
            {
                index = script.Length;
            }
            // else if the next word is a variable
            else if (parentScriptword != null && script.ToSubstring(index, index) == ".")
            {
                index++;
                scriptItemKind = ScriptItemKinds.Function;
            }
            else
            {
                int nextFuncIndex = script.IndexOf(BdoScriptHelper.Symbol_Fun, index);

                // if a potential function or variable was found
                if (nextFuncIndex > -1)
                {
                    if (nextFuncIndex < script.Length - 1 &&
                        script.Substring(nextFuncIndex, 2) == BdoScriptHelper.Symbol_Var)
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
                switch (scriptItemKind)
                {
                    case ScriptItemKinds.Function:
                        // we look for the next "(" character.
                        nextIndex = script.IndexOfFromScript("(", index);
                        if (nextIndex >= script.Length)
                        {
                            log?.AddError(
                                title: "Syntax Error: Required character '(' for functions missing. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_SYNTAXERROR")
                                .WithDetail(
                                    BdoElements.NewScalar("Position", (index + offsetIndex).ToString()));

                            return null;
                        }

                        // we instantiate the script word
                        scriptword = new BdoScriptword(ScriptItemKinds.Function)
                            .WithName(script.ToSubstring(index, nextIndex - 1).Trim());
                        index = nextIndex;

                        // we retrieve the function parameters
                        int scriptwordParameterCount = 0;
                        while ((nextIndex < script.Length) && (script.Substring(nextIndex, 1) != ")"))
                        {
                            nextIndex = script.IndexOfFromScript(",", nextIndex + 1);

                            // if the next index is out of range
                            if (nextIndex >= script.Length)
                            {
                                log?.AddError(
                                    title: "Syntax Error: Character ')' not found for function. Position " + (index + offsetIndex),
                                    resultCode: "SCRIPT_SYNTAXERROR")
                                    .WithDetail(
                                        BdoElements.NewScalar("Position", (index + offsetIndex).ToString()));

                                return null;
                            }
                            else
                            {
                                // else we get the parameter value and we interprete it
                                string subScript = script.Substring(index + 1, nextIndex - index - 1).Trim();
                                if (!string.IsNullOrEmpty(subScript))
                                {
                                    int subIndex = 0;
                                    object parameterValue = Evaluate(
                                        subScript,
                                        ref subIndex,
                                        offsetIndex + index + 1,
                                        varElementSet,
                                        log);

                                    scriptword.Parameters.Add(parameterValue);
                                    scriptwordParameterCount++;
                                }
                                index = nextIndex;
                            }
                        }
                        break;
                    case ScriptItemKinds.Variable:
                        // we look for the next ")" character.
                        nextIndex = script.IndexOfFromScript(")", index + 2);
                        if (nextIndex >= script.Length)
                        {
                            log?.AddError(
                                title: "Syntax Error: Character ')' needed for function has not been found. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_SYNTAXERROR")
                                .WithDetail(
                                    BdoElements.NewScalar("Position", (index + offsetIndex).ToString()));

                            return null;
                        }

                        // we instantiate the script word
                        scriptword = new BdoScriptword(ScriptItemKinds.Variable)
                            .WithName(script.ToSubstring(index, nextIndex - 1).Trim());
                        index = nextIndex;
                        break;
                }

                if (scriptword != null)
                {
                    scriptword.WithParent(parentScriptword);

                    if (PopulateScriptwordDefinition(scriptword, log, index, offsetIndex))
                    {
                        // if the script word is a variable then we retrieve the sub script word
                        if (script.ToSubstring(nextIndex + 1, nextIndex + 1) == ".")
                        {
                            var obj = EvaluateScriptword(scriptword, varElementSet, log, index);

                            // we evaluate the variable value
                            scriptword.WithItem(obj);

                            nextIndex++;
                            scriptword = FindNextScriptword(
                                script,
                                scriptword,
                                ref nextIndex,
                                offsetIndex,
                                varElementSet,
                                log);
                            if (scriptword != null) index = nextIndex;
                        }
                    }
                }
            }

            return scriptword;
        }

        private bool PopulateScriptwordDefinition(
            IBdoScriptword scriptword,
            IBdoLog log = null,
            int index = 0,
            int offsetIndex = 0)
        {
            if (scriptword != null)
            {
                // we try to find the corresponding defined function
                var scriptwordDefinitions = GetDefinitionsWithExactName(scriptword.Definition?.Name, scriptword.Parent?.Definition);

                if (scriptwordDefinitions.Count == 0)
                {
                    log?.AddError(
                        title: "Function named '" + scriptword.Definition?.Name + "' not defined",
                        description: "Syntax error: Function named '" + scriptword.Definition?.Name + "' not defined" +
                            (scriptword.Parent == null ? string.Empty : " for parent function '" + scriptword.Parent?.Definition?.Name + "'") +
                            ". Position " + (index + offsetIndex),
                        resultCode: "SCRIPT_NOTEXISTINGWORD")
                        .WithDetail(
                            BdoElements.NewScalar("Position", (index + offsetIndex).ToString()));
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
                        log?.AddError(
                            title: "Invalid arguments: Function called '" + scriptword.Definition?.Name + "' has invalid parameters. Either the number of parameters does not match or their value types. Position " + (index + offsetIndex),
                            resultCode: "SCRIPT_INVALIDARGUMENT"
                            )
                            .WithDetail(
                                BdoElements.NewScalar("Position", (index + offsetIndex).ToString()));
                    }
                    else
                    {
                        // else we affect the correct method name and is unlimited properties
                        if (scriptwordDefinition.Kind == ScriptItemKinds.Function
                            && scriptwordDefinition.RuntimeScopedFunction == null)
                        {
                            log?.AddError(
                                title: "Invalid definition: Method not defined for function called '" + scriptword.Definition?.Name + "'. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_DEFINITION")
                                .WithDetail(
                                    BdoElements.NewScalar("Position", (index + offsetIndex).ToString()));
                        }
                        scriptword.WithDefinition(scriptwordDefinition);

                        return true;
                    }
                }
            }

            return false;
        }

        // Returns the result of the script word scriptword with the specified parameter values
        private object EvaluateScriptword(
            IBdoScriptword scriptword,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null,
            int offsetIndex = 0)
        {
            if (_scope == null) return null;

            if (scriptword?.Definition == null)
            {
                PopulateScriptwordDefinition(scriptword, log);
            }
            if (scriptword?.Definition == null) return null;

            switch (scriptword.Kind)
            {
                case ScriptItemKinds.Function:
                    try
                    {
                        if (scriptword.Definition.RuntimeScopedFunction != null)
                        {
                            var variable = new BdoScriptwordDomain(_scope, varElementSet, scriptword);
                            return scriptword.Definition.RuntimeScopedFunction(variable);
                        }
                        else if (scriptword.Definition.RuntimeBasicFunction != null)
                        {
                            return scriptword.Definition.RuntimeBasicFunction();
                        }
                    }
                    catch (Exception ex)
                    {
                        log?.AddError(
                            "Raised the following exception: " + ex.ToString(),
                            Criticalities.High,
                            string.Empty,
                            "Evaluation Error: Error when tempting to evaluate function " +
                            "(Name='" + scriptword.Definition?.Name + "';BusinessLibraryName='" + scriptword.Definition?.LibraryId + "')" +
                            ". Position " + (offsetIndex) + ".",
                            "SCRIPT_EVALUATION"
                            )
                            .WithDetail(
                                BdoElements.NewScalar("Position", offsetIndex.ToString()));
                    }
                    break;
                case ScriptItemKinds.Variable:
                    var name = scriptword.Parameters?.FirstOrDefault()?.ToString();
                    return varElementSet[name].GetItem(_scope, varElementSet, log);
            }

            return null;
        }

        /// <summary>
        /// Determines whether the specified script word corresponds to the specified definition.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <param name="scriptwordDefinition">The script word definition to consider.</param>
        /// <returns></returns>
        public bool IsWordMatching(IBdoScriptword scriptword, IBdoScriptwordDefinition definition)
        {
            if (definition == null) return false;

            // we check the number of parameters
            if ((!definition.IsRepeatedParameters) && ((scriptword.Parameters == null) && (definition.ParameterSpecification != null) |
                (definition.ParameterSpecification == null) && (scriptword.Parameters != null)))
            {
                return false;
            }

            if ((scriptword.Parameters == null) && ((!definition.IsRepeatedParameters) || (definition.ParameterSpecification == null)))
                return true;
            if ((!definition.IsRepeatedParameters) && (scriptword.Parameters?.Count != definition.ParameterSpecification?.Count))
                return false;

            if ((!definition.IsRepeatedParameters) && ((definition.MaxParameterNumber != -1) && (scriptword.Parameters?.Count > definition.MaxParameterNumber)))
                return false;
            if ((!definition.IsRepeatedParameters) && ((definition.MinParameterNumber != -1) && (scriptword.Parameters?.Count < definition.MinParameterNumber)))
                return false;

            // we search the defined script word parameters

            if (definition?.ParameterSpecification?.Items != null)
            {
                var index = 0;
                foreach (var param in scriptword.Parameters)
                {
                    var paramSpecification = definition?.ParameterSpecification.Items[index];

                    // we check that the value type of the current script word parameter corresponds to the defined one (considering the en-US culture info)

                    if (definition.IsRepeatedParameters && definition.RepeatedParameterValueType != DataValueTypes.Any)
                    {
                        return definition.RepeatedParameterValueType.IsCompatibleWith(param.GetValueType());
                    }
                    else if (paramSpecification.ValueType != DataValueTypes.Any)
                    {
                        return paramSpecification.IsCompatibleWithItem(param);
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