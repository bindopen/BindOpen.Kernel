namespace BindOpen.Script
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

            return word;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithParent<T>(
            this T word, IBdoScriptword scriptword)
            where T : IBdoScriptword
        {
            if (word != null)
            {
                word.Parent = scriptword;
            }

            return word;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        public static T WithDefinitionUniqueName<T>(
            this T word,
            string uniqueName)
            where T : IBdoScriptword
        {
            if (word != null)
            {
                word.DefinitionUniqueName = uniqueName;
            }

            return word;
        }
    }
}