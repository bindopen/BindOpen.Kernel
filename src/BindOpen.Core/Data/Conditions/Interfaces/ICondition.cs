using BindOpen.Data.Items;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICondition : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        bool TrueValue { get; set; }
    }
}