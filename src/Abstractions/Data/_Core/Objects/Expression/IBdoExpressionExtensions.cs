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
    }
}