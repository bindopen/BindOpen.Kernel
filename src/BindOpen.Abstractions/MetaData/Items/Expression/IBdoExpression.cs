using BindOpen.Extensions.Scripting;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IBdoExpression : IBdoItem
    {
        /// <summary>
        /// The kind.
        /// </summary>
        BdoExpressionKind Kind { get; set; }

        /// <summary>
        /// The text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IBdoExpression WithText(string text)
        {
            Text = text;
            return this;
        }

        /// <summary>
        /// The script word.
        /// </summary>
        IBdoScriptword Word { get; set; }

        IBdoExpression WithWord(IBdoScriptword word)
        {
            Word = word;
            return this;
        }

        IBdoExpression AsScript();

        IBdoExpression AsLiteral();

        IBdoExpression AsAuto();
    }
}