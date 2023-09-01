using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
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
                .WithDataType(BdoExtensionKinds.Scriptword, meta?.DataType?.DefinitionUniqueName)
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
                .WithDataType(BdoExtensionKinds.Scriptword, meta?.DataType?.DefinitionUniqueName)
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
            var word = NewWord(ScriptItemKinds.Variable)
                .WithName(name);

            return word;
        }

        public static BdoScriptword Var(string name)
            => Variable(name);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public static BdoScriptword Variable(
            this IBdoScriptword word,
            string name)
        {
            return Variable(name).WithParent(word);
        }

        public static BdoScriptword Var(
            this IBdoScriptword word,
            string name)
            => word.Variable(name);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public static TBdoScriptword<T> Variable<T>(string name)
        {
            var word = NewWord<T>(ScriptItemKinds.Variable)
                .WithName(name);

            return word;
        }

        public static TBdoScriptword<T> Var<T>(string name)
            => Variable<T>(name);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public static TBdoScriptword<T> Variable<T>(
            this IBdoScriptword word,
            string name)
        {
            return Variable<T>(name).WithParent(word);
        }

        public static TBdoScriptword<T> Var<T>(
            this IBdoScriptword word,
            string name)
            => word.Variable<T>(name);

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
            var word = NewWord<T>(ScriptItemKinds.Function)
                .WithName(name);

            var index = 0;
            foreach (var param in parameters)
            {
                word.InsertData(param);
                index++;
            }

            return word;
        }

        public static BdoScriptword Function(
            string name,
            params object[] parameters)
        {
            var word = NewWord(ScriptItemKinds.Function)
                .WithName(name);

            var index = 0;
            foreach (var param in parameters)
            {
                word.InsertData(param);
                index++;
            }

            return word;
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
            return Function(name, parameters).WithParent(word);
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

        // This

        public static string __VarName_This = "$this";

        public static TBdoScriptword<T> _This<T>()
            => Var<T>(__VarName_This);

        public static TBdoScriptword<T> _Parent<T>()
            => Var(__VarName_This).Var<T>("parent");

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        [BdoFunction("prop")]
        public static object Property(
            this IBdoObject obj,
            string propName)
        {
            return obj?.GetPropertyValue(propName, typeof(BdoPropertyAttribute));
        }
    }
}
