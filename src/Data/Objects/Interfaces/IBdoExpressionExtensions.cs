namespace BindOpen.Data.Objects.Interfaces;

/// <summary>
/// This class provides extension methods for the IBdoExpression interface.
/// </summary>
public static class IBdoExpressionExtensions
{
    /// <summary>
    /// Sets the text and optionally the kind of the specified expression.
    /// </summary>
    /// <typeparam name="T">The expression type.</typeparam>
    /// <param name="exp">The expression to configure.</param>
    /// <param name="text">The text to set.</param>
    /// <param name="kind">The kind of the expression.</param>
    /// <returns>Returns the expression instance for fluent chaining.</returns>
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
                exp.ExpressionKind = kind;
            }
        }

        return exp;
    }

    /// <summary>
    /// Sets the kind of the specified expression to Script.
    /// </summary>
    /// <typeparam name="T">The expression type.</typeparam>
    /// <param name="exp">The expression to configure.</param>
    /// <returns>Returns the expression instance for fluent chaining.</returns>
    public static T AsScript<T>(
        this T exp)
        where T : IBdoExpression
    {
        if (exp != null)
        {
            exp.ExpressionKind = BdoExpressionKind.Script;
        }

        return exp;
    }

    /// <summary>
    /// Sets the kind of the specified expression to Literal.
    /// </summary>
    /// <typeparam name="T">The expression type.</typeparam>
    /// <param name="exp">The expression to configure.</param>
    /// <returns>Returns the expression instance for fluent chaining.</returns>
    public static T AsLiteral<T>(
        this T exp)
        where T : IBdoExpression
    {
        if (exp != null)
        {
            exp.ExpressionKind = BdoExpressionKind.Literal;
        }

        return exp;
    }

    /// <summary>
    /// Sets the kind of the specified expression to Auto.
    /// </summary>
    /// <typeparam name="T">The expression type.</typeparam>
    /// <param name="exp">The expression to configure.</param>
    /// <returns>Returns the expression instance for fluent chaining.</returns>
    public static T AsAuto<T>(
        this T exp)
        where T : IBdoExpression
    {
        if (exp != null)
        {
            exp.ExpressionKind = BdoExpressionKind.Auto;
        }

        return exp;
    }
}