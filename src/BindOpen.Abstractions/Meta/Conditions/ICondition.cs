using BindOpen.Meta.Items;

namespace BindOpen.Meta.Conditions
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ICondition AsTrue(bool value)
        {
            TrueValue = value;
            return this;
        }
    }
}