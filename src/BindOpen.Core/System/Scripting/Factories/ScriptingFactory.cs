using BindOpen.Extensions.Runtime;

namespace BindOpen.System.Scripting
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ScriptingFactory
    {
        // Static creators -------------------------

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IScriptVariableSet CreateVariableSet(
            params (string name, object value)[] items)
        {
            var variableSet = new ScriptVariableSet();
            foreach (var (name, value) in items)
            {
                variableSet.SetValue(name, value);
            }

            return variableSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static BdoScriptword Function(string name, params object[] parameters)
        {
            var scriptword = new BdoScriptword
            {
                Name = name,
                Kind = ScriptItemKinds.Function
            };
            var index = 0;
            foreach (var param in parameters)
            {
                scriptword.AddParameter(param);
                index++;
            }

            return scriptword;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static BdoScriptword Variable(string name)
        {
            var scriptword = new BdoScriptword
            {
                Name = name,
                Kind = ScriptItemKinds.Variable
            };
            return scriptword;
        }
    }
}
