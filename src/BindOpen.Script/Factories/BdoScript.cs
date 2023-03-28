using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Functions;
using BindOpen.Logging;
using BindOpen.Scopes;

namespace BindOpen.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        // Interpreters -------------------------

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param key="definitions">The definitions to consider.</param>
        public static BdoScriptInterpreter CreateInterpreter(
            params IBdoFunctionDefinition[] definitions)
        {
            return new BdoScriptInterpreter(definitions);
        }

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        public static BdoScriptInterpreter CreateInterpreter(
            this IBdoScope scope)
        {
            return new BdoScriptInterpreter(scope);
        }

        // Word

        public static BdoScriptword NewWord(
            ScriptItemKinds kind,
            string name = null)
            => new BdoScriptword()
                .WithName(name)
                .WithKind(kind);

        public static BdoScriptword NewWordFromScript(
            string script,
            IBdoLog log = null)
        {
            var interpreter = CreateInterpreter();
            var word = interpreter.FindNextWord(script, log) as BdoScriptword;
            return word;
        }

        // Variable

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public static BdoScriptword Variable(string name)
        {
            var scriptword = NewWord(ScriptItemKinds.Variable);
            scriptword.WithName(name);

            return scriptword;
        }

        public static BdoScriptword Var(string name)
            => Variable(name);

        // Function

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="parameters"></param>
        /// <returns></returns>
        public static BdoScriptword Function(
            string name,
            params object[] parameters)
        {
            var scriptword = NewWord(ScriptItemKinds.Function);
            scriptword.WithName(name);

            var index = 0;
            foreach (var param in parameters)
            {
                scriptword.InsertData(param);
                index++;
            }

            return scriptword;
        }

        public static BdoScriptword Func(
            string name,
            params object[] parameters)
            => Function(name, parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="word"></param>
        /// <param key="name"></param>
        /// <param key="parameters"></param>
        /// <returns></returns>
        public static BdoScriptword Function(
            this BdoScriptword word,
            string name,
            params object[] parameters)
        {
            if (word != null)
            {
                var subWord = Function(name, parameters);
                word.WithChild(subWord);
            }

            return word;
        }

        public static BdoScriptword Func(
            this BdoScriptword word,
            string name,
            params object[] parameters)
            => word.Function(name, parameters);

    }
}
