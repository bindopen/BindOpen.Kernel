namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IBdoExpression : IBdoObjectNotMetable
    {
        /// <summary>
        /// The kind.
        /// </summary>
        BdoExpressionKind Kind { get; set; }

        /// <summary>
        /// The text.
        /// </summary>
        string Text { get; set; }
    }
}