using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Functions;
using BindOpen.Logging;
using BindOpen.Scopes.Scopes;
using System;
using System.Linq;

namespace BindOpen.Script
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

        private readonly IBdoScope _scope;

        private readonly ITBdoSet<IBdoFunctionDefinition> _definitions;

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
        /// <param key="definitions">The definitions to consider.</param>
        public BdoScriptInterpreter(params IBdoFunctionDefinition[] definitions)
        {
            _definitions = new TBdoSet<IBdoFunctionDefinition>();
            foreach (var definition in definitions)
            {
                if (definition != null)
                {
                    _definitions.Add(definition);
                }
            }
        }

        /// <summary>
        /// Instantiates a new instance of the BdoScriptInterpreter class.
        /// </summary>
        /// <param key="scope">The application scope used to interprete.</param>
        public BdoScriptInterpreter(IBdoScope scope)
        {
            _scope = scope;
            _definitions = _scope?.ExtensionStore?.GetDefinitions<IBdoFunctionDefinition>();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the word definitions with the specified name.
        /// </summary>
        /// <param key="name">The name of the script words to return.</param>
        /// <param key="parentDefinition">The parent definition.</param>
        /// <returns>The script words with the specified name.</returns>
        public ITBdoSet<IBdoFunctionDefinition> GetDefinitionsWithName(
            string name,
            bool isExact = false)
        {
            var definitions = new TBdoSet<IBdoFunctionDefinition>();

            if (_definitions != null && !string.IsNullOrEmpty(name))
            {
                if (isExact)
                    definitions = BdoData.NewSet(_definitions.Where(p => p?.Name.BdoKeyEquals(name) == true).ToArray());
                else
                    definitions = BdoData.NewSet(_definitions.Where(p => p?.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) > 0).ToArray());
            }

            return _definitions;
        }

        /// <summary>
        /// Returns the word definitions with the specified name.
        /// </summary>
        /// <param key="name">The name of the script words to return.</param>
        /// <param key="parentDefinition">The parent definition.</param>
        /// <returns>The script words with the specified name.</returns>
        public ITBdoSet<IBdoFunctionDefinition> GetDefinitions()
            => _definitions;

        #endregion

        // ------------------------------------------
        // EVALUATION
        // ------------------------------------------

        #region Evaluation

        // Expression

        /// <summary>
        /// Evaluates the specified data reference.
        /// </summary>
        /// <param key="exp">The data exp to consider.</param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns>Literal or script value according to the specified default mode.</returns>
        public object Evaluate(
            IBdoExpression exp,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            int index;
            int scriptwordBeginIndex;

            var script = exp?.Text ?? string.Empty;
            switch (exp?.Kind)
            {
                case BdoExpressionKind.Auto:
                    if (!string.IsNullOrEmpty(script))
                    {
                        var resultScript = script;

                        scriptwordBeginIndex = script.IndexOf("{{");
                        while (scriptwordBeginIndex > -1)
                        {
                            index = script.IndexOfNextString("}}", scriptwordBeginIndex + 1);

                            if (scriptwordBeginIndex > -1 && index > -1)
                            {
                                var subScript = script[2..][0..^2];
                                subScript = Evaluate(
                                    subScript.ToExpression(BdoExpressionKind.Script),
                                    varSet,
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
                        var result = Evaluate(script, ref index, 0, varSet, subLog);

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
            }

            return exp?.Text;
        }

        public object Evaluate(
            IBdoReference reference,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            switch (reference?.Kind)
            {
                case BdoExpressionKind.Any:
                    if (reference.Word == null)
                        return null;
                    else
                    {
                        var cloned = BdoScript.NewWord(reference.Word.Kind, reference.Word.Name)
                            .WithDefinitionUniqueName(reference.Word.DefinitionUniqueName);

                        switch (reference.Word.Kind)
                        {
                            case ScriptItemKinds.Function:
                                if (reference.Word.Count > 0)
                                {
                                    foreach (var paramValue in reference.Word)
                                    {
                                        IBdoExpression expParam = null;
                                        if (paramValue is IBdoScriptword scriptwordParam)
                                        {
                                            expParam = scriptwordParam.ToReference();
                                        }

                                        if (expParam != default)
                                        {
                                            var paramObject = Evaluate(expParam, varSet, log);
                                            cloned.InsertData(paramObject);
                                        }
                                        else
                                        {
                                            cloned.InsertData(paramValue);
                                        }
                                    }
                                }
                                break;
                        }

                        return EvaluateScriptword(cloned, varSet, log);
                    }
                default:
                    return Evaluate(reference as IBdoExpression, varSet, log);
            }
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
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (!string.IsNullOrEmpty(script))
            {
                // we parse the text to interpretate
                index = 0;
                while (index < script.Length)
                {
                    // we get the next function or variable
                    var scriptword = FindNextWord(
                        script,
                        null,
                        ref index,
                        offsetIndex,
                        varSet,
                        log);

                    // we increment the index
                    index++;

                    // if the script word has been found
                    if (scriptword != null)
                    {
                        var obj = EvaluateScriptword(
                            scriptword, varSet, log, index + offsetIndex);

                        // we replace the script word by its value
                        return scriptword.WithData(obj);
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
        /// <param key="script"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        public IBdoScriptword FindNextWord(
            string script,
            IBdoLog log = null)
        {
            var index = 0;
            var offsetIndex = 0;

            return FindNextWord(
                script,
                null,
                ref index,
                offsetIndex,
                log: log);
        }

        /// <summary>
        /// Finds the next script words at the specified index.
        /// </summary>
        /// <param key="script">The script to consider.</param>
        /// <param key="parentScriptword">The parent script word to consider.</param>
        /// <param key="index">The index to consider.</param>
        /// <param key="offsetIndex">The offset index to consider.</param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="log"></param>
        /// <returns></returns>
        private IBdoScriptword FindNextWord(
            string script,
            IBdoScriptword parentScriptword,
            ref int index,
            int offsetIndex,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
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
                                    BdoMeta.NewScalar("Position", (index + offsetIndex).ToString()));

                            return null;
                        }

                        // we instantiate the script word
                        scriptword = BdoScript.NewWord(
                            ScriptItemKinds.Function,
                            script.ToSubstring(index, nextIndex - 1).Trim());
                        index = nextIndex;

                        // we retrieve the function parameters
                        int scriptwordParameterCount = 0;
                        while (nextIndex < script.Length && script.Substring(nextIndex, 1) != ")")
                        {
                            nextIndex = script.IndexOfFromScript(",", nextIndex + 1);

                            // if the next index is out of range
                            if (nextIndex >= script.Length)
                            {
                                log?.AddError(
                                    title: "Syntax Error: Character ')' not found for function. Position " + (index + offsetIndex),
                                    resultCode: "SCRIPT_SYNTAXERROR")
                                    .WithDetail(
                                        BdoMeta.NewScalar("Position", (index + offsetIndex).ToString()));

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
                                        varSet,
                                        log);

                                    scriptword.InsertData(parameterValue);
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
                                    BdoMeta.NewScalar("Position", (index + offsetIndex).ToString()));

                            return null;
                        }

                        // we instantiate the script word
                        scriptword = BdoScript.NewWord(
                            ScriptItemKinds.Variable,
                            script.ToSubstring(index, nextIndex - 1).Trim());
                        index = nextIndex;
                        break;
                }

                if (scriptword != null)
                {
                    scriptword.WithParent(parentScriptword);

                    // if the script word is a variable then we retrieve the sub script word
                    if (script.ToSubstring(nextIndex + 1, nextIndex + 1) == ".")
                    {
                        nextIndex++;
                        scriptword = FindNextWord(
                            script,
                            scriptword,
                            ref nextIndex,
                            offsetIndex,
                            varSet,
                            log);
                        if (scriptword != null) index = nextIndex;
                    }
                }
            }

            return scriptword;
        }

        private IBdoFunctionDefinition GetFunctionDefinition(
            IBdoScriptword scriptword,
            IBdoLog log = null,
            int index = 0,
            int offsetIndex = 0)
        {
            if (scriptword != null)
            {
                // we try to find the corresponding defined function
                var functionDefinitions = GetDefinitionsWithName(scriptword?.Name, true);

                if (functionDefinitions?.Any() != true)
                {
                    log?.AddError(
                        title: "Function named '" + scriptword?.Name + "' not defined",
                        description: "Syntax error: Function named '" + scriptword?.Name + "' not defined" +
                            (scriptword.Parent == null ? string.Empty : " for parent function '" + scriptword.Parent?.Name + "'") +
                            ". Position " + (index + offsetIndex),
                        resultCode: "SCRIPT_NOTEXISTINGWORD")
                        .WithDetail(
                            BdoMeta.NewScalar("Position", (index + offsetIndex).ToString()));
                }
                else
                {
                    IBdoFunctionDefinition functionDefinition = null;
                    foreach (var definition in functionDefinitions)
                    {
                        if (IsWordMatchingFunction(scriptword, definition))
                        {
                            functionDefinition = definition;
                            break;
                        }
                    }

                    // if no defined script word match then
                    if (functionDefinition == null)
                    {
                        log?.AddError(
                            title: "Invalid arguments: Function called '" + scriptword?.Name + "' has invalid parameters. Either the number of parameters does not match or their value types. Position " + (index + offsetIndex),
                            resultCode: "SCRIPT_INVALIDARGUMENT"
                            )
                            .WithDetail(
                                BdoMeta.NewScalar("Position", (index + offsetIndex).ToString()));
                    }
                    else
                    {
                        // else we affect the correct method name and is unlimited properties
                        if (functionDefinition.RuntimeBasicFunction == null
                            && functionDefinition.RuntimeScopedFunction == null
                            && functionDefinition.RuntimeFunction == null)
                        {
                            log?.AddError(
                                title: "Invalid definition: Method not defined for function called '" + functionDefinition.Name + "'. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_DEFINITION")
                                .WithDetail(
                                    BdoMeta.NewScalar("Position", (index + offsetIndex).ToString()));
                        }

                        return functionDefinition;
                    }
                }
            }

            return null;
        }

        // Returns the result of the script word scriptword with the specified parameter values
        private object EvaluateScriptword(
            IBdoScriptword scriptword,
            IBdoMetaSet varSet = null,
            IBdoLog log = null,
            int offsetIndex = 0)
        {
            if (_scope == null) return null;

            var definition = GetFunctionDefinition(scriptword);

            if (definition == null) return null;

            switch (scriptword.Kind)
            {
                case ScriptItemKinds.Function:
                    try
                    {
                        if (definition.RuntimeScopedFunction != null)
                        {
                            var domain = new BdoScriptwordDomain(_scope, varSet, scriptword);
                            return definition.RuntimeScopedFunction(domain);
                        }
                        else if (definition.RuntimeBasicFunction != null)
                        {
                            return definition.RuntimeBasicFunction();
                        }
                    }
                    catch (Exception ex)
                    {
                        log?.AddError(
                            "Raised the following exception: " + ex.ToString(),
                            Criticalities.High,
                            string.Empty,
                            "Evaluation Error: Error when tempting to evaluate function " +
                            "(Name='" + definition.Name + "';BusinessLibraryName='" + definition.LibraryId + "')" +
                            ". Position " + offsetIndex + ".",
                            "SCRIPT_EVALUATION"
                            )
                            .WithDetail(
                                BdoMeta.NewScalar("Position", offsetIndex.ToString()));
                    }
                    break;
                case ScriptItemKinds.Variable:
                    var name = scriptword?.FirstOrDefault()?.ToString();
                    return varSet.GetData(name, _scope, varSet, log);
            }

            return null;
        }

        /// <summary>
        /// Determines whether the specified script word corresponds to the specified definition.
        /// </summary>
        /// <param key="scriptword">The script word to consider.</param>
        /// <param key="definition">The script word definition to consider.</param>
        /// <returns></returns>
        public bool IsWordMatchingFunction(IBdoScriptword scriptword, IBdoFunctionDefinition definition)
        {
            if (definition == null) return false;

            return definition?.SpecDetail?.IsCompatibleWith(scriptword) ?? false;
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