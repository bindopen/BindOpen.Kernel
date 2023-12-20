namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoCondition : IIdentified,
        INamed, IReferenced, IBdoObjectNotMetable, ITChild<IBdoCompositeCondition>
    {
        BdoConditionKind Kind { get; set; }
    }
}