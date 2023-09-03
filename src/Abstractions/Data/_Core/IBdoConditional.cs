using BindOpen.System.Data.Conditions;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConditional
    {
        IBdoCondition Condition { get; set; }
    }
}