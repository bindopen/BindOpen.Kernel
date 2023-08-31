namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExpressionCondition : IBdoCondition
    {
        IBdoExpression Expression { get; set; }
    }
}