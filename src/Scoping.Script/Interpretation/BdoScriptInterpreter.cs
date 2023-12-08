using AutoMapper.Execution;
using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Data.Meta.Reflection;
using BindOpen.Kernel.Logging;
using System;

namespace BindOpen.Kernel.Scoping.Script
{
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
            switch (exp?.Kind)
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
                                subScript = Evaluate(
                                    subScript.ToExpression(BdoExpressionKind.Script),
                                    varSet,
                                    log)?.ToString();

                                resultScript = resultScript[..scriptwordBeginIndex]
                                    + subScript
                                    + resultScript[(index + 2)..^0];
                                scriptwordBeginIndex += (subScript?.Length ?? 0) + 1;
                            }
                            else
                            {
                                scriptwordBeginIndex = index;
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

                        // we get the next function or variable
                        var childLog = log?.NewLog();
                        var result = Evaluate(script, ref index, 0, varSet, childLog);

                        if (childLog?.HasEvent(EventKinds.Error, EventKinds.Exception) == true)
                        {
                            log?.AddChild(
                                childLog,
                                title: "Error occured while interpreting script",
                                description: "Error while interpreting the script '" + script + "'. " +
                                    "The result is '" + result?.ToString() + "'.",
                                resultCode: "SCRIPTINTERPRETATIONERROR");
                        }

                        return result;
                    }
                    break;
                case BdoExpressionKind.Word:
                    return Evaluate(exp?.Word, varSet, log);
            }

            return exp?.Text;
        }

        public object Evaluate(
            IBdoScriptword word,
            IBdoMetaSet varSet = null,
            IBdoLog log = null,
            bool root = true)
            => Evaluate(word, varSet, log, root, true, null);

        private object Evaluate(
            IBdoScriptword word,
            IBdoMetaSet varSet,
            IBdoLog log,
            bool root,
            bool child,
            IBdoScriptword parent)
        {
            if (root)
            {
                word = word?.Root() as IBdoScriptword;
            }

            if (word != null)
            {
                var cloned = BdoScript.NewWord(word.Kind, word.Name)
                    .WithDataType(BdoExtensionKinds.Scriptword, word.DataType?.DefinitionUniqueName)
                    .WithParent(parent ?? word.Parent as IBdoScriptword);

                switch (word.Kind)
                {
                    case ScriptItemKinds.Function:
                        if (word.Count > 0)
                        {
                            foreach (var paramValue in word)
                            {
                                if (paramValue is IBdoScriptword scriptwordParam)
                                {
                                    var expParam = BdoData.NewExp(scriptwordParam);
                                    var obj = Evaluate(expParam, varSet, log);

                                    cloned.InsertData(obj);
                                }
                                else
                                {
                                    cloned.InsertData(paramValue);
                                }
                            }
                        }
                        break;
                }

                var data = EvaluateScriptword(cloned, varSet, log);
                cloned.SetData(data);

                if (child && word.Child != null)
                {
                    return Evaluate(word.Child, varSet, log, false, child, cloned);
                }

                return data;
            }

            return null;
        }

        public object Evaluate(
            IBdoReference reference,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            switch (reference?.Kind)
            {
                case BdoReferenceKind.Expression:
                    return Evaluate(reference?.Expression, varSet, log);
                case BdoReferenceKind.Identifier:
                    return varSet?[reference?.Identifier];
                case BdoReferenceKind.MetaData:
                    return reference?.MetaData.GetData(_scope, varSet, log);
            }

            return null;
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
                        var obj = EvaluateScriptword(scriptword, varSet, log);

                        // we replace the script word by its value
                        return obj;
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
        /// <param key="log">The BindOpen log used for tracking.</param>
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
            ScriptItemKinds scriptItemKind = ScriptItemKinds.None;
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

                scriptItemKind = ScriptItemKinds.Variable;
            }
            // else if the next word is a sub function
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
                        nextIndex = script.IndexOfScript("(", index);
                        if (nextIndex >= script.Length)
                        {
                            log?.AddEvent(EventKinds.Error,
                                "Syntax Error: Required character '(' for functions missing. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_SYNTAXERROR")
                                .WithDetail(
                                    BdoData.NewScalar("Position", (index + offsetIndex).ToString()));

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
                            nextIndex = script.IndexOfScript(",", nextIndex + 1);

                            // if the next index is out of range
                            if (nextIndex >= script.Length)
                            {
                                log?.AddEvent(EventKinds.Error,
                                    "Syntax Error: Character ')' not found for function. Position " + (index + offsetIndex),
                                    resultCode: "SCRIPT_SYNTAXERROR")
                                    .WithDetail(
                                        BdoData.NewScalar("Position", (index + offsetIndex).ToString()));

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
                    case ScriptItemKinds.Variable:
                        // we look for the next ")" character.
                        nextIndex = script.IndexOfScript(")", index);
                        if (nextIndex >= script.Length)
                        {
                            log?.AddEvent(EventKinds.Error,
                                "Syntax Error: Character ')' needed for function has not been found. Position " + (index + offsetIndex),
                                resultCode: "SCRIPT_SYNTAXERROR")
                                .WithDetail(
                                    BdoData.NewScalar("Position", (index + offsetIndex).ToString()));

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
                        scriptword = BdoScript.NewWord(ScriptItemKinds.Variable, varName);
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

        // Returns the result of the script word scriptword with the specified parameter values
        private object EvaluateScriptword(
            IBdoScriptword scriptword,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (_scope == null || scriptword == null) return null;

            switch (scriptword.Kind)
            {
                case ScriptItemKinds.Function:
                    return _scope.CallFunction(scriptword, varSet, log);
                case ScriptItemKinds.Variable:
                    if (scriptword.Parent == null)
                    {
                        return varSet?.GetData(scriptword?.Name, _scope, varSet, log);
                    }
                    else
                    {
                        var obj = scriptword.Parent.GetData(_scope, varSet, log);

                        return obj?.GetPropertyValue(scriptword?.Name, typeof(BdoPropertyAttribute));
                    }
            }

            return null;
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