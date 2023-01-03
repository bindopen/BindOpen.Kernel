using BindOpen.Data.Items;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICondition : IBdoItem
    {
        /// <summary>
        /// 
        /// </summary>
        bool TrueValue { get; set; }

        ICondition AsTrue(bool value)
        {
            TrueValue = value;
            return this;
        }
    }
}