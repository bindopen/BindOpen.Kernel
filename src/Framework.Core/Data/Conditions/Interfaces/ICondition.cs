using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Conditions
{
    public interface ICondition : IDataItem
    {
        bool TrueValue { get; set; }
    }
}