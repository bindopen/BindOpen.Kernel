using BindOpen.Data;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        // Static creators -------------------------

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="definitions">The definitions to consider.</param>
        public static BdoScriptInterpreter CreateInterpreter(
            params IBdoScriptwordDefinition[] definitions)
        {
            return new BdoScriptInterpreter(definitions);
        }

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        public static BdoScriptInterpreter CreateInterpreter(
            this IBdoScope scope)
        {
            return new BdoScriptInterpreter(scope);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static BdoScriptword CreateWord(
            string script,
            IBdoLog log = null)
        {
            var interpreter = CreateInterpreter();
            return interpreter.FindNextScriptword(script, log) as BdoScriptword;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static BdoScriptword Variable(string name)
        {
            var scriptword = new BdoScriptword(ScriptItemKinds.Variable);
            scriptword.WithName(name);

            return scriptword;
        }

        public static BdoScriptword Var(string name)
            => Variable(name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static BdoScriptword Function(
            string name,
            params object[] parameters)
        {
            var scriptword = new BdoScriptword(ScriptItemKinds.Function);
            scriptword.WithName(name);

            var index = 0;
            foreach (var param in parameters)
            {
                scriptword.AddParameter(param);
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
        /// <param name="word"></param>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static BdoScriptword Function(
            this BdoScriptword word,
            string name,
            params object[] parameters)
        {
            if (word != null)
            {
                var subWord = Function(name, parameters);
                word.WithSubScriptword(subWord);
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
