using BindOpen.System.Data;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IBdoScriptwordExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        public static T WithKind<T>(
            this T word,
            ScriptItemKinds kind)
            where T : IBdoScriptword
        {
            if (word != null)
            {
                word.Kind = kind;
            }

            return word;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithChild<T>(
            this T word, IBdoScriptword scriptword)
            where T : IBdoScriptword
        {
            if (word != null)
            {
                word.Child = scriptword;
            }

            if (scriptword != null)
            {
                scriptword.Parent = word;
            }

            return word;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithParent<T>(
            this T word, IBdoScriptword scriptword)
            where T : IBdoScriptword
        {
            scriptword.WithChild(word);

            return word;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static bool IsScriptword(this BdoDataType dataType)
        {
            return dataType >= typeof(IBdoScriptword);
        }
    }
}