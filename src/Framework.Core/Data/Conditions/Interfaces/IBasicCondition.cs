namespace BindOpen.Framework.Core.Data.Conditions
{
    public interface IBasicCondition : ICondition
    {
        object Argument1 { get; set; }
        object Argument2 { get; set; }
        ConditionOperator Operator { get; set; }
    }
}