using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Data.Conditions
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