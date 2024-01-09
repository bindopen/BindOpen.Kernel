namespace BindOpen.Scoping.Script
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
        public static T WithTokenKind<T>(
            this T word,
            ScriptTokenKinds kind)
            where T : IBdoScriptword
        {
            if (word != null)
            {
                word.TokenKind = kind;
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
        public static IBdoScriptword LastChild(this IBdoScriptword word)
        {
            return word.Child == null ? word : word.Child.LastChild();
        }
    }
}