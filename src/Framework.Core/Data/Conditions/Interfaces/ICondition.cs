using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Conditions
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