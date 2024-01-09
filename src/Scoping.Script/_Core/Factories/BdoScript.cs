using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using System;

namespace BindOpen.Scoping.Script
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
                .WithTokenKind(word?.TokenKind ?? ScriptTokenKinds.None)
                .WithChild(word?.Child)
                .WithParent(word?.Parent as IBdoScriptword);

            foreach (var param in word)
            {
                toWord.Add(param);
            }

            return toWord;
        }

        // Word

        public static ITBdoScriptword<T> NewWord<T>(
            ScriptTokenKinds kind,
            string name = null)
            => new TBdoScriptword<T>()
                .WithName(name)
                .WithTokenKind(kind);

        public static ITBdoScriptword<T> NewWord<T>(
            IBdoMetaObject meta)
        {
            var word = new TBdoScriptword<T>();

            word
                .WithDataType(BdoExtensionKinds.Scriptword, meta?.DataType?.DefinitionUniqueName)
                .With(meta.Items?.ToArray());

            return word;
        }

        public static IBdoScriptword NewWord(
            ScriptTokenKinds kind,
            string name = null)
            => new BdoScriptword()
                .WithName(name)
                .WithTokenKind(kind);

        public static IBdoScriptword NewWord(
            IBdoMetaObject meta)
        {
            var word = new BdoScriptword();

            word
                .WithDataType(BdoExtensionKinds.Scriptword, meta?.DataType?.DefinitionUniqueName)
                .With(meta.Items?.ToArray());

            return word;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IBdoScriptword NewWord<T>(Func<ITBdoScriptword<T>, IBdoScriptword> func)
        {
            var word = func?.Invoke(This<T>());

            return word;
        }

        // Variable

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public static IBdoScriptword Variable(string name)
        {
            var word = NewWord(ScriptTokenKinds.Variable)
                .WithName(name);

            return word;
        }

        public static IBdoScriptword Var(string name)
            => Variable(name);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public static IBdoScriptword Variable(
            this IBdoScriptword word,
            string name)
        {
            return Variable(name).WithParent(word);
        }

        public static IBdoScriptword Var(
            this IBdoScriptword word,
            string name)
            => word.Variable(name);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public static ITBdoScriptword<T> Variable<T>(string name)
        {
            var word = NewWord<T>(ScriptTokenKinds.Variable)
                .WithName(name);

            return word;
        }

        public static ITBdoScriptword<T> Var<T>(string name)
            => Variable<T>(name);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public static ITBdoScriptword<T> Variable<T>(
            this IBdoScriptword word,
            string name)
        {
            return Variable<T>(name).WithParent(word);
        }

        public static ITBdoScriptword<T> Var<T>(
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
        public static ITBdoScriptword<T> Function<T>(
            string name,
            params object[] parameters)
        {
            var word = NewWord<T>(ScriptTokenKinds.Function)
                .WithName(name);

            var index = 0;
            foreach (var param in parameters)
            {
                word.InsertData(param);
                index++;
            }

            return word;
        }

        public static IBdoScriptword Function(
            string name,
            params object[] parameters)
        {
            var word = NewWord(ScriptTokenKinds.Function)
                .WithName(name);

            var index = 0;
            foreach (var param in parameters)
            {
                word.InsertData(param);
                index++;
            }

            return word;
        }

        public static IBdoScriptword Func(
            string name,
            params object[] parameters)
            => Function(name, parameters);

        public static ITBdoScriptword<T> Func<T>(
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
        public static IBdoScriptword Function(
            this IBdoScriptword word,
            string name,
            params object[] parameters)
        {
            return Function(name, parameters).WithParent(word);
        }

        public static IBdoScriptword Func(
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
        public static ITBdoScriptword<T> Function<T>(
            this IBdoScriptword word,
            string name,
            params object[] parameters)
        {
            return Function<T>(name, parameters).WithParent(word);
        }

        public static ITBdoScriptword<T> Func<T>(
            this IBdoScriptword word,
            string name,
            params object[] parameters)
            => word.Function<T>(name, parameters);

        // This

        public static ITBdoScriptword<T> This<T>()
            => Var<T>(BdoData.__VarName_This);

        public static ITBdoScriptword<T> _Parent<T>()
            => Var(BdoData.__VarName_This).Var<T>("parent");

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="metaSet">The variable meta set to use.</param>
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
