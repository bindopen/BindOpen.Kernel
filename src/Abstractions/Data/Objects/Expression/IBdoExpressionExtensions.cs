using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoExpressionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="text"></param>
        /// <returns></returns>
        public static T WithText<T>(
            this T exp,
            string text,
            BdoExpressionKind kind = BdoExpressionKind.Any)
            where T : IBdoExpression
        {
            if (exp != null)
            {
                exp.Text = text;
                if (kind != BdoExpressionKind.Any)
                {
                    exp.Kind = kind;
                }
            }

            return exp;
        }

        public static T AsScript<T>(
            this T exp)
            where T : IBdoExpression
        {
            if (exp != null)
            {
                exp.Kind = BdoExpressionKind.Script;
            }

            return exp;
        }


        public static T AsLiteral<T>(
            this T exp)
            where T : IBdoExpression
        {
            if (exp != null)
            {
                exp.Kind = BdoExpressionKind.Literal;
            }

            return exp;
        }

        public static T AsAuto<T>(
            this T exp)
            where T : IBdoExpression
        {
            if (exp != null)
            {
                exp.Kind = BdoExpressionKind.Auto;
            }

            return exp;
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param key="keys">The keys of the item to remove.</param>
        public static T WithWord<T>(
            this T exp,
            IBdoScriptword word)
            where T : IBdoExpression
        {
            if (exp != null)
            {
                exp.Kind = BdoExpressionKind.Word;
                exp.Word = word;
            }

            return exp;
        }
    }
}