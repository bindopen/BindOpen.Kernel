using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Data.Objects;
using BindOpen.Logging;
using System;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class represents a script interpreter. A script interpreter allows to interpret a script
/// using script dictionnaries and data context, providing a log for the interpretation task.
/// </summary>
public class BdoScriptInterpreter : BdoObject, IBdoScriptInterpreter
{
    // ------------------------------------------
    // VARIABLES
    // ------------------------------------------

    #region Variables

    private readonly IBdoScope _scope;

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
    /// <param key="scope">The application scope used to interprete.</param>
    public BdoScriptInterpreter(IBdoScope scope)
    {
        _scope = scope;
    }

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
    /// <param key="metaSet">The variable element set to consider.</param>
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
        switch (exp?.ExpressionKind)
        {
            case BdoExpressionKind.Auto:
                if (!string.IsNullOrEmpty(script))
                {
                    var resultScript = script;

                    scriptwordBeginIndex = resultScript.IndexOfNextString("{{");
                    while (scriptwordBeginIndex > -1 && scriptwordBeginIndex < resultScript.Length)
                    {
                        index = resultScript.IndexOfNextString("}}", scriptwordBeginIndex + 2);

                        if (scriptwordBeginIndex > -1 && index > -1 && index < resultScript.Length)
                        {
                            var subScript = resultScript[(scriptwordBeginIndex + 2)..index];
                            var subResult = Evaluate(
                                subScript.ToExpression(BdoExpressionKind.Script),
                                varSet,
                                log);

                            string replacement = subResult?.ToString() ?? string.Empty;
                            resultScript = resultScript[..scriptwordBeginIndex]
                                + replacement
                                + resultScript[(index + 2)..];

                            scriptwordBeginIndex = resultScript.IndexOfNextString("{{", scriptwordBeginIndex + replacement.Length);
                        }
                        else
                        {
                            scriptwordBeginIndex = -1;
                        }
                    }

                    return resultScript;
                }
                break;
            case BdoExpressionKind.Script:
                if (!string.IsNullOrEmpty(script))
                {
                    index = 0;
                    var childLog = log?.NewLog();
                    var result = Evaluate(script, ref index, 0, varSet, childLog);

                    if (childLog?.HasEvent(BdoEventKinds.Error, BdoEventKinds.Exception) == true)
                    {
                        log?.AddChild(
                            childLog,
                            title: "Error occured while interpreting script",
                            description: $"Error while interpreting the script '{script}'. The result is '{result}'.",
                            resultCode: "SCRIPTINTERPRETATIONERROR");
                    }

                    return result;
                }
                break;
            case BdoExpressionKind.Word:
                return Evaluate(exp as IBdoScriptword, varSet, log);
        }

        return exp?.Text;
    }

    /// <summary>
    /// Evaluates the specified script word.
    /// </summary>
    /// <param name="word">The script word to evaluate.</param>
    /// <param name="varSet">The variable set to consider for resolution.</param>
    /// <param name="log">The log to populate with execution events.</param>
    /// <param name="root">Indicates whether to start evaluation from the root of the word chain.</param>
    /// <returns>Returns the result of the evaluation.</returns>
    public object Evaluate(
        IBdoScriptword word,
        IBdoMetaSet varSet = null,
        IBdoLog log = null,
        bool root = true)
        // Redirects to the private recursive method, 
        // initializing 'child' to true and 'parent' to null.
        => Evaluate(word, varSet, log, root, true, null);

    private object Evaluate(
        IBdoScriptword word,
        IBdoMetaSet varSet,
        IBdoLog log,
        bool root,
        bool child,
        IBdoScriptword parent)
    {
        if (word == null) return null;

        // 1. Navigation Logic
        // Only climb to the root on the very first call. 
        // This prevents infinite recursion when processing children or parameters.
        IBdoScriptword currentWord = root ? (word.Root() as IBdoScriptword) : word;

        if (currentWord != null)
        {
            // 2. Execution Context (Cloning)
            // We create a runtime instance. The 'parent' here is the evaluation 
            // result of the previous word in a fluent chain.
            var cloned = BdoScript.NewWord(currentWord.TokenKind, currentWord.Name)
                .WithDataType(BdoExtensionKinds.Scriptword, currentWord.DataType?.DefinitionUniqueName);

            if (parent != null)
            {
                cloned.WithParent(parent);
            }
            else
            {
                cloned.Parent = currentWord.Parent;
            }

            // 3. Parameter Evaluation
            if (currentWord.TokenKind == ScriptTokenKinds.Function)
            {
                foreach (var paramValue in currentWord)
                {
                    if (paramValue is IBdoScriptword wordParam)
                    {
                        // Evaluate the parameter script. 
                        // root: false because we want to start exactly at this wordParam.
                        // child: true to ensure the entire nested chain is evaluated.
                        var evaluatedParam = Evaluate(
                            wordParam,
                            varSet,
                            log,
                            root: true,
                            child: true,
                            parent: null);

                        cloned.InsertData(evaluatedParam);
                    }
                    else
                    {
                        // Literal values (strings, numbers, etc.)
                        cloned.InsertData(paramValue);
                    }
                }
            }

            // 4. Word Resolution
            // EvaluateScriptword is the actual engine call (e.g., executing the C# method).
            var data = EvaluateScriptword(cloned, varSet, log);
            cloned.SetData(data);

            // 5. Fluent Chain Continuation ($obj.func())
            if (child && currentWord.Child != null)
            {
                // IMPORTANT: We pass 'cloned' as the parent for the next evaluation.
                // This allows the next word to access the current 'data' via $this.
                return Evaluate(
                    currentWord.Child,
                    varSet,
                    log,
                    root: false,
                    child: child,
                    parent: cloned);
            }

            return data;
        }

        return null;
    }

    /// <summary>
    /// Evaluates the specified data reference.
    /// </summary>
    /// <param name="reference">The data reference to evaluate.</param>
    /// <param name="varSet">The variable set to consider for resolution.</param>
    /// <param name="log">The log to populate with execution events.</param>
    /// <returns>Returns the evaluated value based on the reference kind.</returns>
    public object Evaluate(
        IBdoReference reference,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        // Check the kind of reference to determine the resolution strategy
        switch (reference?.Kind)
        {
            // The reference is a script or an auto-expression
            case BdoReferenceKind.Expression:
                return Evaluate(reference?.Expression, varSet, log);

            // The reference is a direct identifier (variable name)
            case BdoReferenceKind.Identifier:
                return varSet?[reference?.Identifier];

            // The reference points to a meta-data object that needs extraction
            case BdoReferenceKind.MetaData:
                return reference?.MetaData?.GetData(_scope, varSet, log);
        }

        return null;
    }

    #endregion

    // ------------------------------------------
    // PRIVATE METHODS
    // ------------------------------------------

    #region Private_Methods

    private object Evaluate(
        string script,
        ref int index,
        int offsetIndex,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        if (!string.IsNullOrEmpty(script))
        {
            var scriptword = FindNextWord(script, null, ref index, offsetIndex, varSet, log);

            if (scriptword != null)
            {
                return Evaluate(scriptword, varSet, log);
            }
            else
            {
                script = script.Trim();
                var textValue = script.ToUnquoted();
                if (script != textValue) return textValue;

                var value = script.ToObject();
                return value is string ? null : value;
            }
        }

        return null;
    }

    /// <summary>
    /// Finds and parses the first script word found in the specified string.
    /// </summary>
    /// <param name="script">The script string to analyze.</param>
    /// <param name="log">The log to populate with any parsing or syntax errors.</param>
    /// <returns>Returns the identified <see cref="IBdoScriptword"/>; otherwise, returns null.</returns>
    public IBdoScriptword FindNextWord(
        string script,
        IBdoLog log = null)
    {
        var index = 0;
        // Initializes the recursive parsing process starting at index 0.
        return FindNextWord(script, null, ref index, 0, null, log);
    }

    /// <summary>
    /// Finds the next script words at the specified index.
    /// </summary>
    /// <param key="script">The script to consider.</param>
    /// <param key="parentScriptword">The parent script word to consider.</param>
    /// <param key="index">The index to consider.</param>
    /// <param key="offsetIndex">The offset index to consider.</param>
    /// <param key="metaSet">The variable element set to consider.</param>
    /// <param key="log">The BindOpen log used for tracking.</param>
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
        ScriptTokenKinds scriptElementKind = ScriptTokenKinds.None;
        int nextIndex = -1;

        script = script.Trim();
        // if the script word begins and ends with a quote then it is a constant
        if (script.StartsWith("'") && script.Trim().EndsWith("'"))
        {
            index = script.Length;
        }
        // else if the next word is a sub variable
        else if (parentScriptword != null && script.ToSubstring(index, index + 1) == ".(")
        {
            index += 2;

            scriptElementKind = ScriptTokenKinds.Variable;
        }
        // else if the next word is a sub function
        else if (parentScriptword != null && script.ToSubstring(index, index) == ".")
        {
            index++;
            scriptElementKind = ScriptTokenKinds.Function;
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
                    scriptElementKind = ScriptTokenKinds.Variable;
                }
                else
                {
                    index = nextFuncIndex + 1;
                    scriptElementKind = ScriptTokenKinds.Function;
                }
            }
            else
            {
                index = script.Length;
            }
        }

        // if a next script word has been found then
        if (scriptElementKind != ScriptTokenKinds.None)
        {
            switch (scriptElementKind)
            {
                case ScriptTokenKinds.Function:
                    // we look for the next "(" character.
                    nextIndex = script.IndexOfScript("(", index);
                    if (nextIndex >= script.Length)
                    {
                        log?.AddEvent(BdoEventKinds.Error,
                            "Syntax Error: Required character '(' for functions missing. Position " + (index + offsetIndex),
                            resultCode: "SCRIPT_SYNTAXERROR")
                            .WithDetail(
                                ("Position", (index + offsetIndex).ToString()));

                        return null;
                    }

                    // we instantiate the script word
                    scriptword = BdoScript.NewWord(
                        ScriptTokenKinds.Function,
                        script.ToSubstring(index, nextIndex - 1).Trim());
                    index = nextIndex;

                    // we retrieve the function parameters
                    int scriptwordParameterCount = 0;
                    while (nextIndex < script.Length && script.Substring(nextIndex, 1) != ")")
                    {
                        nextIndex = script.IndexOfScript(",", nextIndex + 1);

                        // if the next index is out of range
                        if (nextIndex >= script.Length)
                        {
                            log?.AddEvent(BdoEventKinds.Error,
                                "Syntax Error: Character ')' not found for function. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_SYNTAXERROR")
                                .WithDetail(
                                    ("Position", (index + offsetIndex).ToString()));

                            return null;
                        }
                        else
                        {
                            // else we get the parameter value and we interprete it
                            string subScript = script.Substring(index + 1, nextIndex - index - 1).Trim();
                            if (!string.IsNullOrEmpty(subScript))
                            {
                                int subIndex = 0;
                                var word = FindNextWord(
                                    subScript, null,
                                    ref subIndex, offsetIndex + index + 1,
                                    varSet, log);

                                if (word != null)
                                {
                                    var obj = Evaluate(word, varSet, log);
                                    word.WithData(obj);

                                    scriptword.InsertData(word);
                                }
                                else
                                {
                                    if (subScript.IsQuoted())
                                    {
                                        subScript = subScript.ToUnquoted();
                                        scriptword.InsertData(subScript);
                                    }
                                    else
                                    {
                                        var value = subScript.ToObject();
                                        scriptword.InsertData(value);
                                    }
                                }

                                scriptwordParameterCount++;
                            }
                            index = nextIndex;
                        }
                    }
                    break;
                case ScriptTokenKinds.Variable:
                    // we look for the next ")" character.
                    nextIndex = script.IndexOfScript(")", index);
                    if (nextIndex >= script.Length)
                    {
                        log?.AddEvent(BdoEventKinds.Error,
                            "Syntax Error: Character ')' needed for function has not been found. Position " + (index + offsetIndex),
                            resultCode: "SCRIPT_SYNTAXERROR")
                            .WithDetail(
                                ("Position", (index + offsetIndex).ToString()));

                        return null;
                    }

                    var varName = script.ToSubstring(index, nextIndex - 1).Trim();
                    if (varName.Equals("this", StringComparison.OrdinalIgnoreCase))
                    {
                        varName = BdoData.__VarName_This;
                    }
                    else
                    {
                        varName = varName.ToUnquoted();
                    }

                    // we instantiate the script word
                    scriptword = BdoScript.NewWord(ScriptTokenKinds.Variable, varName);
                    index = nextIndex;
                    break;
            }

            if (scriptword != null)
            {
                scriptword.WithParent(parentScriptword);

                // if the script word is a variable then we retrieve the sub script word
                if (script.ToSubstring(nextIndex + 1, nextIndex + 1) == ".")
                {
                    //scriptword.WithParent(parentScriptword);
                    var obj = Evaluate(scriptword, varSet, log, false);
                    scriptword.WithData(obj);

                    //parentScriptword = scriptword;

                    nextIndex++;
                    scriptword = FindNextWord(
                        script,
                        scriptword,
                        ref nextIndex,
                        offsetIndex,
                        varSet,
                        log);

                    //scriptword.WithParent(parentScriptword);

                    if (scriptword != null) index = nextIndex;
                }
            }
        }

        return scriptword;
    }

    private object EvaluateScriptword(
        IBdoScriptword scriptword,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        if (_scope == null || scriptword == null) return null;

        switch (scriptword.TokenKind)
        {
            case ScriptTokenKinds.Function:
                return _scope.CallFunction(scriptword, varSet, log);
            case ScriptTokenKinds.Variable:
                if (scriptword.Parent == null)
                    return varSet?.GetData(scriptword.Name, _scope, varSet, log);

                var parentData = scriptword.Parent.GetData(_scope, varSet, log);
                return parentData?.GetPropertyValue(scriptword.Name, typeof(BdoPropertyAttribute));
        }

        return null;
    }

    #endregion

    #region IDisposable_Methods

    private bool _isDisposed = false;

    /// <summary>
    /// Disposes of the resources used by the interpreter.
    /// </summary>
    /// <param name="isDisposing">True to release both managed and unmanaged resources; 
    /// false to release only unmanaged resources.</param>
    /// <remarks>
    /// This method follows the standard .NET disposal pattern to ensure that the 
    /// associated scope is properly cleaned up and to prevent memory leaks.
    /// </remarks>
    protected override void Dispose(bool isDisposing)
    {
        // Prevent multiple disposals
        if (_isDisposed) return;

        if (isDisposing)
        {
            // Dispose of the managed scope as it may hold references to extensions or external resources
            _scope?.Dispose();
        }

        _isDisposed = true;

        // Call the base class implementation to complete the disposal chain
        base.Dispose(isDisposing);
    }

    #endregion
}