using BindOpen.Data.Items;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICondition : IBdoHandledItem
    {
        /// <summary>
        /// 
        /// </summary>
        bool TrueValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="value"></param>
        /// <returns></returns>
        ICondition AsTrue(bool value)
        {
            TrueValue = value;
            return this;
        }
    }
}