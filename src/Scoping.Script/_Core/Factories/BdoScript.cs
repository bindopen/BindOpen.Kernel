using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using System.Linq;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static T To<T>(this IBdoScriptword word) where T : BdoScriptword, new()
        {
            var toWord = BdoData.New<T>()
                .WithName(word?.Name)
                .WithKind(word?.Kind ?? ScriptItemKinds.None)
                .WithChild(word?.Child)
                .WithParent(word?.Parent as IBdoScriptword);

            foreach (var param in word)
            {
                toWord.Add(param);
            }

            return toWord;
        }

        // Word

        public static TBdoScriptword<T> NewWord<T>(
            ScriptItemKinds kind,
            string name = null)
            => new TBdoScriptword<T>()
                .WithName(name)
                .WithKind(kind);

        public static TBdoScriptword<T> NewWord<T>(
            IBdoMetaObject meta)
        {
            var word = new TBdoScriptword<T>();

            word
                .WithDataType(BdoExtensionKind.Scriptword, meta?.DataType.ClassReference?.DefinitionUniqueName)
                .With(meta.Items?.ToArray());

            return word;
        }

        public static BdoScriptword NewWord(
            ScriptItemKinds kind,
            string name = null)
            => new BdoScriptword()
                .WithName(name)
                .WithKind(kind);

        public static BdoScriptword NewWord(
            IBdoMetaObject meta)
        {
            var word = new BdoScriptword();

            word
                .WithDataType(BdoExtensionKind.Scriptword, meta?.DataType.ClassReference?.DefinitionUniqueName)
                .With(meta.Items?.ToArray());

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
            var scriptword = NewWord(ScriptItemKinds.Variable)
                .WithName(name);

            return scriptword;
        }

        public static BdoScriptword Var(string name)
            => Variable(name);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public static BdoScriptword Variable<T>(string name)
        {
            var scriptword = NewWord(ScriptItemKinds.Variable)
                .WithName(name);

            return scriptword;
        }

        public static BdoScriptword Var<T>(string name)
            => Variable<T>(name);

        // Function

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="parameters"></param>
        /// <returns></returns>
        public static TBdoScriptword<T> Function<T>(
            string name,
            params object[] parameters)
        {
            var scriptword = NewWord<T>(ScriptItemKinds.Function)
                .WithName(name);

            var index = 0;
            foreach (var param in parameters)
            {
                scriptword.InsertData(param);
                index++;
            }

            return scriptword;
        }

        public static BdoScriptword Function(
            string name,
            params object[] parameters)
        {
            var scriptword = NewWord(ScriptItemKinds.Function)
                .WithName(name);

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

        public static TBdoScriptword<T> Func<T>(
            string name,
            params object[] parameters)
            => Function<T>(name, parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="word"></param>
        /// <param key="name"></param>
        /// <param key="parameters"></param>
        /// <returns></returns>
        public static BdoScriptword Function(
            this IBdoScriptword word,
            string name,
            params object[] parameters)
        {
            return word.WithChild(Function(name, parameters)) as BdoScriptword;
        }

        public static BdoScriptword Func(
            this IBdoScriptword word,
            string name,
            params object[] parameters)
            => word.Function(name, parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="word"></param>
        /// <param key="name"></param>
        /// <param key="parameters"></param>
        /// <returns></returns>
        public static TBdoScriptword<T> Function<T>(
            this IBdoScriptword word,
            string name,
            params object[] parameters)
        {
            return Function<T>(name, parameters).WithParent(word);
        }

        public static TBdoScriptword<T> Func<T>(
            this IBdoScriptword word,
            string name,
            params object[] parameters)
            => word.Function<T>(name, parameters);
    }
}
