namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoCondition :
        INamed, IReferenced, IBdoObjectNotMetable
    {
        BdoConditionKind Kind { get; set; }
    }
}